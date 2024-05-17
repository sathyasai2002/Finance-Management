using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Exceptions
{
    internal class ExpenseNotFoundException:ApplicationException
    {
        public ExpenseNotFoundException() { }
        public ExpenseNotFoundException(string message) : base(message) { }
    }
}
