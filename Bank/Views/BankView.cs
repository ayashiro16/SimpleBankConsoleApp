using IBankView = SimpleBankConsoleApp.Interfaces.IBankView;
using BankController = SimpleBankConsoleApp.Controllers.BankController;
using DisplayText = SimpleBankConsoleApp.Utils.DisplayText;

namespace SimpleBankConsoleApp.Views;

public class BankView : IBankView
{
    private readonly BankController _bank;

    public BankView()
    {
        _bank = BankController.Create();
    }
        
    public void CreateAccount()
    {
        var first = GetName("first");
        var last = GetName("last");
        var balance = GetStartingBalance();
        var id = _bank.CreateAccount(first, last, balance);
        DisplayText.AccountCreated(id);
    }

    public void Deposit()
    {
        if (!GetAccountId(out var id))
        {
            DisplayText.InvalidAccountId();
            return;
        }
        try
        {
            var account = _bank.RetrieveAccount(id);
            var amount = GetMoneyAmount();
            var balance = account.Deposit(amount);
            DisplayText.Success();
            DisplayText.Balance(balance);
        }
        catch (KeyNotFoundException)
        {
            DisplayText.AccountNotFound();
        }
        catch
        {
            DisplayText.Failure();
        }
    }

    public void Withdraw()
    {
        if (!GetAccountId(out var id))
        {
            DisplayText.InvalidAccountId();
            return;
        }
        try
        {
            var account = _bank.RetrieveAccount(id);
            var amount = GetMoneyAmount();
            if (account.TryWithdraw(amount, out var balance))
            {
                DisplayText.Success();
                DisplayText.Balance(balance);
            }
            else
            {
                DisplayText.Failure();
            }
        }
        catch (KeyNotFoundException)
        {
            DisplayText.AccountNotFound();
        }
        catch (Exception)
        {
            DisplayText.Failure();
        }
    }

    public void CheckBalance()
    {
        if (!GetAccountId(out var id))
        {
            DisplayText.InvalidAccountId();
            return;
        }
        try
        {
            DisplayText.Balance(_bank.RetrieveAccount(id).CheckBalance());
        }
        catch (KeyNotFoundException)
        {
            DisplayText.AccountNotFound();
        }
        catch
        {
            DisplayText.Failure();
        }
    }

    public void TransferFunds()
    {
        Console.WriteLine("TRANSFER FROM");
        if (!GetAccountId(out var id1))
        {
            DisplayText.InvalidAccountId();
            return;
        }
        Console.WriteLine("TRANSFER TO");
        if (!GetAccountId(out var id2))
        {
            DisplayText.InvalidAccountId();
            return;
        }
        var amount = GetMoneyAmount();
        try
        {
            if (_bank.RetrieveAccount(id1)?.TryWithdraw(amount) ?? false)
            {
                _bank.RetrieveAccount(id2).Deposit(amount);
                DisplayText.Success();
            }
            else
            {
                DisplayText.Failure();
            }
        }
        catch (KeyNotFoundException)
        {
            DisplayText.AccountNotFound();
        }
        catch
        {
            DisplayText.Failure();
        }
    }
    
    
    private string GetName(string firstOrLast)
    {
        while (true)
        {
            DisplayText.EnterName(firstOrLast);
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name) && name.All(char.IsLetter))
            {
                return name;
            }
            DisplayText.InvalidName();
        }
    }
    
    private decimal GetMoneyAmount()
    {
        while (true)
        {
            DisplayText.EnterDollarAmount();
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out var amount))
            {
                return amount;
            }
            DisplayText.InvalidAmount();
        }
    }

    private decimal GetStartingBalance()
    {
        while (true)
        {
            DisplayText.StartingBalance();
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out var amount) && amount >= 0)
            {
                return amount;
            }
            DisplayText.InvalidAmount();
        }
    }

    private bool GetAccountId(out Guid id)
    {
        DisplayText.EnterAccountId();
        var input = Console.ReadLine();
        return Guid.TryParse(input, out id);
    }


}