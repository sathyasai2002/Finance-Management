using FinanceManagement.Models;
using FinanceManagement.Repository;
using FinanceManagement.Services;
namespace FinanceManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            FinanceManagementApp financeManagementApp = new FinanceManagementApp();
            financeManagementApp.MainMenu();

            

        }
    }
}
