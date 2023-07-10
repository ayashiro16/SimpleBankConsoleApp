namespace SimpleBankConsoleApp.Interfaces;

public interface IBankView
{
    public void CreateAccount();
    public void Deposit();
    public void Withdraw();
    public void CheckBalance();
    public void TransferFunds();
}