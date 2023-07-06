using SimpleBankConsoleApp.Models;

namespace SimpleBankConsoleApp.Controllers;

public class AccountController
{
    private readonly AccountModel _account;

    private AccountController(AccountModel account)
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

    public decimal CheckBalance() => _account.Balance;

    public bool TryMakeDeposit(decimal amount, out decimal balance)
    {
        if (amount <= 0)
        {
            balance = _account.Balance;
            return false;
        }
        _account.Balance += amount;
        balance = _account.Balance;
        return true;
    }

    public bool TryWithdraw(decimal amount, out decimal balance)
    {
        if (amount <= 0 || _account.Balance < amount)
        {
            balance = _account.Balance;
            return false;
        }
        _account.Balance -= amount;
        balance = _account.Balance;
        return true;
    }

}