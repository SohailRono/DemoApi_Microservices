using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Entities;

namespace DemoNet.Api.Interfaces
{
    public interface ICustomerInfoRepository : IAsyncRepository<CustomerInfo> 
    {
    }
}
