namespace SimpleBankConsoleApp.Interfaces;

public interface IBankController
{
    Guid CreateAccount(string first, string last, decimal balance);
    IAccountController RetrieveAccount(Guid id);
}