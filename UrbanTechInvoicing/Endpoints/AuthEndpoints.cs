using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UrbanTechInvoicing.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/register", async (
        UserManager<IdentityUser> userManager,
        RegisterRequest request) =>
        {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };
            var result = await userManager.CreateAsync(user, request.Password);

            return result.Succeeded ? Results.Ok("User registered") : Results.BadRequest(result.Errors);
        });

        routes.MapPost("/login", async (
            UserManager<IdentityUser> userManager,
            IConfiguration config,
            string email, string password) =>
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Results.Unauthorized();
        });

    }
}

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}