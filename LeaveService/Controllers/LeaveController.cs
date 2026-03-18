using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LeaveController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public LeaveController(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration config)
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
        catch { }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leaves = await _context.Leaves.ToListAsync();
        return Ok(leaves);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var leaves = await _context.Leaves
            .Where(l => l.EmployeeId == employeeId)
            .ToListAsync();
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Leave leave)
    {
        leave.Status = "Pending";
        leave.TotalDays = (leave.EndDate - leave.StartDate).Days + 1;
        _context.Leaves.Add(leave);
        await _context.SaveChangesAsync();

        // 🔔 Notification
        await SendNotification(
            "Nouvelle Demande de Congé",
            $"{leave.EmployeeName} a demandé un congé {leave.Type} du {leave.StartDate:dd/MM/yyyy} au {leave.EndDate:dd/MM/yyyy}",
            "NewLeave"
        );

        return CreatedAtAction(nameof(GetAll), new { id = leave.Id }, leave);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusRequest request)
    {
        var leave = await _context.Leaves.FindAsync(id);
        if (leave == null) return NotFound();

        leave.Status = request.Status;
        await _context.SaveChangesAsync();

        // 🔔 Notification
        await SendNotification(
            $"Congé {request.Status}",
            $"Le congé de {leave.EmployeeName} a été {request.Status}",
            "LeaveStatusUpdate"
        );

        return Ok(leave);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var leave = await _context.Leaves.FindAsync(id);
        if (leave == null) return NotFound();
        _context.Leaves.Remove(leave);
        await _context.SaveChangesAsync();
        return Ok("Congé supprimé !");
    }
}

public class StatusRequest
{
    public required string Status { get; set; }
}