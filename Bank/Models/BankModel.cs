using SimpleBankConsoleApp.Controllers;

namespace SimpleBankConsoleApp.Models;

public class BankModel
{
    public Dictionary<Guid, AccountController> Accounts { get; init; }
}