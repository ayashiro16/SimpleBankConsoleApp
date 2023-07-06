using SimpleBankConsoleApp.Controllers;

namespace SimpleBankConsoleApp.Models;

public class BankModel
{
    public Dictionary<Guid, AccountController> Accounts { get; } = new();
    
    public AccountController this[Guid key] => Accounts[key];
}