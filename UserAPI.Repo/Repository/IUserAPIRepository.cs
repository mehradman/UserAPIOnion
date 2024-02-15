using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Data.Entities;

namespace UserAPI.Repo.Repository
{
    public interface IUserAPIRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int userId, bool includeInfo);
        Task<bool> UserExistsAsync(int userId);
        Task<UserInfo?> GetInfoForUserAsync(int userId);
        Task AddUser(User user);
        Task AddInfoForUserAsync(int userId, UserInfo userInfo);
        Task DeleteUser(User user);
        void DeleteInfo(UserInfo userInfo);
        Task<bool> SaveChangesAsync();

    }
}
