using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Data.DbContexts;
using UserAPI.Data.Entities;

namespace UserAPI.Repo.Repository
{
    public class UserAPIRepository : IUserAPIRepository
    {
        private readonly UserDbContext userDbContext;
        public UserAPIRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext ?? throw new ArgumentNullException(nameof(userDbContext));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userDbContext.Users.OrderBy(o => o.UserId).ToListAsync();
        }
        public async Task<User?> GetUserAsync(int userId, bool includeInfo)
        {
            if (includeInfo)
            {
                return await userDbContext.Users.Include(i => i.UserInfo)
                    .Where(w => w.UserId == userId).FirstOrDefaultAsync();
            }
            return await userDbContext.Users
                .Where(w => w.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await userDbContext.Users.AnyAsync(a => a.UserId == userId);
        }
        public async Task<UserInfo?> GetInfoForUserAsync(int userId)
        {
            return await userDbContext.UserInfos.Where(w => w.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task AddUser(User user)
        {
            await userDbContext.Users.AddAsync(user);
        }
        public async Task AddInfoForUserAsync(int userId, UserInfo userInfo)
        {
            var user = await GetUserAsync(userId, false);
            if (user != null)
            {
                userInfo.UserId = userId;
                await userDbContext.UserInfos.AddAsync(userInfo);
            }
        }
        public async Task DeleteUser(User user)
        {
            var userInfo = await GetInfoForUserAsync(user.UserId);
            userDbContext.Users.Remove(user);
            if (userInfo != null)
            {
                userDbContext.UserInfos.Remove(userInfo);
            }

        }
        public void DeleteInfo(UserInfo userInfo)
        {
            userDbContext.UserInfos.Remove(userInfo);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await userDbContext.SaveChangesAsync() > 0);
        }
    }
}
