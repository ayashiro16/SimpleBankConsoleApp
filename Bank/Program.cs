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
                MakeDeposit();
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

void MakeDeposit()
{
    if (!GetAccountId(out var id))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    var amount = GetMoneyAmount();
    try
    {
        if (bank.RetrieveAccount(id).TryMakeDeposit(amount, out var balance))
        {
            DisplayText.Success();
            DisplayText.Balance(balance);
        }
        else
        {
            DisplayText.Failure();
        }
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
    var amount = GetMoneyAmount();
    try
    {
        if (bank.RetrieveAccount(id).TryWithdraw(amount, out var balance))
        {
            DisplayText.Success();
            DisplayText.Balance(balance);
        }
        else
        {
            DisplayText.Failure();
        }
    }
    catch
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
        bank.RetrieveAccount(id1);
        bank.RetrieveAccount(id2);
        if (bank.RetrieveAccount(id1).TryWithdraw(amount, out _))
        {
            bank.RetrieveAccount(id2).TryMakeDeposit(amount, out _);
            DisplayText.Success();
        }
        else
        {
            DisplayText.Failure();
        }
    }
    catch
    {
        DisplayText.Failure();
        return;
    }
}

string GetName(string firstOrLast)
{
    DisplayText.EnterName(firstOrLast);
    var name = Console.ReadLine();
    while (!TextValidation.Name(name))
    {
        DisplayText.InvalidName();
        DisplayText.EnterName(firstOrLast);
        name = Console.ReadLine();
    }
    return name;
}

decimal GetStartingBalance()
{
    decimal amount;
    DisplayText.StartingBalance();
    var input = Console.ReadLine();
    while (!decimal.TryParse(input, out amount) || amount < 0)
    {
        DisplayText.InvalidAmount();
        DisplayText.StartingBalance();
        input = Console.ReadLine();
    }
    return amount;
}

bool GetAccountId(out Guid id)
{
    DisplayText.EnterAccountId();
    var input = Console.ReadLine();
    return Guid.TryParse(input, out id);
}

string GetMenuSelection()
{
    DisplayText.SelectFromMenu();
    var input = Console.ReadLine();
    while (!TextValidation.MenuSelection(input))
    {
        DisplayText.InvalidMenuChoice();
        DisplayText.SelectFromMenu();
        input = Console.ReadLine();
    }
    return input;
}

decimal GetMoneyAmount()
{
    DisplayText.EnterDollarAmount();
    var input = Console.ReadLine();
    while (!decimal.TryParse(input, out var amount))
    {
        DisplayText.InvalidAmount();
        DisplayText.EnterDollarAmount();
        input = Console.ReadLine();
    }
    return decimal.Parse(input);
}