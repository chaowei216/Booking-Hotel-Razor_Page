using AutoMapper;
using BusinessObject.Models;
using Common.DTO;

namespace LuuTrieuViRazorPage.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterRequestDTO, Customer>()
                        .ForMember(c => c.CustomerStatus, options => options.Ignore())
                        .ForMember(c => c.BookingReservations, options => options.Ignore());
            CreateMap<Customer, RegisterRequestDTO>();
        }
    }
}
