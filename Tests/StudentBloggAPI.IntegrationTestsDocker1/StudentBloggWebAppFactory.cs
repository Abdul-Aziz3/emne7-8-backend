using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentBloggAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MySql;

namespace StudentBloggAPI.IntegrationTestsDocker1;

public class StudentBloggWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
	private readonly MySqlContainer _mySqlContainer;
	private readonly int _port = Random.Shared.Next(10000, 60000); //45454;

    public StudentBloggWebAppFactory()
    {
		_mySqlContainer = new MySqlBuilder()
			.WithImage("abdul80/studbloggt-db:v1")
			.WithPortBinding(_port, 3306)
			.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
			.Build();
    }

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureTestServices(services =>
		{
			// først ta bort DbContextOptions
			var descriptor = services.FirstOrDefault(
				s => s.ServiceType == typeof(DbContextOptions<StudentBloggDbContext>));
			if (descriptor is not null)
			{
				services.Remove(descriptor);
			}

			var conn = $"Server=localhost;Port={_port};Database=ga_emne7_studentblogg; User ID=ga-app;Password=ga-5ecret-%;";
			services.AddDbContext<StudentBloggDbContext>(options =>
			{
				options.UseMySql(
					conn,
					new MySqlServerVersion(new Version(8, 0, 33)));
			});
		});
	}

	public async Task InitializeAsync()
	{
		await _mySqlContainer.StartAsync();
	}

	public new async Task DisposeAsync()
	{
		await _mySqlContainer.StopAsync();
	}
}
