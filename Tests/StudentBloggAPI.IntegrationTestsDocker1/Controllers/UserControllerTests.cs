using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentBloggAPI.IntegrationTestsDocker1.Controllers;

public class UserControllerTests : IAsyncLifetime
{
	private readonly StudentBloggWebAppFactory _factory;
	private readonly HttpClient _client;

    public UserControllerTests()
    {
        _factory = new StudentBloggWebAppFactory();
        _client = _factory.CreateClient();
    }

	[Fact]
    public async Task GetUsersAsync_DefualtPageSize_ReturnData()
    {
        // arrange
        // set up test data response!!

        // act
        var response = await _client.GetAsync("/api/v1/Users");

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

	public async Task InitializeAsync()
	{
		await _factory.InitializeAsync();
	}
	public async Task DisposeAsync()
	{
        await _factory.DisposeAsync();
	}
}
