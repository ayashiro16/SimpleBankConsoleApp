using SimpleBankConsoleApp.Models;

namespace SimpleBankConsoleApp.Controllers;

public class AccountController
{
    protected readonly AccountModel _account;

    public AccountController(AccountModel account)
    {
        _account = account;
    }

    public static AccountController Create(string first, string last, decimal balance, out Guid id)
    {
        var account = new AccountModel()
        {
            FirstName = first,
            LastName = last,
            Balance = balance,
            Id = Guid.NewGuid()
        };
        id = account.Id;
        return new AccountController(account);
    }

    public decimal CheckBalance()
    {
        return _account.Balance;
    }

    public decimal MakeDeposit(decimal amount)
    {
        _account.Balance += amount;
        return _account.Balance;
    }

    public decimal Withdraw(decimal amount)
    {
        _account.Balance -= amount;
        return _account.Balance;
    }

}