using banko;
namespace BankomatTest;

public class BankomatTest
{
    [Fact]
    public void TestInsertCard()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        
        // test
        bool result = bankomat.insertCard(card);
        Assert.True(result);
    }

    [Fact]
    public void TestEnterPin()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        
        // clear messages
        bankomat.getMessage();
        
        // test correct pin
        bool result = bankomat.enterPin("0123");
        Assert.True(result);
        
        // test incorrect pin
        result = bankomat.enterPin("1234");
        Assert.False(result);

    }
    
    [Fact]
    public void TestWithdraw5000Success()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("0123");
        
        // clear messages
        bankomat.getMessage();
        bankomat.getMessage();
        
        // test
        bool result = bankomat.withdraw(5000);
        Assert.True(result);
        Assert.Equal("Withdrawing 5000", bankomat.getMessage());
    }
    
    [Fact]
    public void TestEjectCard()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        
        // clear messages
        bankomat.getMessage();
        
        // test
        bankomat.ejectCard();
        Assert.Equal("Card removed, don't forget it!", bankomat.getMessage());
    }
    
    [Fact]
    public void TestAddToMachineBalance()
    {
        // setup
        Bankomat bankomat = new();
        
        // test
        int balance = bankomat.GetMachineBalance();
        Assert.Equal(11000, balance);
    }

    [Theory]
    [InlineData(11000, 5000, 16000)]
    [InlineData(11000, 1000, 12000)]
    [InlineData(11000, 0, 11000)]
    [InlineData(11000, -1000, 11000)]
    public void TheoryTestGetMachineBalance(int initialBalance, int amount, int expectedBalance)
    {
        // setup
        Bankomat bankomat = new();
        // test
        bankomat.AddToMachineBalance(amount);
        int actualBalance = bankomat.GetMachineBalance();
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void TestWithdrawFailsWhenMachineHasInsufficientFunds()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("0123");
        bankomat.withdraw(10000); // finns bara 1000kr kvar nu
        
        // clear messages
        bankomat.getMessage();
        bankomat.getMessage();
        bankomat.getMessage();

        // test
        bool result = bankomat.withdraw(7000); 
        Assert.False(result);
        Assert.Equal("Machine has insufficient funds", bankomat.getMessage());
    }

    [Fact]
    public void TestWithdrawFailsWhenAccountHasInsufficientFunds()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("0123");
        bankomat.withdraw(5000); // 0 kr kvar på kontot nu
        
        // clear messages
        bankomat.getMessage();
        bankomat.getMessage();
        bankomat.getMessage();
        
        // test
        bool result = bankomat.withdraw(6000);
        Assert.False(result);
        Assert.Equal("Card has insufficient funds", bankomat.getMessage());
        
    }

    [Fact]
    public void SwallowCardWhenPinIsIncorrectMultipleTimes()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("1234");
        bankomat.enterPin("2345");
        bankomat.enterPin("3456");

        bankomat.getMessage();
        bankomat.getMessage();
        bankomat.getMessage();
        
        // test
        Assert.False(bankomat.isCardInserted());
        Assert.Equal("Too many incorrect pin attempts. Card swallowed", bankomat.getMessage());
    }

    [Fact]
    public void TestMachineSafety()
    {
        // setup
        Bankomat bankomat = new();
        Account account = new();
        Card card = new(account);
        bankomat.insertCard(card);
        bankomat.enterPin("1234");
        
        // test
        bool result = bankomat.withdraw(5000);
        Assert.True(result);
    }
}

/*  Ett enda långt scenario-test:
 
 [Fact]
public void ScenarioTest_BankomatFullFlow()
{
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);

    // 1. Sätt in kort
    bankomat.insertCard(card);
    Assert.Equal("Card inserted", bankomat.getMessage());

    // 2. Fel pinkod
    var pinResult = bankomat.enterPin("1234");
    Assert.False(pinResult);
    Assert.Equal("Incorrect pin", bankomat.getMessage());

    // 3. Rätt pinkod
    pinResult = bankomat.enterPin("0123");
    Assert.True(pinResult);
    Assert.Equal("Correct pin", bankomat.getMessage());

    // 4. Uttag 5000 (ska lyckas)
    var withdrawResult = bankomat.withdraw(5000);
    Assert.True(withdrawResult);
    Assert.Equal("Withdrawing 5000", bankomat.getMessage());

    // 5. Mata ut kort
    bankomat.ejectCard();
    Assert.Equal("Card removed, don't forget it!", bankomat.getMessage());

    // 6. Sätt in samma kort igen
    bankomat.insertCard(card);
    Assert.Equal("Card inserted", bankomat.getMessage());

    // 7. Rätt pinkod igen
    pinResult = bankomat.enterPin("0123");
    Assert.True(pinResult);
    Assert.Equal("Correct pin", bankomat.getMessage());

    // 8. Uttag 7000 (bankomaten har bara 6000 kvar, så det ska misslyckas)
    withdrawResult = bankomat.withdraw(7000);
    Assert.False(withdrawResult);
    Assert.Equal("Machine has insufficient funds", bankomat.getMessage());

    // 9. Uttag 6000 (bankomaten har pengar, men kontot har 0)
    withdrawResult = bankomat.withdraw(6000);
    Assert.False(withdrawResult);
    Assert.Equal("Card has insufficient funds", bankomat.getMessage());
}*/