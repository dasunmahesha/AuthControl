using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AuthControl.Application.Interfaces;
using AuthControl.Domain.Entities;
using AuthControl.Infrastructure.Data;


namespace AuthControl.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _dbContext;

        public UserRepository(IDapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                string sql = "GetUserByUsername"; 
                var user = await dbConnection.QueryFirstOrDefaultAsync<User>(
                    sql,
                    new { Username = username },
                    commandType: CommandType.StoredProcedure
                );
                return user;
            }
        }

        public async Task AddUserAsync(User user)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                string sql = "AddUser"; 
                await dbConnection.ExecuteAsync(sql, new
                {
                    user.Username,
                    user.PasswordHash,
                    user.Email,
                    user.Role
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
