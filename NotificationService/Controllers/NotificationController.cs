using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotificationController(AppDbContext context)
    {
        _context = context;
    }

    // GET — toutes les notifications
    [HttpGet]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetAll()
    {
        var notifications = await _context.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return Ok(notifications);
    }

    // GET — notifications non lues
    [HttpGet("unread")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetUnread()
    {
        var notifications = await _context.Notifications
            .Where(n => !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return Ok(new {
            Count = notifications.Count,
            Notifications = notifications
        });
    }

    // GET — notifications par employé
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.EmployeeId == employeeId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return Ok(new {
            Count = notifications.Count(n => !n.IsRead),
            Notifications = notifications
        });
    }

    // POST — créer une notification
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Notification notification)
    {
        notification.CreatedAt = DateTime.Now;
        notification.IsRead = false;
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = notification.Id }, notification);
    }

    // PUT — marquer comme lue
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return NotFound();
        notification.IsRead = true;
        await _context.SaveChangesAsync();
        return Ok(notification);
    }

    // PUT — marquer toutes comme lues
    [HttpPut("read-all")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var notifications = await _context.Notifications
            .Where(n => !n.IsRead)
            .ToListAsync();
        notifications.ForEach(n => n.IsRead = true);
        await _context.SaveChangesAsync();
        return Ok($"{notifications.Count} notifications marquées comme lues !");
    }

    // DELETE
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return NotFound();
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return Ok("Notification supprimée !");
    }
}