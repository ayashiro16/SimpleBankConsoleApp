// See https://aka.ms/new-console-template for more information
using BankView = SimpleBankConsoleApp.Views.BankView;
using DisplayText = SimpleBankConsoleApp.Utils.DisplayText;

BankView bank;
Action[] menuActions;
Start();

void Start()
{
    bank = new();
    menuActions = new[] { bank.CreateAccount, bank.Deposit, bank.Withdraw, bank.CheckBalance, bank.TransferFunds, Exit };
    MainMenu();
}

void MainMenu()
{
    while (true)
    {
        DisplayText.MainMenu();
        var selection = GetMenuSelection();
        if (selection > menuActions.Length || selection <=0)
        {
            DisplayText.InvalidMenuChoice();
            continue;
        }
        menuActions[selection - 1]();
        if (selection == 6)
        {
            break;
        }
    }
}

int GetMenuSelection()
{
    while (true)
    {
        DisplayText.SelectFromMenu();
        var input = Console.ReadLine();
        if (int.TryParse(input, out var selection))
        {
            return selection;
        }
        DisplayText.InvalidMenuChoice();
    }
}

void Exit()
{
    DisplayText.Exit();
}