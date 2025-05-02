using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LR1OBD
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-EVR8N9M\\SQLEXPRESS;Database=Examples;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();  // Відкриваємо підключення

                // Створення таблиці Orders
                string createOrdersTable = "CREATE TABLE Orders (OrderId INT PRIMARY KEY IDENTITY, OrderDate DATETIME NOT NULL, UserId INT, FOREIGN KEY (UserId) REFERENCES Users(Id));";
                SqlCommand createOrdersCommand = new SqlCommand(createOrdersTable, connection);
                await createOrdersCommand.ExecuteNonQueryAsync();
                Console.WriteLine("Таблиця Orders створена");

                // Вставка даних в таблицю Orders
                string insertOrder = "INSERT INTO Orders (OrderDate, UserId) VALUES (GETDATE(), 1);";
                SqlCommand insertOrderCommand = new SqlCommand(insertOrder, connection);
                int orderNumber = await insertOrderCommand.ExecuteNonQueryAsync();
                Console.WriteLine($"Додано об'єктів в Orders: {orderNumber}");
            }

            Console.Read();
        }
    }
}