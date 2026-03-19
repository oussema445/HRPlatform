using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public EmployeeController(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    private async Task SendNotification(string title, string message, string type)
    {
        try {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Add("Authorization", token);

            var notification = new {
                title,
                message,
                type,
                targetRole = "Admin,HR",
                isRead = false
            };

            var json = JsonSerializer.Serialize(notification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync($"{_config["Services:NotificationService"]}/api/notification", content);
        }
        catch { /* Ne pas bloquer si notification échoue */ }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _context.Employees.ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Update(int id, [FromBody] Employee updated)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        var oldStatus = employee.Status;
        employee.FullName = updated.FullName;
        employee.Email = updated.Email;
        employee.Phone = updated.Phone;
        employee.Position = updated.Position;
        employee.Department = updated.Department;
        employee.Salary = updated.Salary;
        employee.Status = updated.Status;
        await _context.SaveChangesAsync();

        // 🔔 Notification si statut changé
        if (oldStatus != updated.Status)
        {
            await SendNotification(
                "Statut Employé Modifié",
                $"Le statut de {employee.FullName} a changé de {oldStatus} à {updated.Status}",
                "StatusChange"
            );
        }

        return Ok(employee);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        // 🔔 Notification
        await SendNotification(
            "Employé Supprimé",
            $"L'employé {employee.FullName} a été supprimé",
            "DeleteEmployee"
        );

        return Ok($"Employé {employee.FullName} supprimé !");
    }
    [HttpPost]
[Authorize(Roles = "Admin,HR")]
public async Task<IActionResult> Create([FromBody] Employee employee)
{
    _context.Employees.Add(employee);
    await _context.SaveChangesAsync();

    // 🔔 Notification
    await SendNotification(
        "Nouvel Employé",
        $"L'employé {employee.FullName} a été ajouté au département {employee.Department}",
        "NewEmployee"
    );

    // 👤 Crée automatiquement un compte
    await CreateUserAccount(employee);

    return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
}

private async Task CreateUserAccount(Employee employee)
{
    try {
        var client = _httpClientFactory.CreateClient();
        var token = Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Add("Authorization", token);

        // Username = prénom.nom en minuscules
        var nameParts = employee.FullName.ToLower().Split(' ');
        var username = nameParts.Length >= 2
            ? $"{nameParts[0]}.{nameParts[1]}"
            : nameParts[0];

        var registerRequest = new {
            username,
            password = "12345", // mot de passe par défaut
            role = "Employee",
            fullName = employee.FullName,
            email = employee.Email,
            employeeId = employee.Id
        };

        var json = JsonSerializer.Serialize(registerRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await client.PostAsync(
            $"{_config["Services:AuthService"]}/api/auth/register",
            content
        );
    }
    catch { }
}
}