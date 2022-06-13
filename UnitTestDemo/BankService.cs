using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo
{
    public class BankService : IBankService
    {
        private Dictionary<string, decimal> AccountInfo;

        public BankService()
        {
            AccountInfo = new Dictionary<string, decimal>();
            AccountInfo.Add("steve", 1000);
            AccountInfo.Add("marcel", 2000);
        }

        private DbConnection _connection;

        public BankService(DbConnection connection)
        {
            _connection = connection;
        }

        public decimal GetBalance(string customerName)
        {
            
                using (DbCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Balance FROM Account WHERE cname = @cname";
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = "cname";
                    param.Value = customerName;

                    return (decimal)cmd.ExecuteScalar();
                }
           
            //return AccountInfo[customerName];
        }

        public Dictionary<string, decimal> GetAllAccount()
        {
            return AccountInfo;
        }
    }
}
