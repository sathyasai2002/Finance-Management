using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Models
{
    public class Expenses
    {
        public int? ExpenseID { get; set; }
        public int UserID {  get; set; }
        public decimal Amount {  get; set; }
        public int CategoryID {  get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }

        public Expenses(int? expensesID,int userID,decimal amount,int categoryID,DateTime date,string description) 
        {
            ExpenseID = expensesID;
            UserID = userID;
            Amount = amount;
            CategoryID = categoryID;
            Date = date;
            Description = description;

        }
        public Expenses()
        {
            
        }
        public override string ToString()
        {
            return $"{ExpenseID},{UserID},{Amount},{CategoryID},{Date},{Description}";
        }
    }
}
