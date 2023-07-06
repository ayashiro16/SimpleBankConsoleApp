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

    public decimal Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception("Cannot deposit a negative amount.");
        }
        _account.Balance += amount;
        return _account.Balance;
    }

    public bool TryWithdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Cannot withdraw a negative amount.");
        }
        if (_account.Balance < amount)
        {
            return false;
        }
        _account.Balance -= amount;
        return true;
    }    
    public bool TryWithdraw(decimal amount, out decimal balance)
    {
        var output = TryWithdraw(amount);
        balance = _account.Balance;
        return output;
    }

}