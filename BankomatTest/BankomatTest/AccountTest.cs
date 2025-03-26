using banko;
using Microsoft.VisualBasic;

namespace BankomatTest;

public class AccountTest
{
    [Fact]
    public void Withdraw_ShouldReturnZero_IfNotEnoughFunds()
    {
        // setup
        Account account = new(); 
        int amountWithdrawn = account.withdraw(6000); // försöker ta ut 6000 kr
        
        // test
        Assert.Equal(0, amountWithdrawn); // 0 kr drogs eftersom det inte fanns tillräckligt med pengar
        Assert.Equal(5000, account.getBalance()); // 5000 kr kvar på kontot
    }

    [Fact]
    public void Withdraw_ShouldReturnZero_IfInsufficientFunds()
    {
        // setup
        Account account = new();
        int amountWithdrawn = account.withdraw(-1000); // försöker ta ut -1000 kr. det går ju inte ...
        
        // test
        Assert.Equal(0, amountWithdrawn); // uttaget misslyckas och 0 kr dras
        Assert.Equal(5000, account.getBalance()); // saldot blir oförändrat
    }

    [Fact]
    public void ShowBalance()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("0123");
        
        // test
        Assert.Equal(5000, account.showBalance());
    }
}