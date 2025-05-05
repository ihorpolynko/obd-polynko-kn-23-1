using System;
using Microsoft.Data.SqlClient;
using System.Data;

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

            // створюємо запит до бази даних
            SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);

            // отримуємо дані з бази даних за допомогою SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // Отримуємо назви полів та читаємо дані з SqlDataReader
            while (reader.Read())
            {
                Console.Write("{0}\t{1}\t{2} \n", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2));
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
