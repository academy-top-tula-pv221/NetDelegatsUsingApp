using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDelegatsUsingApp
{
    
    internal class AccountEventArgs
    {
        public string Message { get; set; }
        public int Amount { get; set; }
        public int TotalAmount { get; set; }

        public AccountEventArgs(string message, int amount, int totalAmount)
        {
            Message = message;
            Amount = amount;
            TotalAmount = totalAmount;
        }
    }

    internal class Account
    {
        int amount;

        public string Name { set; get; }

        public delegate void AccountHandler(Account sender, AccountEventArgs args);

        public event AccountHandler? Notify;

        /*
        public event AccountHandler? Notify
        {
            add
            {
                notify += value;
                Console.WriteLine($"add method: {value?.Method.Name}");
            }
            remove
            {
                notify -= value;
                Console.WriteLine($"remove method: {value?.Method.Name}");
            }
        }
        */        

        //public AccountHandler Handler { set; private get; }

        //AccountHandler? handler;

        //public void AddHandler(AccountHandler handler)
        //{
        //    this.handler += handler;
        //}

        //public void RemoveHandler(AccountHandler handler)
        //{
        //    this.handler -= handler;
        //}

        public Account(int amount)
        {
            this.amount = amount;
        }
        public void Put(int amount)
        {
            this.amount += amount;
            //handler?.Invoke($"At account add amount {amount}, total accaunt amount = {this.amount}");
            //notify?.Invoke($"At account add amount {amount}, total accaunt amount = {this.amount}");
            Notify?.Invoke(this, new("Amount put", amount, this.amount));
        }
        public void Take(int amount)
        {
            if (this.amount >= amount)
            {
                this.amount -= amount;
                //notify?.Invoke($"At account takes amount {amount}, total accaunt amount = {this.amount}");
                //handler?.Invoke($"At account takes amount {amount}, total accaunt amount = {this.amount}");
                Notify?.Invoke(this, new("Amount Take", amount, this.amount));
            }
            else
                //notify?.Invoke($"Total amount {this.amount}");
                Notify?.Invoke(this, new("Error take", amount, this.amount));

        }
    }
}
