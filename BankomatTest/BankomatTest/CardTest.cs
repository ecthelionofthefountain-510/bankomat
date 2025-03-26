using banko;

namespace BankomatTest;

public class CardTest
{
    [Fact]
    public void Card_ShouldHaveDefaultPin()
    {
        // setup
        Account account = new();
        Card card = new(account);
        
        // test
        Assert.Equal("0123", card.pin); // testar att pinkoden är 0123
    }

    [Fact]
    public void Card_ShouldHaveAccount()
    {
        // setup
        Account account = new();
        Card card = new(account); // skapar ett kort med ett konto
        
        // test
        Assert.Equal(account, card.account); // testar att kortet har rätt koppling till kontot
    }
}