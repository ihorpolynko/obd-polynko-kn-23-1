using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Default;

        // Створення XML-файлу
        XElement productsXml = new XElement("products",
            new XElement("product",
                new XElement("name", "Samsung Galaxy S20"),
                new XElement("price", 12000),
                new XElement("category", "Мобільні телефони"),
                new XElement("inStock", true)
            ),
            new XElement("product",
                new XElement("name", "Xiaomi Redmi Note 8"),
                new XElement("price", 8000),
                new XElement("category", "Мобільні телефони"),
                new XElement("inStock", true)
            ),
            new XElement("product",
                new XElement("name", "Sony Headphones"),
                new XElement("price", 700),
                new XElement("category", "Електроніка"),
                new XElement("inStock", true)
            ),
            new XElement("product",
                new XElement("name", "Dell Laptop"),
                new XElement("price", 25000),
                new XElement("category", "Електроніка"),
                new XElement("inStock", false)
            )
        );

        // Збереження XML у файл
        string filePathOriginal = "products.xml";
        productsXml.Save(filePathOriginal);

        // Збереження XML у файл
        string filePathChange = "products2.xml";
        productsXml.Save(filePathChange);

        // Завдання 1: Вивести список продуктів з категорії «Електроніка» та ціною менше 1000 грн
        Console.WriteLine("Продукти з категорії «Електроніка» та ціною менше 1000 грн:");
        var electronicsUnder1000 = productsXml.Elements("product")
            .Where(p => (string?)p.Element("category") == "Електроніка" && (int?)p.Element("price") < 1000);

        foreach (var product in electronicsUnder1000)
        {
            Console.WriteLine(product.Element("name")?.Value);
        }

        // Завдання 2: Змінити ціну продукту «Samsung Galaxy S20» на 15000 грн
        var samsungProduct = productsXml.Elements("product")
            .FirstOrDefault(p => (string?)p.Element("name") == "Samsung Galaxy S20");
        if (samsungProduct != null)
        {
            samsungProduct.Element("price")?.SetValue(15000);
        }

        // Завдання 3: Видалити продукт «Xiaomi Redmi Note 8»
        var xiaomiProduct = productsXml.Elements("product")
            .FirstOrDefault(p => (string?)p.Element("name") == "Xiaomi Redmi Note 8");
        xiaomiProduct?.Remove();

        // Завдання 4: Додати новий продукт «Apple iPhone 14»
        productsXml.Add(new XElement("product",
            new XElement("name", "Apple iPhone 14"),
            new XElement("price", 25000),
            new XElement("category", "Мобільні телефони"),
            new XElement("inStock", true)
        ));

        // Збереження змін у файл
        productsXml.Save(filePathChange);

        Console.WriteLine("\nЗміни збережено у файл.");
    }
}