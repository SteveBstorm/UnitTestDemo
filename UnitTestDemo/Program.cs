// See https://aka.ms/new-console-template for more information

using UnitTestDemo;

BankAccount compte1 = new BankAccount("Steve", 1000);

compte1.Credit(200);
Console.WriteLine(compte1.Balance);

//compte1.Debit(1500);
//compte1.Credit(-150);
