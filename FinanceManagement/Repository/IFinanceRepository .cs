using FinanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Repository
{
    public interface IFinanceRepository
    {
       public String VerifyUser(int UserId, String password); 
       public int CreateUser(Users user);
       public Boolean CreateExpense(Expenses expense);
       public Boolean DeleteUser(int UserID);
       public Boolean DeleteExpense(int ExpenseID);
       public List<Expenses> GetAllExpenses(int UserID);
       public Boolean UpdateExpenses(Expenses expenses);
       public void GenerateReport(int UserID);


    }
}
