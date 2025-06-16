using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            return result.Succeeded
                ? Results.Ok(new { success = true, message = "User registered" })
                : Results.BadRequest(new { success = false, message = "Registration failed", errors = result.Errors });

        });

        routes.MapPost("/login", async (
            [FromBody] LoginRequest loginRequest,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> SignInManager,
            IConfiguration config) =>
            
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email!)
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

                return Results.Ok(new
                {
                    success = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user = new { email = user.Email }
                });
            }
            return Results.Json(new { success = false, message = "Invalid credentials" }, statusCode: 401);
        });

        routes.MapGet("/logout", async (SignInManager<IdentityUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok("User logged out");
        });
        routes.MapGet("/user", async (HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Results.Unauthorized();
            }
            return Results.Ok(new { UserId = userId, Email = httpContext.User.Identity?.Name });
        }).RequireAuthorization();

    }
}

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}