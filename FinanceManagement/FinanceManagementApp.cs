using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.Services;
using FinanceManagement.Repository;

namespace FinanceManagement
{
    internal class FinanceManagementApp
    {
        UserServices userServices = new UserServices();
        Boolean Loop=false;
        public void MainMenu() 
        {
            while (!Loop)
            {
                Console.WriteLine("Welcome To Finance Mnager App");
                Console.WriteLine("1. Login ");
                Console.WriteLine("2. Register ");
                Console.WriteLine("3. Exit ");
                int value = Convert.ToInt32(Console.ReadLine());
                
                switch(value)
                {
                    case 1:
                        Console.WriteLine("Enter Id: ");
                        int userId= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Password: ");
                        String password = Convert.ToString(Console.ReadLine());
                        Boolean Loop2 = false;

                        while (!Loop2)
                        {
                            IFinanceRepository financeManagement = new FinanceRepositoryImpl();
                            String name = financeManagement.VerifyUser(userId, password);
                            Console.WriteLine("Select an Option: ");
                            Console.WriteLine("1. Create Expense: ");
                            Console.WriteLine("2. Delete User: ");
                            Console.WriteLine("3. Delete Expense: ");
                            Console.WriteLine("4. Get All Expense: ");
                            Console.WriteLine("5. Update Expense: ");
                            Console.WriteLine("6. Generate Report: ");
                            Console.WriteLine("7. Exit: ");
                            int Option = Convert.ToInt32(Console.ReadLine());

                        

                            switch (Option)
                            {
                                case 1:
                                    userServices.CreateExpense(userId);
                                    break;
                                case 2:
                                    userServices.DeleteUser(userId);
                                    Loop2 = true;
                                    break;
                                case 3:
                                    userServices.DeleteExpense();
                                    break;
                                case 4:
                                    userServices.GetAllExpenses(userId);
                                    break;
                                case 5:
                                    userServices.UpdateExpense(userId);
                                    break;
                                case 6:
                                    userServices.GenerateReport(userId);
                                    break;
                                case 7:
                                    Loop2= true;
                                    break;
                            }
                        }
                        break;
                                              
                        

                    case 2:
                        userServices.CreateUsers();
                        break;

                    case 3:
                        Console.WriteLine("Exiting Application....");
                        Loop = true;
                        break;
                        

                }
            }

        }

    }
}
