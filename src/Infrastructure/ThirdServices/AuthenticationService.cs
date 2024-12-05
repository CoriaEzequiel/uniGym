using Application.Interfaces;
using Application.Models.Helpers;
using Application.Models.Request;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationAuthenticationService = Application.Interfaces.IAuthenticationService;


namespace Infrastructure.ThirdServices;

public class AuthenticationService : ApplicationAuthenticationService
{
    private readonly IProfessorRepository _professorRepository;
    private readonly IVipClientRepository _vipclientRepository;
    private readonly ISuperAdmRepository _superAdmRepository;
    private readonly AuthenticationServiceOptions _options;

    public AuthenticationService(
        IProfessorRepository professorRepository,
        IVipClientRepository vipclientRepository,
        ISuperAdmRepository superAdmRepository,
        IOptions<AuthenticationServiceOptions> options)
    {
        _professorRepository = professorRepository;
        _vipclientRepository = vipclientRepository;
        _superAdmRepository = superAdmRepository;
        _options = options.Value;
    }

    private User? ValidateUser(AuthenticationRequest authenticationRequest)
    {
        User? user = null;

        var professors = _professorRepository.GetAllProfessors();
        user = professors.FirstOrDefault(x =>
            x.UserName.Equals(authenticationRequest.UserName) &&
            x.Password.Equals(authenticationRequest.Password));

        if (user != null)
        {
            return user;
        }

        var vipclients = _vipclientRepository.GetAllVipClients();
        user = vipclients.FirstOrDefault(x =>
            x.UserName.Equals(authenticationRequest.UserName) &&
            x.Password.Equals(authenticationRequest.Password));

        if (user != null)
        {
            return user;
        }

        
        return user;
    }

    public string Authenticate(AuthenticationRequest authenticationRequest)
    {
        var user = ValidateUser(authenticationRequest);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Authentication failed");
        }

        var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
        var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("Id", user.Id.ToString()));
        claimsForToken.Add(new Claim("Name", user.FirstName));
        claimsForToken.Add(new Claim("LastName", user.LastName));
        claimsForToken.Add(new Claim("TypeUser", user.TypeUser));

        var jwtSecurityToken = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(30),
            credentials);

        var tokenToReturn = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return tokenToReturn.ToString();
    }
}