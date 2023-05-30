using AutoMapper;
using DemoNet.Api.Models.Entities;
using DemoNet.Api.Models.VwModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoNet.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerInfo, VmCustomerInfo>().ReverseMap();
        }
    }
}
