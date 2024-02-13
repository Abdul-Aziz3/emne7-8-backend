using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBloggAPI.IntergrationTests.Controller.TestData;
public class TestUserDataItems
{
    public static IEnumerable<object[]> GetTestUsers => new List<object[]>
    {
        new object[]
        {
            new TestUser
            {
                Base64EncodedUsernamePassword = "eW5ndmU6aGVtbWVsaWc=",
                User = new Models.Entities.User
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
                }
            }
        },
        new object[]
        {
            new TestUser
            {
                Base64EncodedUsernamePassword = "cGVyOnBlclBlcnNvbjE/",
                User = new Models.Entities.User
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
                }
            }
        }
    };
}
