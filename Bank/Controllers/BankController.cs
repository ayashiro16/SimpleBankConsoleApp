using IAccountController = SimpleBankConsoleApp.Interfaces.IAccountController;
using IBankController = SimpleBankConsoleApp.Interfaces.IBankController;
using BankModel = SimpleBankConsoleApp.Models.BankModel;

namespace SimpleBankConsoleApp.Controllers;

public class BankController : IBankController
{
    private readonly BankModel _bank;

    private BankController(BankModel bank)
    {
        _bank = bank;
    }

    public static BankController Create() => new (new BankModel());


    public Guid CreateAccount(string first, string last, decimal balance)
    {
        var newAccount = AccountController.Create(first, last, balance, out var newId);
        _bank.Accounts.Add(newId, newAccount);
        return newId;
    }

    public IAccountController? RetrieveAccount(Guid id) => _bank.Accounts.TryGetValue(id, out var account)
        ? account
        : null;

}