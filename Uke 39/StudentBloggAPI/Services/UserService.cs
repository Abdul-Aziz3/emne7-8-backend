using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services;

public class UserService : IUserService
{
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegMapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IMapper<User, UserDTO> userMapper, 
        IMapper<User, UserRegistrationDTO> userRegMapper,
        IUserRepository userRepository,
        ILogger<UserService> logger)
    {
        _userMapper = userMapper;
        _userRegMapper=userRegMapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    

    public async Task<UserDTO?> DeleteUserAsync(int id)
    {
        
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        //if (user.Id != int.Parse(authenticatedUserId))
        //{
        //    return null;
        //}

        var deletedUser = await _userRepository.DeleteUserByIdAsync(id);

        return deletedUser != null ? _userMapper.MapToDTO(deletedUser) : null;
    }

    public async Task<ICollection<UserDTO>> GetAllUsersAsync(int pageNr, int pageSize)
    {
        _logger.LogInformation($"Fetching users for page number {pageNr} with page size {pageSize}");
        var users = await _userRepository.GetPagedUsersAsync(pageNr, pageSize);

        // Mapping
        var dtos = users.Select(user => _userMapper.MapToDTO(user)).ToList();
        _logger.LogDebug($"Fetched {dtos.Count} users successfully.");

        return dtos;
    }

    public async Task<int> GetAutenticatedIdAsync(string userName, string password)
    {
        var usr = await _userRepository.GetUserByNameAsync(userName);
        if (usr == null)
            return 0;

        // prøver å verifisere passwordet mot lagret hash-verdi
        if( BCrypt.Net.BCrypt.Verify(password, usr.HashedPassword))
        {
            return usr.Id;
        }
        return 0;
    }

    public async Task<UserDTO?> GetUserByIdAsync(int id)
    {
        //var user = await _userRepository.GetUserByIdAsync(id);
        //if (user == null)
        //{
        //    return null;
        //}
        //var dto = _userMapper.MapToDTO(user);

        //return dto;
        var res = await _userRepository.GetUserByIdAsync(id);
        return res != null ? _userMapper?.MapToDTO(res) : null;
    }

    public async Task<UserDTO?> RegisterAsync(UserRegistrationDTO userRegDTO)
    {
        // UserRegistration -> User (Mapping)
        var user = _userRegMapper.MapToModel(userRegDTO);

        // lage salt og hashverdier
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegDTO.Password, user.Salt);

        var res = await _userRepository.AddUserAsync(user);

        return _userMapper.MapToDTO(res!);
    }

    public async Task<UserDTO?> UpdateUserAsync(int id, UserDTO userDto)
    {
        var user = _userMapper.MapToModel(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(id, user);

        return updatedUser != null ? _userMapper.MapToDTO(updatedUser) : null;

    
    }
}


//public async Task<UserDTO?> AddUserAsync(UserDTO userDTO)
//{
//    var user = _userMapper.MapToModel(userDTO);
//    var res = await _userRepository.AddUserAsync(user);

//    if (res == null)
//        return null;

//    return userDTO;
//}