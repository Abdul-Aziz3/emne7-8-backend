namespace StudentBloggAPI.Models.DTOs;

//readonly propeteries.
public record UserDTO(
    int Id,
    string? UserName,
    string? FirstName,
    string? LastName,
    string? Email, 
    DateTime created);

//public class UserDTO2
//{
//    public UserDTO(int id, string? userName, string? firstName,
//        string? lastName, string? email, DateTime created)
//    {
//        Id=id;
//        UserName=userName;
//        FirstName=firstName;
//        LastName=lastName;
//        Email=email;
//        Created=created;
//    }

//    public int Id { get; }
//    public string? UserName { get; }
//    public string? FirstName { get; }
//    public string? LastName { get; }
//    public string? Email { get; }
//    public DateTime Created { get; }

//}
