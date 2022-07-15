namespace EmployeeManagement.Api.Models;

public class CreateKey
{
    public int Size { get; set; }
    public string Algorithm { get; set; } = null!;
}