namespace SimpleBankConsoleApp.Models;

public class AccountModel
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public decimal Balance { get; set; }
}