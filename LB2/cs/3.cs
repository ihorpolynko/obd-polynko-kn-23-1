using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Default;

        string connectionString = "Server=DESKTOP-EVR8N9M\\SQLEXPRESS;Database=Examples;Trusted_Connection=True;TrustServerCertificate=True;";

        using SqlConnection connection = new(connectionString);
        try
        {
            connection.Open();

            // Отримуємо дані з бази
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", connection);
            DataSet dataset = new DataSet();

            // Заповнюємо DataSet
            adapter.Fill(dataset, "Users");

            // Виводимо дані
            Console.WriteLine("ПОТОЧНІ ДАНІ:");
            foreach (DataRow row in dataset.Tables["Users"].Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}", row["Id"], row["Age"], row["Name"]);
            }

            // Запит користувача: змінити ім'я користувача з певним Id
            Console.WriteLine("\nВведіть ID користувача, ім'я якого потрібно змінити:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть новий вік:");
            string newAge = Console.ReadLine();

            Console.WriteLine("Введіть нове ім'я:");
            string newName = Console.ReadLine();

            // Знаходимо відповідний рядок і редагуємо
            DataTable usersTable = dataset.Tables["Users"];
            DataRow[] rowsToUpdate = usersTable.Select($"Id = {id}");

            if (rowsToUpdate.Length > 0)
            {
                rowsToUpdate[0]["Age"] = newAge;
                rowsToUpdate[0]["Name"] = newName;

                // Використовуємо SqlCommandBuilder для генерації команд UPDATE
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                // Оновлюємо базу даних
                adapter.Update(dataset, "Users");

                Console.WriteLine("Дані користувача оновлено.");

                Console.WriteLine("\nПОТОЧНІ ДАНІ:");
                foreach (DataRow row in dataset.Tables["Users"].Rows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", row["Id"], row["Age"], row["Name"]);
                }
            }
            else
            {
                Console.WriteLine("Користувача з таким ID не знайдено.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}