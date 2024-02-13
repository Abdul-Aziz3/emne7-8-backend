using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Controllers;

//https://localhost:7251/api/v1/Users

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(int pageNr = 1, int pageSize = 10)
    {
        var res = await _userService.GetAllUsersAsync(pageNr, pageSize);
        return res != null 
            ? Ok(res)
            : NoContent();
    }

    [HttpGet("{id}", Name = "GetUsersId")]
    public async Task<ActionResult<UserDTO>> GetUsersByIdAsync(int id)
    {
        var res = await _userService.GetUserByIdAsync(id);
        return res != null
            ? Ok(res)
            : NotFound("Fant ikke bruker");
        /*
        if (res == null)
        {
            return NotFound("fant ikke bruker");
        }
        return Ok(res);
        */
    }

    //[HttpGet("{id}/posts", Name = "GetUserPosts")]
    //public async Task<ActionResult<IEnumerable<PostDTO>>>

    // https://localhost:7251/api/v1/Users/2
    [HttpDelete("{id}", Name = "DeleteUser")]
    //[Authorize]
    public async Task<ActionResult<UserDTO>> DeleteUserAsync(int id)
    {

        int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

       // var authenticatedUserId = await _userService.GetAutenticatedIdAsync(userName, password);

        if(id == authenticatedUserId)
        {
            var res = await _userService.DeleteUserAsync(id);

            return res != null ? Ok(res) : BadRequest("kan ikke slette bruker");
        }

        return Unauthorized("Du har ikke tilgang til å slette denne brukeren.");
        
        
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUserAsync(int id, UserDTO dto)
    {
        int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

        if (id != authenticatedUserId)
        {

            return Unauthorized("Du har ikke tilgang til å slette denne brukeren.");
            
        }
        var res = await _userService.UpdateUserAsync(id, dto);
        return res != null ? Ok(res) : NotFound("Fikk ikke oppdatert bruker eller brukeren eksistere ikke");
    }

    [HttpPost("register",Name = "AddUser")]
    public async Task<ActionResult<UserDTO>> AddUserAsync(UserRegistrationDTO userRegDTO)
    {
        // modelbinding
        // validering har skjedd
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userDTO = await _userService.RegisterAsync(userRegDTO);
        return userDTO != null
            ? Ok(userDTO)
            : BadRequest("Klarte ikke å registere ny bruker.");
    }
}







//var user = await _userService.AddUserAsync(userDTO);
//if (user == null)
//    return BadRequest("Klarte ikke å legge til ny bruker.");

//return Ok(user);

