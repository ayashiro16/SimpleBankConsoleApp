// See https://aka.ms/new-console-template for more information

using SimpleBankConsoleApp.Controllers;

BankController bank;
Start();

void Start()
{
    bank = new BankController();
    Console.WriteLine("---------------------");
    Console.WriteLine("-----Simple Bank-----");
    Console.WriteLine("---------------------");
    Console.WriteLine("------Main Menu------");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Make a deposit");
    Console.WriteLine("3. Withdraw");
    Console.WriteLine("4. Check Balance");
    Console.WriteLine("5. Transfer Funds");
    Console.WriteLine("9. Exit Application");
    Console.Write("Please make a numerical selection: ");
}

void OpenDialogue()
{
    
}

void ProcessUserRequest()
{
    
}