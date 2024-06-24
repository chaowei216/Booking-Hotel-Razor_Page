using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class RoomStatusUpdateWorker : BackgroundService
    {
        private readonly ILogger<RoomStatusUpdateWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _config;
        private readonly HubConnection _hubConnection;

        public RoomStatusUpdateWorker(
            ILogger<RoomStatusUpdateWorker> logger,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration config)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_config["BookingHub"]!)
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _hubConnection.StartAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while connecting to SignalR hub.");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateRoomStatusesAsync();
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating room statuses.");
                }
            }
        }

        private async Task UpdateRoomStatusesAsync()
        {
            _logger.LogInformation("Starting room status update process...");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var roomRepository = scope.ServiceProvider.GetRequiredService<IRoomRepository>();
                var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingReservationRepository>();

                var rooms = await roomRepository.GetAllRooms();
                foreach (var room in rooms)
                {
                    var currentBooking = await bookingRepository.GetBookingOfRoomByCurrentDate(room.RoomId);
                    if (currentBooking != null)
                    {
                        room.RoomStatus = 0;
                    }
                    else
                    {
                        room.RoomStatus = 1;
                    }

                    roomRepository.UpdateRoom(room);

                    await _hubConnection.InvokeAsync("UpdateRoomStatus", room.RoomId, room.RoomStatus);
                }
            }

            _logger.LogInformation("Room status update process completed.");
        }
    }
}
