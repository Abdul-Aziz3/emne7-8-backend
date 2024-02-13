namespace StudentBloggAPI.Models.DTOs
{
    public record CommentDTO(
        int CommentId,
        int PostId,
        int UserId,
        string CommentContent,
        DateTime Created
        );
}
