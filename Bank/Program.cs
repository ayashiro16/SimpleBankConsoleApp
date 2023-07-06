﻿// See https://aka.ms/new-console-template for more information

using System.Net.Mime;
using System.Runtime.CompilerServices;
using SimpleBankConsoleApp.Controllers;
using SimpleBankConsoleApp.Utils;

BankController bank;
Start();

void Start()
{
    bank = new BankController();
    MainMenu();
}

// string OpenDialogue(Context context)
// {
//     Console.Write(); //TODO write the prompt according to the context
//     string response = Console.ReadLine();
//     return response;
// }


// this looks like it'll become a mess of switch statements. abandoning this method

// void ProcessUserRequest(Context context, string response)
// {
//     switch (context)
//     {
//         case MainMenu:
//             if (TextValidation.MenuSelection(response))
//             {
//                 switch (response)
//                 {
//                     
//                 }
//             }
//             break;
//         default:
//             Console.WriteLine("Please make a valid selection.");
//             break;
//     }
// }

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
    string first = GetName("first");
    string last = GetName("last");
    decimal balance = GetStartingBalance();
    Guid id = bank.AddNewAccount(first, last, balance);
    DisplayText.AccountCreated(id);
}

void MakeDeposit()
{
    if (!GetAccountId(out Guid id))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    decimal amount = GetMoneyAmount();
    try
    {
        bank.RetrieveAccount(id)?.MakeDeposit(amount);
    }
    catch
    {
        DisplayText.Failure();
        return;
    }
    DisplayText.Success();
}

void Withdraw()
{
    if (!GetAccountId(out Guid id))
    {
        DisplayText.InvalidAccountId();
        return;
    }

    decimal amount = GetMoneyAmount();
    try
    {
        decimal currBalance = bank.RetrieveAccount(id).CheckBalance();
        if (amount > currBalance)
        {
            DisplayText.Failure();
            return;
        }

        bank.RetrieveAccount(id).Withdraw(amount);
    }
    catch
    {
        DisplayText.Failure();
        return;
    }
    DisplayText.Success();
}

void CheckBalance()
{
    if (!GetAccountId(out Guid id))
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
        return;
    }
}

void TransferFunds()
{
    Console.WriteLine("TRANSFER FROM");
    if (!GetAccountId(out Guid id1))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    Console.WriteLine("TRANSFER TO");
    if (!GetAccountId(out Guid id2))
    {
        DisplayText.InvalidAccountId();
        return;
    }
    decimal amount = GetMoneyAmount();
    try
    {
        decimal currBalance = bank.RetrieveAccount(id1).CheckBalance();
        if (amount > currBalance)
        {
            DisplayText.Failure();
            return;
        }

        bank.RetrieveAccount(id1).Withdraw(amount);
        bank.RetrieveAccount(id2).MakeDeposit(amount);
    }
    catch
    {
        DisplayText.Failure();
        return;
    }
    DisplayText.Success();
}

string GetName(string firstOrLast)
{
    DisplayText.EnterName(firstOrLast);
    String name = Console.ReadLine();
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
    string? input = Console.ReadLine();
    while (!Decimal.TryParse(input, out amount) || amount < 0)
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
    string? input = Console.ReadLine();
    return Guid.TryParse(input, out id);
}

string GetMenuSelection()
{
    DisplayText.SelectFromMenu();
    string? input = Console.ReadLine();
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
    string? input = Console.ReadLine();
    while (!Decimal.TryParse(input, out decimal amount) || amount <= 0)
    {
        DisplayText.InvalidAmount();
        DisplayText.EnterDollarAmount();
        input = Console.ReadLine();
    }
    return Decimal.Parse(input);
}