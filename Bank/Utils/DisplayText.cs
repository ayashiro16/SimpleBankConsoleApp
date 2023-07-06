namespace SimpleBankConsoleApp.Utils;

public static class DisplayText
{
    public static void MainMenu()
    {
        Console.WriteLine("---------------------");
        Console.WriteLine("-----Simple Bank-----");
        Console.WriteLine("---------------------");
        Console.WriteLine("------Main Menu------");
        Console.WriteLine("1. Create Account");
        Console.WriteLine("2. Make a deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Check Balance");
        Console.WriteLine("5. Transfer Funds");
        Console.WriteLine("6. Exit Application");
    }

    public static void SelectFromMenu()
    {
        Console.Write("Please make a numerical selection: ");
    }

    public static void EnterName(string firstOrLast)
    {
        Console.Write($"Please enter your {firstOrLast} name: ");
    }

    public static void EnterDollarAmount()
    {
        Console.Write("Please enter a dollar amount: ");
    }

    public static void InvalidName()
    {
        Console.WriteLine("Invalid name. Letters only, please.");
    }

    public static void StartingBalance()
    {
        Console.Write("Starting balance: ");
    }

    public static void AccountCreated(Guid id)
    {
        Console.WriteLine($"Your account has been created. \n Your ID is {id}");
    }

    public static void Balance(decimal balance)
    {
        Console.WriteLine($"Your current balance is {Math.Truncate((balance * 100) / 100)}");
    }

    public static void InvalidAmount()
    {
        Console.WriteLine("Invalid amount.");
    }

    public static void InvalidMenuChoice()
    {
        Console.WriteLine("Invalid menu selection.");
    }

    public static void EnterAccountId()
    {
        Console.WriteLine("Please enter your ID: ");
    }

    public static void InvalidAccountId()
    {
        Console.WriteLine("Could not find account.");
    }

    public static void Failure()
    {
        Console.WriteLine("Could not complete task.");
    }

    public static void Success()
    {
        Console.WriteLine("Task successfully completed.");
    }

    public static void Exit()
    {
        Console.WriteLine("Thank you for using Simple Bank!");
    }
}  