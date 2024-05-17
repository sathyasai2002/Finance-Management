using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Models
{
    public class ExpenseCategories
    {
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ExpenseCategories(int? categoryID, string categoryname)
        {
            CategoryID = categoryID;
            CategoryName = categoryname;
        }
        public ExpenseCategories()
        {

        }

        public override string ToString()
        {
            return $"{CategoryID} {CategoryName}";
        }
    }
}
