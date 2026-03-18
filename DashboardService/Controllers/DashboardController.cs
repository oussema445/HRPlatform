using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public DashboardController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();
        var token = Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Add("Authorization", token);
        return client;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var client = CreateClient();

        // Appel vers EmployeeService
        var employeeUrl = _config["Services:EmployeeService"];
        var leaveUrl = _config["Services:LeaveService"];
        var payrollUrl = _config["Services:PayrollService"];

        var employeeResponse = await client.GetAsync($"{employeeUrl}/api/employee");
        var leaveResponse = await client.GetAsync($"{leaveUrl}/api/leave");
        var payrollResponse = await client.GetAsync($"{payrollUrl}/api/payroll/summary/2026/3");

        var employees = JsonSerializer.Deserialize<List<JsonElement>>(
            await employeeResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var leaves = JsonSerializer.Deserialize<List<JsonElement>>(
            await leaveResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var payrollSummary = JsonSerializer.Deserialize<JsonElement>(
            await payrollResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return Ok(new {
            TotalEmployees = employees?.Count ?? 0,
            ActiveEmployees = employees?.Count(e => e.GetProperty("status").GetString() == "Active") ?? 0,
            PendingLeaves = leaves?.Count(l => l.GetProperty("status").GetString() == "Pending") ?? 0,
            ApprovedLeaves = leaves?.Count(l => l.GetProperty("status").GetString() == "Approved") ?? 0,
            PayrollSummary = payrollSummary
        });
    }

    [HttpGet("employees/departments")]
    public async Task<IActionResult> GetByDepartment()
    {
        var client = CreateClient();
        var employeeUrl = _config["Services:EmployeeService"];

        var response = await client.GetAsync($"{employeeUrl}/api/employee");
        var employees = JsonSerializer.Deserialize<List<JsonElement>>(
            await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var byDepartment = employees?
            .GroupBy(e => e.GetProperty("department").GetString())
            .Select(g => new { Department = g.Key, Count = g.Count() });

        return Ok(byDepartment);
    }
} 