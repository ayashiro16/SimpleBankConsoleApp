using IAccountController = SimpleBankConsoleApp.Interfaces.IAccountController;

namespace SimpleBankConsoleApp.Models;

public class BankModel
{
    public Dictionary<Guid, IAccountController> Accounts { get; } = new();
}