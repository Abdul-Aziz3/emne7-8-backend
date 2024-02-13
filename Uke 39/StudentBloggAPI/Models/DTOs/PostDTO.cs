namespace StudentBloggAPI.Models.DTOs;

public record PostDTO(
    int PostId,
    int UserId,
    string Header, 
    string PostContent,
    DateTime Created);
