using ReadList.Domain.Models;

namespace ReadList.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> SearchByUsername(string username);
        Task<UserModel> SearchByEmail(string email);
    }
}
