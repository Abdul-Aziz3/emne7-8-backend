using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentBloggAPI.Controllers;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBloggAPI.UnitTests.Controllers;

public class UsersControllerTests
{
    private readonly UsersController _usersController;
    private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

    public UsersControllerTests()
    {
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetUsersByIdAsync_WhenIdIsGiven_ShouldReturn_UserDTOWithId()
    {
        // Arrange
        int id = 1;
        var userDTO = new UserDTO(id,
            "ola", "Ola", "Normann",
            "ola@gmail.com", new DateTime(2023, 11, 7, 12, 0, 0));


        _userServiceMock.Setup(x => x.GetUserByIdAsync(id))
            .ReturnsAsync(userDTO);

        // Act
        var res = await _usersController.GetUsersByIdAsync(id);


        // Assert
        var actionResult = Assert.IsType<ActionResult<UserDTO>>(res);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
        var dto = Assert.IsType<UserDTO>(returnValue.Value);

        Assert.Equal(dto.UserName, userDTO.UserName);
        Assert.Equal(dto.FirstName, userDTO.FirstName);
        Assert.Equal(dto.LastName, userDTO.LastName);
        Assert.Equal(dto.Email, userDTO.Email);
        Assert.Equal(dto.created, userDTO.created);

    }

    [Fact]
    public async Task GetUsersByIdAsync_WhenIdIsGivenAndNotFound_ShouldReturn_NotFound()
    {
        // Arrange
        int id = 1000;

        _userServiceMock.Setup(x => x.GetUserByIdAsync(id))
            .ReturnsAsync(() => null);

        // Act
        var res = await _usersController.GetUsersByIdAsync(id);


        // Assert
        var actionResult = Assert.IsType<ActionResult<UserDTO>>(res);
        var returnValue = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorMessage = Assert.IsType<string>(returnValue.Value);
        Assert.Equal("Fant ikke bruker", errorMessage);
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturn_UserDTO()
    {
        // Arrange
        List<UserDTO> dtos = new List<UserDTO>()
        {
            new UserDTO(1,
            "ola", "Ola", "Normann",
            "ola@gmail.com", new DateTime(2023, 11, 7, 12, 0, 0)),
            new UserDTO(2,
            "per", "Per", "Hander",
            "Per@gmail.com", new DateTime(2023, 11, 7, 12, 0, 0))
        };

        //_userServiceMock.Setup(x => x.GetAllUsersAsync(pageNr, pageSize))
            //.ReturnsAsync(dtos);
        // Act
        var res = await _usersController.GetUsersAsync();

        // Assert
        var ActionResult = Assert.IsType<ActionResult<IEnumerable<UserDTO>>>(res);
        var returnValue = Assert.IsType<OkObjectResult>(ActionResult.Result);
        var dto_result = Assert.IsType<List<UserDTO>>(returnValue.Value);
        //var dto = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(OkResult.Value);

        var dto = dtos.FirstOrDefault();
        Assert.Equal("ola", dto?.UserName);
    }
}
