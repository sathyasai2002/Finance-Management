using FinanceManagement.Models;
using FinanceManagement.Exceptions;
using FinanceManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Services
{
    internal class UserServices
    {
        IFinanceRepository financeRepository;
        public UserServices()
        {
            financeRepository=new FinanceRepositoryImpl();
        }
       
        public void CreateUsers()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            try
            {

                Users newUser = new Users
                {
                    UserName = username,
                    Password = password,
                    Email = email
                };

                
                int User= financeRepository.CreateUser(newUser);

                if (User > 0)
                {
                    Console.WriteLine($"User created successfully, your userId :{User}");
                }
                else
                {
                    Console.WriteLine("Failed to create user. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void CreateExpense(int id)
        {
            Console.WriteLine("Enter Amount: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" 1. food \n 2. Transportation \n 3. Utilities \n 4. HealthCare \n 5. Education \n 6. Rent \n 7. Miscellaneous");
            Console.WriteLine("Enter Category Id:");
            int CategoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Date: ");
            DateTime date= Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Description: ");
            String description = Console.ReadLine();

            Expenses expenses = new Expenses
            {
                UserID = id,
                Amount = amount,
                CategoryID = CategoryId,
                Date = date,
                Description = description
            };

            
            Boolean expense=financeRepository.CreateExpense(expenses);
            Expenses expenses1 = new Expenses();
            

            if (expense)
            {
                Console.WriteLine("Your Expense is created successfully ");
            }
        }

        public void DeleteUser(int id) 
        { 
            
            Boolean result=financeRepository.DeleteUser(id);
            if (result)
            {
                Console.WriteLine("Deleted User Successfully");
            }
        }

        public void DeleteExpense()
        {
            try
            {

    
            Console.WriteLine("Enter the expense id which you want to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Boolean result=financeRepository.DeleteExpense(id);
            if (result)
            {
                Console.WriteLine("Expense Deleted Successfully ");

            }
            }
            catch (ExpenseNotFoundException ex)
            {
                Console.WriteLine("Expense Id Not Found" + ex.Message);
                

            }


        }
        public void GetAllExpenses(int id)
        {

            
            List<Expenses> expenses = new List<Expenses> ();
            expenses=financeRepository.GetAllExpenses(id);
            foreach(Expenses expense in expenses)
            {
                Console.WriteLine($"ExpenseId:{expense.ExpenseID} userId:{ expense.UserID} Amount:{ expense.Amount} CategoryId:{expense.CategoryID} Date:{ expense.Date} Description:{ expense.Description}");
                
                

            }
            
        }

        public void UpdateExpense(int id)
        {
            Console.WriteLine("Enter Expense ID: ");
            int expenseId=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Amount to Update: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" 1. food \n 2. Transportation \n 3. Utilities \n 4. HealthCare \n 5. Education \n 6. Rent \n 7. Miscellaneous");
            Console.WriteLine("Enter Category Id to Update: ");
            int categoryId=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter date to update: ");
            DateTime date=Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Descriptioin to update: ");
            String description=Convert.ToString(Console.ReadLine());

            Expenses expenses = new Expenses
            {
                UserID = id,
                ExpenseID = expenseId,
                Amount = amount,
                CategoryID = categoryId,
                Date = date,
                Description=description
            };

            
            Boolean result = financeRepository.UpdateExpenses(expenses);
            if (result)
            {
                Console.WriteLine("Expense Updated Successfully ");
            }
        }
        public void GenerateReport(int id)
        {
            
            financeRepository.GenerateReport(id);

        }
    }
}
