public class Employee
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Position { get; set; }
    public required string Department { get; set; }
    public DateTime HireDate { get; set; }
    public double Salary { get; set; }
    public required string Status { get; set; } // Active, Inactive
}