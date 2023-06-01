using Azure;
using Azure.Core;
using DemoNet.Api.Data;
using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DemoNet.Api.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DemoDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> CheckLogin(string email)
        {
            var user = await _dbContext.User
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

    }
}
