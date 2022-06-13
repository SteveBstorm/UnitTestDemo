using Apps72.Dev.Data.DbMocker;
using UnitTestDemo;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Debit_WithValidBalance()
        {
            BankAccount b = new BankAccount("steve", 1000);

            b.Debit(200);
            Assert.AreEqual(800, b.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Credit_WithInvalidAmount()
        {
            BankAccount b = new BankAccount("steve", 1000);
            b.Credit(-500);

            Assert.AreEqual(1000, b.Balance);
        }

        [TestMethod]
        public void Credit_WithValidAmount()
        {
            BankAccount b = new BankAccount("steve", 1000);
            b.Credit(500);

            Assert.AreEqual(1500, b.Balance);
        }

        [TestMethod]
        public void Debit_Service()
        {
            IBankService service = new MyTestBankService();
            BankAccount a = new BankAccount(service, "steve");

            a.Debit(200);

            Assert.AreEqual(800, a.Balance);
        }

        public class MyTestBankService : IBankService
        {
            public Dictionary<string, decimal> GetAllAccount()
            {
                throw new NotImplementedException();
            }

            public decimal GetBalance(string customerName)
            {
                return 1000;
            }
        }

        [TestMethod]
        public void Debit_WithMoq()
        {
            var service = new Moq.Mock<IBankService>();
            service.Setup(srv => srv.GetBalance("steve")).Returns(1000);

            BankAccount b = new BankAccount(service.Object, "steve");
            b.Debit(200);
            Assert.AreEqual(800, b.Balance);
        }

        [TestMethod]
        public void GetAll_WithMoq()
        {
            var service = new Moq.Mock<IBankService>();
            service.Setup(srv => srv.GetAllAccount()).Returns(new Dictionary<string, decimal>());

            BankAccount b = new BankAccount(service.Object, "steve");

            Assert.IsInstanceOfType(service.Object.GetAllAccount(), typeof(Dictionary<string, decimal>));
        }


        [TestMethod]
        public void Debit_withDBMOCKER()
        {
            var conn = new MockDbConnection();
            conn.Mocks.When(c => c.HasValidSqlServerCommandText())
                .ReturnsTable(MockTable.WithColumns("Balance", "cname").AddRow(1000m, "steve"));

            var service = new BankService(conn);
            BankAccount b = new BankAccount(service, "steve");

            b.Debit(100);
            Assert.AreEqual(900, b.Balance);
            Assert.IsTrue(b.Balance.ToString().Length < 4);
        }
    }
}