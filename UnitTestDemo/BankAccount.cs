using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo
{
    public class BankAccount
    {
        private readonly string _customerName;
        private decimal _balance;

        public BankAccount(string customerName, decimal balance)
        {
            _customerName = customerName;
            _balance = balance;
        }

        public BankAccount(IBankService service, string customerName)
        {
            _customerName = customerName;
            _balance = service.GetBalance(customerName);
        }

        public decimal Balance
        {
            get { return _balance; }
        }

        public void Debit(decimal amount)
        {
            if(amount > _balance)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if(amount < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if(amount > _balance + 1000)
            {
                throw new ArgumentOutOfRangeException("erreur 2");
            }
            _balance -= amount;
        }

        public void Credit(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            _balance += amount;
        }
    }
}
