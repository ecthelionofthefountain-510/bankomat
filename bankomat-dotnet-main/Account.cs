using System.Collections.Generic;

namespace banko;

public class Account{
    int balance = 5000;
    List<string> msgs = new List<string>();
    
    public int withdraw(int amount){
        if(amount > 0 && balance >= amount){
            balance -= amount;
            return amount;
        }else{
            return 0;
        }
    }

    public int getBalance(){
        return balance;
    }

    public int showBalance()
    {
        return balance;
    }

}