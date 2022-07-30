using Task4.Domain.Entities.DTOs;

namespace Task4.Domain.Interfaces;

public interface IAdminService
{
    Task<IEnumerable<UserDto>> GetUsers();
    Task ChangeUserStatus(int[] userIds,bool isActive);
    Task DeleteUsers(int[] userIds);
    
}