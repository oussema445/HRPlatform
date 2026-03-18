public class Leave
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required string EmployeeName { get; set; }
    public required string Type { get; set; } // Annual, Sick, Emergency
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
    public string Status { get; set; } = "Pending"; // ← valeur par défaut
    public string? Reason { get; set; }
}