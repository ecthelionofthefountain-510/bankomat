using System.Collections.Generic;

namespace banko;

public class Bankomat {

    bool cardInserted = false; 
    bool isAuthenticated = false;
    Card card;
    int amount;
    int machineBalance = 11000;
    private int failedPinAttempts = 0;
    List<string> msgs = new List<string>();

    public int GetMachineBalance()
    {
        return machineBalance;
    }

    public void AddToMachineBalance(int amount)
    {
        if (amount > 0)
        {
            machineBalance += amount;
        }
    }

    public string getMessage(){
        var msg = "";
        if(msgs.Count > 0){
            // return from shift left:
            msg = msgs[0]; // pick
            msgs.RemoveAt(0); // clear
        }
        return msg; // return 
    }

    // void != deterministic

    public bool insertCard(Card card){
        cardInserted = true;
        this.card = card;
        msgs.Add("Card inserted");
        return cardInserted;
    }

    public void ejectCard(){
        cardInserted = false;
        msgs.Add("Card removed, don't forget it!");
    }

    public bool enterPin(string pin){
        if(card.pin == pin)
        {
            failedPinAttempts = 0;
            isAuthenticated = true;
            msgs.Add("Correct pin");
            return true;            
        }else{
            failedPinAttempts++;
            isAuthenticated = false;
            if (failedPinAttempts >= 3)
            {
                cardInserted = false;
                msgs.Add("Too many incorrect pin attempts. Card swallowed");
            }
            else
            {
                msgs.Add("Incorrect pin");
            }
            return false;
        }
    }

    public bool withdraw(int amount){
        if(pin == "0123" &&  amount > 0 && amount <= machineBalance && amount <= card.account.getBalance()){
            machineBalance -= amount;
            card.account.withdraw(amount);
            msgs.Add("Withdrawing " + amount);
            return true;
        }else{
            if(amount > machineBalance){
                msgs.Add("Machine has insufficient funds"); 
            }else if(amount > card.account.getBalance()){
                msgs.Add("Card has insufficient funds");
            }
            else
            {
                msgs.Add("You can not withdraw 0 or less money");
            }                
            return false;
        }
    }

    public bool isCardInserted()
    {
        return cardInserted;
    }

}

