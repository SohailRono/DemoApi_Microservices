using AutoMapper;
using DemoNet.Api.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoNet.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerInfo, VmCustomerInfo>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
