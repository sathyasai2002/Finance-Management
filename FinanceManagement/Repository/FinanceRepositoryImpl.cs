using System;
using FinanceManagement.Models;
using FinanceManagement.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.Utility;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace FinanceManagement.Repository
{
    public class FinanceRepositoryImpl : IFinanceRepository
    {

        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public FinanceRepositoryImpl() 
        {
            cmd = new SqlCommand();
            sqlConnection = new SqlConnection(DbUtil.GetConnectionString());
        }



        public String VerifyUser(int userId,String password)
        {
            try
            {

                String name = "";
                SqlDataReader reader = null;
                sqlConnection.Open();
                cmd.CommandText = "select username from Users where user_id= @userId and password= @password;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Connection = sqlConnection;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    name = reader["username"].ToString();
                }
                Console.WriteLine($"welcome: {name}");
                sqlConnection.Close();
                return name;
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine("Invalid User"+ex.Message);

            }
            catch(Exception ex)
            {
                Console.WriteLine("wrong Info"+ex.Message);
            }
            return null;
        }
        public int CreateUser(Users user)
        {
            try
            {
                int User = 0;
                sqlConnection.Open();
                cmd.CommandText = "INSERT INTO users OUTPUT INSERTED.user_id VALUES (@UsersName, @Email,@Password)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UsersName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Connection = sqlConnection;
                User=Convert.ToInt32(cmd.ExecuteScalar());
                sqlConnection.Close();
                
                return User;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("A user with the same password already exists.");
                }
                else
                {
                    throw;
                }
            }
        }
        public Boolean DeleteUser(int UserID) 
        {
            sqlConnection.Open();
            cmd.CommandText= "DELETE FROM Users WHERE user_id = @UserId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserId", UserID);
            cmd.Connection = sqlConnection;
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return rowsAffected > 0;
        }

        public Boolean CreateExpense(Expenses expenses) 
        {
                try
                {
                    sqlConnection.Open();
                    cmd.CommandText = "INSERT INTO Expenses (user_id, category_id, amount,Date, description) " +
                       "VALUES (@Userid, @categoryId, @amount,@Date, @description)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Userid", expenses.UserID);
                    cmd.Parameters.AddWithValue("@Amount", expenses.Amount);
                    cmd.Parameters.AddWithValue("@categoryID", expenses.CategoryID);
                    cmd.Parameters.AddWithValue("@Date", expenses.Date);
                    cmd.Parameters.AddWithValue("@Description", expenses.Description);

                    cmd.Connection = sqlConnection;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating Expense: " + ex.Message);
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                }
            

        }

        public Boolean DeleteExpense(int ExpenseID)
        {
            try
            {

                sqlConnection.Open();
                cmd.CommandText = "DELETE FROM EXPENSES WHERE expense_id=@expenseId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@expenseId", ExpenseID);
                cmd.Connection = sqlConnection;
                int rowsAffected = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return rowsAffected > 0;
                
            }
            catch (ExpenseNotFoundException ex)
            {
                Console.WriteLine("Expense Id Not Found"+ex.Message);
                return false;

            }
            

        }
        public List<Expenses> GetAllExpenses(int UserID) 
        {
            List<Expenses> expenses = new List<Expenses>();
            sqlConnection.Open();
            cmd.CommandText = "SELECT * FROM Expenses WHERE User_Id = @UserId";
            cmd.Parameters.Clear();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserId", UserID);
            cmd.Connection = sqlConnection;
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) 
            {
                Expenses expense = new Expenses
                {
                    ExpenseID = Convert.ToInt32(reader["expense_id"]),
                    UserID = Convert.ToInt32(reader["User_id"]),
                    Amount= Convert.ToDecimal(reader["amount"]),
                    CategoryID = Convert.ToInt32(reader["Category_id"]),
                    Date = Convert.ToDateTime(reader["date"]),
                    Description = Convert.ToString(reader["description"])

                };
                expenses.Add(expense);
            }
            //reader.Close();
            sqlConnection.Close();
            return expenses;
        }
        
        public Boolean UpdateExpenses( Expenses expenses) 
        {
            sqlConnection.Open();
            cmd.CommandText = "UPDATE Expenses SET amount =@amount,date = @date,category_Id=@categoryId,Description=@description WHERE User_id=@usersId and expense_id=@expenseId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@amount", expenses.Amount);
            cmd.Parameters.AddWithValue("@date",expenses.Date);
            cmd.Parameters.AddWithValue("@categoryId", expenses.CategoryID);
            cmd.Parameters.AddWithValue("@description", expenses.Description);
            cmd.Parameters.AddWithValue("@usersId", expenses.UserID);
            cmd.Parameters.AddWithValue("@expenseId",expenses.ExpenseID);
            cmd.Connection = sqlConnection;
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return rowsAffected > 0;

        }

        public void GenerateReport(int UserID) 
        {
            sqlConnection.Open();
            cmd.CommandText = "SELECT ec.category_name,SUM(e.amount) AS total_amount,STRING_AGG(CONCAT(' Date: ', FORMAT(e.date, 'yyyy-MM-dd'),', Description: ', e.description),'; ') AS expense_details FROM Expenses e JOIN ExpenseCategories ec ON e.category_id = ec.category_id WHERE e.user_id = @ID GROUP BY ec.category_name";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", UserID);
            cmd.Connection = sqlConnection;
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("-----------------------------------");
            while (reader.Read())
            {
                string categoryName = reader["category_name"].ToString();
                decimal totalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount"));
                string expenseDetails = reader["expense_details"].ToString();

                Console.WriteLine($"Category: {categoryName}");
                Console.WriteLine($"Total Amount: {totalAmount}");
                Console.WriteLine($"Expense Details: {expenseDetails}");
            }
            Console.WriteLine("-----------------------------------");

            reader.Close();
            sqlConnection.Close ();

        }


    }
}




