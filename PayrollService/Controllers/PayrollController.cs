using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PayrollController : ControllerBase
{
    private readonly AppDbContext _context;

    public PayrollController(AppDbContext context)
    {
        _context = context;
    }

    // GET — tous les salaires
    [HttpGet]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetAll()
    {
        var payrolls = await _context.Payrolls.ToListAsync();
        return Ok(payrolls);
    }

    // GET — salaires par employé
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var payrolls = await _context.Payrolls
            .Where(p => p.EmployeeId == employeeId)
            .ToListAsync();
        return Ok(payrolls);
    }

    // POST — créer une fiche de paie
    [HttpPost]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Create([FromBody] Payroll payroll)
    {
        // Calcul automatique du salaire net
        payroll.NetSalary = payroll.BasicSalary
            + payroll.HousingAllowance
            + payroll.TransportAllowance
            + payroll.Bonus
            - payroll.Deductions;

        _context.Payrolls.Add(payroll);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = payroll.Id }, payroll);
    }

    // PUT — marquer comme payé
    [HttpPut("{id}/pay")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> MarkAsPaid(int id)
    {
        var payroll = await _context.Payrolls.FindAsync(id);
        if (payroll == null) return NotFound();
        payroll.Status = "Paid";
        await _context.SaveChangesAsync();
        return Ok(payroll);
    }

    // GET — résumé mensuel
    [HttpGet("summary/{year}/{month}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetMonthlySummary(int year, int month)
    {
        var payrolls = await _context.Payrolls
            .Where(p => p.Year == year && p.Month == month)
            .ToListAsync();

        return Ok(new {
            TotalEmployees = payrolls.Count,
            TotalNetSalary = payrolls.Sum(p => p.NetSalary),
            TotalPaid = payrolls.Count(p => p.Status == "Paid"),
            TotalPending = payrolls.Count(p => p.Status == "Pending")
        });
    }
}