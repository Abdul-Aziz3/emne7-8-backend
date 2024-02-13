using Moq;
using Newtonsoft.Json;
using StudentBloggAPI.IntergrationTests.Controller.TestData;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentBloggAPI.IntergrationTests.Controller;

public class UsersControllerTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UsersControllerTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Fact]
    public async Task GetUsersAsync_DefaultPageSize_ReturnTwoUser()
    {
        // Arrange
        List<User> users = new();
        User userA = new User
        {
            Created = new DateTime(2023, 11, 16, 14, 45, 00),
            Email = "yngve.magnussen221@gokstadakademiet.no",
            FirstName = "Yngve",
            UserName = "yngve",
            LastName = "Magnussen",
            Id = 8,
            IsAdminUser = false,
            Salt = "$2a$11$B0X0zfKssgRHdM3E0Kdgwu",
            HashedPassword = "$2a$11$B0X0zfKssgRHdM3E0Kdgwus3HwtpShhhHhxQoT5vG6cZkA2MCpaMW",
            Updated = new DateTime(2023, 11, 16, 14, 45, 00)
        };
        User userB = new User
        {
            Created = new DateTime(2023, 11, 16, 14, 45, 00),
            Email = "per@gmail.com",
            FirstName = "Per",
            UserName = "per",
            LastName = "Person",
            Id = 9,
            IsAdminUser = false,
            Salt = "$2a$11$ODjm9dym15giBjK5Nib/Iu",
            HashedPassword = "$2a$11$ODjm9dym15giBjK5Nib/IuXj4e1MitvvMTVnUDAtefmlNFUmBUsTi",
            Updated = new DateTime(2023, 11, 16, 14, 45, 00)
        };
        users.Add(userA);
        users.Add(userB);

        _factory.UserRepositoryMock.Setup(u => u.GetPagedUsersAsync(
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(users);
        //_factory.UserRepositoryMock.Setup(u => u.GetPagedUsersAsync(1, 10).ReturnsAsync(users);
        _factory.UserRepositoryMock.Setup(u => u.GetUserByNameAsync(userA.UserName)).ReturnsAsync(userA);

        // login for 'yngve'
        string base64EncodedAuthenticatonString = "eW5ndmU6aGVtbWVsaWc=";
        _client.DefaultRequestHeaders.Add("Authorization",
            $"Basic {base64EncodedAuthenticatonString}");

        // Act
        var response = await _client.GetAsync("/api/v1/Users"); //http-request

        // model binding
        var data = JsonConvert
            .DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(data);
        Assert.Collection(data, 
                    u =>
                    {
                        Assert.Equal(userA.FirstName, u.FirstName);
                        Assert.Equal(userA.LastName, u.LastName);
                        Assert.Equal(userA.UserName, u.UserName);
                        Assert.Equal(userA.Email, u.Email);
                        Assert.Equal(userA.Id, u.Id);
                        Assert.Equal(userA.Created, u.created);
                    },
                    u => 
                    {
                        Assert.Equal(userB.FirstName, u.FirstName);
                        Assert.Equal(userB.LastName, u.LastName);
                        Assert.Equal(userB.UserName, u.UserName);
                        Assert.Equal(userB.Email, u.Email);
                        Assert.Equal(userB.Id, u.Id);
                        Assert.Equal(userB.Created, u.created);
                    });
    }

    [Theory] //kan bruke paramenter(argumenter)
    [MemberData(nameof(TestUserDataItems.GetTestUsers), MemberType = typeof(TestUserDataItems))]   //[InlineData(1, 3)] - lette data
    public async Task GetUsersAsync_DefaultPageSize_ReturnOneUser(TestUser testUser)
    {
        // Arrange
        User user = testUser.User!;

        _factory.UserRepositoryMock.
            Setup(u => u.GetPagedUsersAsync(1, 10))
            .ReturnsAsync(new List<User> { user! });

        _factory.UserRepositoryMock.Setup(u => u.GetUserByNameAsync(user.UserName))
            .ReturnsAsync(user);

        _client.DefaultRequestHeaders.Add("Authorization",
            $"Basic {testUser.Base64EncodedUsernamePassword}");

        // Act
        var response = await _client.GetAsync("/api/v1/Users");

        var data = JsonConvert
            .DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(data);
        Assert.Collection(data,
                    response_user =>
                    {
                        Assert.Equal(user.FirstName, response_user.FirstName);
                        Assert.Equal(user.LastName, response_user.LastName);
                        Assert.Equal(user.UserName, response_user.UserName);
                        Assert.Equal(user.Email, response_user.Email);
                        Assert.Equal(user.Id, response_user.Id);
                        Assert.Equal(user.Created, response_user.created);
                    });   
    }





    //[Fact]
    //public async Task GetUsersByIdAsync_WhenIdIsGiven_ShouldReturn_UserDTOWithId()
    //{
    //    // Arrange
    //    //_factory.UserRepositoryMock.Setup(x => x.getUser)

    //    // Act
    //    _client.DefaultRequestHeaders.Add("Authorization", "Basic eW5ndmU6aGVtbWVsaWc=");
    //    var response = await _client.GetAsync("/api/v1/Users/8");

    //    var data = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());

    //    // Assert
    //    Assert.Equal(HttpStatusCode.OK, response.StatusCode );
    //    Assert.NotNull(data);
    //    Assert.Equal("yngve", data.FirstName);

    //}
}
