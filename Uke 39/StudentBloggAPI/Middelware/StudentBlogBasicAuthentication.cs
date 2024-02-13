using StudentBloggAPI.Services.Interfaces;
using System.Diagnostics;

namespace StudentBloggAPI.Middelware;

public class StudentBlogBasicAuthentication : IMiddleware
{
    private readonly IUserService _userService;

    public StudentBlogBasicAuthentication(IUserService userService)
    {
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // gylidig path /api/v1/users/register -> gyldig path som ikke trenger authentication
        if (context.Request.Path.StartsWithSegments("/api/v1/Users/register") &&
            context.Request.Method == "POST")
        {
            await next(context);
            return;
        }

        // gylidig path /api/Hello -> gyldig path som ikke trenger authentication
        if (context.Request.Path.StartsWithSegments("/api/Hello") &&
            context.Request.Method == "GET")
        {
            await next(context);
            return;
        }

        try
        {
            //if (!context.Request.Headers.ContainsKey("Authorization"))
            //    throw new UnauthorizedAccessException("'Autohorization' mangler i HTTP-header");

            //var authHeader = context.Request.Headers.Authorization;  //context.Request.Headers["Authorization"]

            //// Basic YWJkaTEyOmhhbmRlcg=
            //string base64string = authHeader.ToString().Split(" ")[1];

            //// "abdi12:hander"
            //string user_password = DecodeBase64String(base64string);

            //string[] arr = user_password.Split(":");
            //string userName = arr[0];
            //string password = arr[1];

            //// userName -> slå opp i database !!
            //int userId = await _userService.GetAutenticatedIdAsync(userName, password);
            //if (userId == 0) 
            //{
            //    //HTTP 401
            //   throw new UnauthorizedAccessException("Ingen tilgang til dette API");
            //}

            context.Items["UserId"] = 1;
            ////context.Items["userName"] = userName;

            await next(context);
        }
        catch (UnauthorizedAccessException ex) 
        {
            await Results.Problem(
                title: "Unauthorized: Ikke lov til å bruke API",
                statusCode: StatusCodes.Status401Unauthorized,
                extensions: new Dictionary<string, Object?>
                {
                    { "traceId", Activity.Current?.Id }
                })
                .ExecuteAsync(context);
        }
        
    }
    private string DecodeBase64String(string base64string)
    {
        byte[] base64bytes = System.Convert.FromBase64String(base64string);
        string username_and_password = System.Text.Encoding.UTF8.GetString(base64bytes);
        return username_and_password;
    }
}
