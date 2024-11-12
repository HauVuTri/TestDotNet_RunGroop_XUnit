using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RunGroopWebApp.Tests.Controller
{
    public class AccountControllerTests
    {
        //Mock Services
        private readonly Mock<UserManager<AppUser>> _userManager;
        private readonly Mock<SignInManager<AppUser>> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly Mock<ILocationService> _locationService;
        //SUT 
        private readonly AccountController accountController;

        public AccountControllerTests()
        {
            _userManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(),
                null, // IOptions<IdentityOptions>
                null, // IPasswordHasher<AppUser>
                null, // IEnumerable<IUserValidator<AppUser>>
                null, // IEnumerable<IPasswordValidator<AppUser>>
                null, // ILookupNormalizer
                null, // IdentityErrorDescriber
                null, // IServiceProvider
                null  // ILogger<UserManager<AppUser>>
            );
            _signInManager = new Mock<SignInManager<AppUser>>(_userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(), null, null, null, null);
            _locationService = new Mock<ILocationService>();

            //Setup InMemory Database for ApplicationDBContext
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new ApplicationDbContext(options);

            //System under test SUT
            accountController = new AccountController(_userManager.Object, _signInManager.Object, _context, _locationService.Object);


        
        }
        [Fact]
        public void AccountController_Login_Get_ReturnsView()
        {
            //
            var res = accountController.Login();
            res.Should().BeOfType<ViewResult>();
        }

    }
}
