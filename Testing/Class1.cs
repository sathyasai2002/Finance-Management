using FinanceManagement.Repository;
using FinanceManagement.Models;
using NUnit.Framework;
namespace Testing
{
    public class Class1
    {
        FinanceRepositoryImpl financeRepository;

        public Class1()
        {
            financeRepository = new FinanceRepositoryImpl();
        }
        [Test]
        public void CreatedExpenseSuccessfully()
        {

            var Expenses = new Expenses();
            var userId = 1;
            var expense = new Expenses
            {
                UserID = userId,
                Amount = 200,
                CategoryID = 1,
                Date = DateTime.Now,
                Description = "Test expense"
            };

        }
    }
}

