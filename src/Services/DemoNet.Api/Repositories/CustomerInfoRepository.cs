using DemoNet.Api.Data;
using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Entities;

namespace DemoNet.Api.Repositories
{
    public class CustomerInfoRepository : RepositoryBase<CustomerInfo>, ICustomerInfoRepository
    {
        public CustomerInfoRepository(DemoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
