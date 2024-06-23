using DataAccess.Repository.Interface;
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

        public RoomStatusUpdateWorker(
            ILogger<RoomStatusUpdateWorker> logger,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration config)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
                        room.RoomStatus = 1;
                    }
                    else
                    {
                        room.RoomStatus = 0;
                    }

                    roomRepository.UpdateRoom(room);
                }
            }

            _logger.LogInformation("Room status update process completed.");
        }
    }
}
