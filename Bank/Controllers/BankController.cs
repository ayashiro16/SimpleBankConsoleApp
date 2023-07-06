using SimpleBankConsoleApp.Models;

namespace SimpleBankConsoleApp.Controllers;

public class BankController
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

    public AccountController RetrieveAccount(Guid id) => _bank[id];

}