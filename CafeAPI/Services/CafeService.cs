using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using CafeAPI.Models;



namespace CafeAPI.Services
{
    public class CafeService
    {
        string connectionString = "Data Source=DESKTOP-RPM5H23\\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True;";
        public Cafe GetCafe(int cafeId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                string sqlStmt = String.Format("Select CafeId, CuisineId,FoodId,CustomerName,FoodName,CuisineType,Price,Status  from Cafe Where cafeId ={0}", cafeId);
                //string sqlStmt1 = "Select StudentId, Name, Gender, DOB from Student Where StudentId = " + studentId;
                //string sqlStmt2 = $"Select StudentId, Name, Gender, DOB from Student Where StudentId = {studentId}";
                Console.WriteLine(sqlStmt);
                //Console.WriteLine(sqlStmt1);
                //Console.WriteLine(sqlStmt2);

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cafe c = new Cafe();
                            c.CafeId = reader.GetInt32(0);
                            c.CuisineId=reader.GetInt32(1);
                            c.FoodId=reader.GetInt32(2);
                            c.CustomerName=reader.GetString(3);
                            c.FoodName=reader.GetString(4);
                            c.CuisineType=reader.GetString(5);
                            c.Price=reader.GetDecimal(6);
                            c.Status=reader.GetString(7);

                            // Console.WriteLine("before new");
                            // Student s = new Student(reader.GetInt32(0),
                            //                 reader.GetString(1),
                            //                 reader.GetChar(2),
                            //                 reader.GetDateTime(3));

                            Console.WriteLine(c.CustomerName);
                            return c;
                        }
                    }
                }
                return null;
            }
        }

        public List<Cafe> GetAllcafes()
        {
            List<Cafe> cafes = new List<Cafe>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string sqlStmt = String.Format("Select CafeId,CuisineId,FoodId,CustomerName,FoodName,CuisineType,Price,Status from Cafe");

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cafe c= new Cafe();
                            c.CafeId = reader.GetInt32(0);
                            c.CuisineId=reader.GetInt32(1);
                            c.FoodId=reader.GetInt32(2);
                            c.CustomerName=reader.GetString(3);
                            c.FoodName=reader.GetString(4);
                            c.CuisineType=reader.GetString(5);
                            c.Price=reader.GetDecimal(6);
                            c.Status=reader.GetString(7);
                            cafes.Add(c);
                        }
                    }
                }
                return cafes;
            }
        }

        public int InsertCafe(Cafe newcafe)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                // string sqlStmt = String.Format(
                // "INSERT INTO [dbo].[Student] ([StudentId],[Name],[DOB],[Gender], [City], [State]) VALUES ({0},'{1}','{2}', '{3}', '{4}', '{5}')",
                //  newStudent.StudentId, newStudent.Name, newStudent.DOB, newStudent.Gender,
                //  newStudent.City, newStudent.State);

                //string sqlStmt = $"INSERT INTO [dbo].[Student] ([StudentId],[Name],[DOB],[Gender], [City], [State]) VALUES ({newStudent.StudentId},'{newStudent.Name}','{newStudent.DOB}', '{newStudent.Gender}', '{newStudent.City}', '{newStudent.State}')";

                string sqlStmt = $"INSERT INTO [dbo].[cafe] ([CuisineId],[FoodId],[CustomerName],[FoodName],[CuisineType],[Price],[Status]) OUTPUT INSERTED.CafeId VALUES ('{newcafe.CuisineId}','{newcafe.FoodId}','{newcafe.CustomerName}','{newcafe.FoodName}','{newcafe.CuisineType}','{newcafe.Price}','{newcafe.Status}')";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int newcafeId = (int) command.ExecuteScalar();
                    //int numOfRows = command.ExecuteNonQuery();
                    return newcafeId;
                }
            }
        }

        public int UpdateCafe(Cafe updCafe)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = @$"UPDATE [dbo].[Cafe] SET
                    CuisineId='{updCafe.CuisineId}',
                    FoodId='{updCafe.FoodId}',
                    CustomerName='{updCafe.CustomerName}',
                    FoodName='{updCafe.FoodName}',
                    CuisineType='{updCafe.CuisineType}',
                    Price='{updCafe.Price}',
                    Status='{updCafe.Status}'               
                    Where CafeId = {updCafe.CafeId}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    return numOfRows;
                }
            }
        
    
        }
       public bool DeleteCafe(int cafeId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
             
             
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = $"DELETE FROM [dbo].[Cafe] WHERE CafeId = {cafeId}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    if (numOfRows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
                

    }
}
