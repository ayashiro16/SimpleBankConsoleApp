// See https://aka.ms/new-console-template for more information

using SimpleBankConsoleApp.Controllers;
using SimpleBankConsoleApp.Utils;

BankController bank;
Start();

void Start()
{
    bank = BankController.Create();
    MainMenu();
}

void MainMenu()
{
    string selection;
    do
    {
        DisplayText.MainMenu();
        selection = GetMenuSelection();
        switch (selection)
        {
            case "1":
                CreateAccount();
                break;
            case "2":
                Deposit();
                break;
            case "3":
                Withdraw();
                break;
            case "4":
                CheckBalance();
                break;
            case "5":
                TransferFunds();
                break;
            case "6":
                DisplayText.Exit();
                break;
        }
    } while (selection != "6");
}

void CreateAccount()
{
    var first = GetName("first");
    var last = GetName("last");
    var balance = GetStartingBalance();
    var id = bank.CreateAccount(first, last, balance);
    DisplayText.AccountCreated(id);
}

void Deposit()
{
    if (!GetAccountId(out var id))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    try
    {
        var account = bank.RetrieveAccount(id);
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

void Withdraw()
{
    if (!GetAccountId(out var id))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    try
    {
        var account = bank.RetrieveAccount(id);
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

void CheckBalance()
{
    if (!GetAccountId(out var id))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    try
    {
        DisplayText.Balance(bank.RetrieveAccount(id).CheckBalance());
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

void TransferFunds()
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
        if (bank.RetrieveAccount(id1)?.TryWithdraw(amount) ?? false)
        {
            bank.RetrieveAccount(id2).Deposit(amount);
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

string GetName(string firstOrLast)
{
    while (true)
    {
        DisplayText.EnterName(firstOrLast);
        var name = Console.ReadLine();
        if (TextValidation.Name(name))
        {
            return name;
        }
        DisplayText.InvalidName();
    }
}

decimal GetStartingBalance()
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

bool GetAccountId(out Guid id)
{
    DisplayText.EnterAccountId();
    var input = Console.ReadLine();
    return Guid.TryParse(input, out id);
}

string GetMenuSelection()
{
    while (true)
    {
        DisplayText.SelectFromMenu();
        var input = Console.ReadLine();
        if (TextValidation.MenuSelection(input))
        {
            return input;
        }
        DisplayText.InvalidMenuChoice();
    }
}

decimal GetMoneyAmount()
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