class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? City { get; set; }
}

class Student
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

class Grade
{
    public int StudentId { get; set; }
    public int Score { get; set; }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Default;

        List<Person> people =
        [
            new Person { FirstName = "Іван", LastName = "Петренко", Age = 25, City = "Київ" },
            new Person { FirstName = "Олена", LastName = "Іванова", Age = 32, City = "Львів" },
            new Person { FirstName = "Андрій", LastName = "Сидоренко", Age = 29, City = "Одеса" },
            new Person { FirstName = "Марія", LastName = "Коваль", Age = 41, City = "Київ" },
            new Person { FirstName = "Петро", LastName = "Бондар", Age = 34, City = "Львів" },
            new Person { FirstName = "Світлана", LastName = "Гончар", Age = 23, City = "Київ" },
            new Person { FirstName = "Ірина", LastName = "Мельник", Age = 22, City = "Львів" },
            new Person { FirstName = "Олег", LastName = "Шевченко", Age = 45, City = "Одеса" },
            new Person { FirstName = "Іван", LastName = "Захарченко", Age = 27, City = "Харків" },
            new Person { FirstName = "Наталія", LastName = "Литвин", Age = 36, City = "Київ" }
        ];

        List<Student> students =
        [
            new Student { Id = 1, Name = "Іван" },
            new Student { Id = 2, Name = "Марія" },
            new Student { Id = 3, Name = "Олег" },
            new Student { Id = 4, Name = "Ірина" }
        ];

        List<Grade> grades =
        [
            new Grade { StudentId = 1, Score = 95 },
            new Grade { StudentId = 2, Score = 87 },
            new Grade { StudentId = 3, Score = 91 },
            new Grade { StudentId = 4, Score = 78 }
        ];

        while (true)
        {
            Console.WriteLine("\n\n=== Меню запитів ===");
            Console.WriteLine("1 - Люди з Києва (сортування за прізвищем)");
            Console.WriteLine("2 - Старші за 30 у Львові (сортування за ім'ям)");
            Console.WriteLine("3 - Унікальні міста");
            Console.WriteLine("4 - Найбільш поширене ім'я");
            Console.WriteLine("5 - Кількість людей у Києві");
            Console.WriteLine("6 - Перші 5 людей зі Львова");
            Console.WriteLine("7 - Останні 3 людини з Одеси");
            Console.WriteLine("8 - Люди з віком, кратним 3 (за спаданням)");
            Console.WriteLine("9 - Загальний вік людей з Києва");
            Console.WriteLine("a - Додаткове: Студенти з оцінками > 90");
            Console.WriteLine("0 - Вивід таблиць");
            Console.WriteLine("n - Вихід із програми");
            Console.Write("\nВаш вибір: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    people
                        .Where(p => p.City == "Київ")
                        .OrderBy(p => p.LastName)
                        .ToList()
                        .ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName} - {p.City}"));
                    break;

                case "2":
                    people
                        .Where(p => p.City == "Львів" && p.Age > 30)
                        .OrderBy(p => p.FirstName)
                        .ToList()
                        .ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName} - {p.Age}"));
                    break;

                case "3":
                    people
                        .Select(p => p.City)
                        .Distinct()
                        .ToList()
                        .ForEach(city => Console.WriteLine(city));
                    break;

                case "4":
                    var commonName = people
                        .GroupBy(p => p.FirstName)
                        .OrderByDescending(g => g.Count())
                        .First().Key;
                    Console.WriteLine($"Найбільш поширене ім’я: {commonName}");
                    break;

                case "5":
                    var count = people.Count(p => p.City == "Київ");
                    Console.WriteLine($"Кількість людей у Києві: {count}");
                    break;

                case "6":
                    people
                        .Where(p => p.City == "Львів")
                        .Take(5)
                        .ToList()
                        .ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName}"));
                    break;

                case "7":
                    people
                        .Where(p => p.City == "Одеса")
                        .Reverse()
                        .Take(3)
                        .ToList()
                        .ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName}"));
                    break;

                case "8":
                    people
                        .Where(p => p.Age % 3 == 0)
                        .OrderByDescending(p => p.Age)
                        .ToList()
                        .ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName} - {p.Age}"));
                    break;

                case "9":
                    int totalAge = people
                        .Where(p => p.City == "Київ")
                        .Sum(p => p.Age);
                    Console.WriteLine($"Загальний вік людей з Києва: {totalAge}");
                    break;

                case "a":
                    var topStudents = students
                        .Join(grades,
                              s => s.Id,
                              g => g.StudentId,
                              (s, g) => new { s.Name, g.Score })
                        .Where(x => x.Score > 90)
                        .ToList();
                    Console.WriteLine("Студенти з оцінками > 90:");
                    topStudents.ForEach(s => Console.WriteLine($"{s.Name} - {s.Score}"));
                    break;

                case "n":
                    Console.WriteLine("Програма завершена.");
                    return;

                case "0":
                    Console.WriteLine("Дані таблиці People:");
                    people.ForEach(p => Console.WriteLine($"Ім'я: {p.FirstName} {p.LastName}, Вік: {p.Age}, Місто: {p.City}"));

                    Console.WriteLine("\nДані таблиці Students:");
                    students.ForEach(s => Console.WriteLine($"ID: {s.Id}, Ім'я: {s.Name}"));

                    Console.WriteLine("\nДані таблиці Grades:");
                    grades.ForEach(g => Console.WriteLine($"ID Студента: {g.StudentId}, Оцінка: {g.Score}"));
                    break;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}