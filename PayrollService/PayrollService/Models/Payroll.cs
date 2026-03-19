public class Payroll
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required string EmployeeName { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public double BasicSalary { get; set; }
    public double HousingAllowance { get; set; }
    public double TransportAllowance { get; set; }
    public double Bonus { get; set; }
    public double Deductions { get; set; }
    public double NetSalary { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Paid
}