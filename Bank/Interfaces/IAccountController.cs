namespace SimpleBankConsoleApp.Interfaces;

public interface IAccountController
{
    decimal CheckBalance();
    decimal Deposit(decimal amount);
    bool TryWithdraw(decimal amount);
    bool TryWithdraw(decimal amount, out decimal balance);
}