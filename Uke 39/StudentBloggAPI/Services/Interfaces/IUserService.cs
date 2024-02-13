using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces;

public interface IUserService
{
    // async -> Task, Task<T>
    // CRUD : create, read, update, delete

    // read
    Task<ICollection<UserDTO>> GetAllUsersAsync(int pageNr, int pageSize);
    Task<UserDTO?> GetUserByIdAsync(int id);

    //// create
    //Task<UserDTO?> AddUserAsync(UserDTO userDTO);

    // update
    Task<UserDTO?> UpdateUserAsync(int id, UserDTO userDTO);

    // delete
    Task<UserDTO?> DeleteUserAsync(int id);

    Task<int> GetAutenticatedIdAsync(string userName, string password);

    // create
    Task<UserDTO?> RegisterAsync(UserRegistrationDTO userDTP);
}
