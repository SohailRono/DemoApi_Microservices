using DemoNet.Api.Models.Entities;

namespace DemoNet.Api.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> CheckLogin(string email);
    }
}
