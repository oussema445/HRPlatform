public class Notification
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public required string Type { get; set; } // NewEmployee, NewLeave, NewPayroll
    public required string TargetRole { get; set; } // Admin, HR, All
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

