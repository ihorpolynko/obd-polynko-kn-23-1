using System;
using Microsoft.Data.SqlClient;
class Program
{
    static void Main(string[] args)
    {
        // з'єднання з базою даних
        SqlConnection connection = new("Server=DESKTOP-EVR8N9M\\SQLEXPRESS;Database=Examples;Trusted_Connection=True;TrustServerCertificate=True;");

        try
        {
            // відкриваємо з'єднання з базою даних
            connection.Open();
            // створюємо запит до бази даних з параметрами
            SqlCommand command = new SqlCommand("SELECT Orders.OrderId, Users.Name, Orders.OrderDate FROM Orders JOIN Users ON Orders.UserId = Users.Id WHERE Users.Age BETWEEN @StartAge AND @EndAge", connection);

            // додаємо параметри до запиту
            command.Parameters.AddWithValue("@StartAge", 20);
            command.Parameters.AddWithValue("@EndAge", 30);

            // отримуємо дані з бази даних за допомогою SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // читаємо дані з SqlDataReader
            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            }

            // закриваємо SqlDataReader
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // закриваємо з'єднання з базою даних
            connection.Close();
        }
    }
}
