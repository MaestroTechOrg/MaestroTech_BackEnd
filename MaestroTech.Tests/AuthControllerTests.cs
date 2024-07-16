using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using MaestroTech.API.Controllers;
using MaestroTech.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text; 

public class AuthControllerTests
{
    private readonly Mock<UserManager<Usuario>> _mockUserManager;
    private readonly Mock<SignInManager<Usuario>> _mockSignInManager;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        var userStoreMock = new Mock<IUserStore<Usuario>>();
        _mockUserManager = new Mock<UserManager<Usuario>>(userStoreMock.Object,
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<IPasswordHasher<Usuario>>(),
            new IUserValidator<Usuario>[0],
            new IPasswordValidator<Usuario>[0],
            Mock.Of<ILookupNormalizer>(),
            Mock.Of<IdentityErrorDescriber>(),
            Mock.Of<IServiceProvider>(),
            Mock.Of<ILogger<UserManager<Usuario>>>());

        var contextAccessor = new Mock<IHttpContextAccessor>();
        var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();

        _mockSignInManager = new Mock<SignInManager<Usuario>>(_mockUserManager.Object,
            contextAccessor.Object,
            userPrincipalFactory.Object,
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<Usuario>>>(),
            Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IUserConfirmation<Usuario>>());

        _mockConfiguration = new Mock<IConfiguration>();

        // Usar uma chave JWT com pelo menos 256 bits (32 caracteres)
        _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns("o@kUq$&NfGZM1ES!LK26MFD$kRl8N1zk");
        _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("MaestroTechAPI");

        _authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task Register_UserWithValidData_ReturnsOk()
    {
        // Arrange
        var registerModel = new RegisterModel { Nome = "Test User", Email = "testuser@example.com", Senha = "Password123!" };
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authController.Register(registerModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Register_UserWithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var registerModel = new RegisterModel { Nome = "Test User", Email = "testuser@example.com", Senha = "Password123!" };
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid data" }));

        // Act
        var result = await _authController.Register(registerModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var loginModel = new LoginModel { Email = "gabriel@gmail.com", Senha = "Lu021805." };
        var user = new Usuario { Email = loginModel.Email };
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

        // Act
        var result = await _authController.Login(loginModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        // Verifique se a resposta é um dicionário de string para objeto
        var tokenResult = okResult.Value as IDictionary<string, object>;
        Assert.NotNull(tokenResult); // Verifica se a resposta não é nula

        // Verifique se contém a chave "token"
        Assert.True(tokenResult.ContainsKey("token"));

        var token = tokenResult["token"] as string;
        Assert.NotNull(token); // Verifique se o token não é nulo

        // Validate the token
        var handler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _mockConfiguration.Object["Jwt:Issuer"],
            ValidAudience = _mockConfiguration.Object["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_mockConfiguration.Object["Jwt:Key"]))
        };

        var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        Assert.NotNull(jwtToken); // Verifique se o token JWT não é nulo

        var expClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp);
        Assert.NotNull(expClaim); // Verifique se o token tem a reivindicação de expiração

        var expValue = long.Parse(expClaim?.Value ?? "0");
        var expDate = DateTimeOffset.FromUnixTimeSeconds(expValue).UtcDateTime;

        Assert.True(expDate > DateTime.UtcNow); // Verifique se a data de expiração é no futuro
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginModel = new LoginModel { Email = "testuser@example.com", Senha = "InvalidPassword" };
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

        // Act
        var result = await _authController.Login(loginModel);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal(401, unauthorizedResult.StatusCode);
    }
}
