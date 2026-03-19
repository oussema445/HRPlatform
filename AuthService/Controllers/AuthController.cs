using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    [HttpPut("change-password")]
[Authorize]
public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
{
    var username = User.Identity?.Name;
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == username);

    if (user == null) return NotFound();

    if (user.PasswordHash != request.OldPassword)
        return BadRequest("Ancien mot de passe incorrect !");

    user.PasswordHash = request.NewPassword;
    await _context.SaveChangesAsync();

    return Ok("Mot de passe modifié avec succès !");
}

public class ChangePasswordRequest
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}

    [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == request.Username
                               && u.PasswordHash == request.Password);

    if (user == null)
        return Unauthorized("Login ou mot de passe incorrect !");

    var token = GenerateToken(user);
    return Ok(new {
        token,
        username = user.Username,
        role = user.Role,
        fullName = user.FullName,
        employeeId = user.EmployeeId  // ← ajoute cette ligne
    });
}
    [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterRequest request)
{
    // Vérifie si username existe déjà
    var exists = await _context.Users
        .AnyAsync(u => u.Username == request.Username);

    if (exists)
        return BadRequest("Ce username existe déjà !");

    var user = new User {
        Username = request.Username,
        PasswordHash = request.Password,
        Role = request.Role,
        FullName = request.FullName,
        Email = request.Email,
        EmployeeId = request.EmployeeId
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return Ok(new { message = "Compte créé !", username = user.Username });
}

public class RegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public int? EmployeeId { get; set; }
}

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FullName", user.FullName),
            new Claim("UserId", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}