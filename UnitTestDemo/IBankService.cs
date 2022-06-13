using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo
{
    public interface IBankService
    {
        decimal GetBalance(string customerName);
        Dictionary<string, decimal> GetAllAccount();
    }
}
