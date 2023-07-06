using SimpleBankConsoleApp.Models;
using SimpleBankConsoleApp.Utils;

namespace SimpleBankConsoleApp.Controllers;

public class BankController
{
    private BankModel Bank;

    public BankController()
    {
        Bank = new BankModel()
        {
            Accounts = new Dictionary<Guid, AccountController>()
        };
    }

    public Guid AddNewAccount(string first, string last, decimal balance)
    {
        AccountController newAccount = AccountController.Create(first, last, balance, out Guid newId);
        Bank.Accounts.Add(newId, newAccount);
        return newId;
    }

    public AccountController RetrieveAccount(Guid id)
    {
        Bank.Accounts.TryGetValue(id, out AccountController account);
        return account;
    }

    public AccountController DeleteAccount(Guid id)
    {
        if (Bank.Accounts.TryGetValue(id, out AccountController account))
        {
            Bank.Accounts.Remove(id);
        }
        return account;
    }
}