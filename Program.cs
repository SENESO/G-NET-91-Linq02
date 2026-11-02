using LINQ.DataSources;
using LINQ.Models;

namespace Linq02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Q1:");
            var top3 = Source.ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
            foreach (var p in top3)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
            Console.WriteLine();

            Console.WriteLine("Q2:");
            int pageNumber = 2;
            int pageSize = 5;
            var page2 = Source.ProductList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            foreach (var p in page2)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
            Console.WriteLine();

            Console.WriteLine("Q3:");
            var cheap = Source.ProductList.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
            foreach (var p in cheap)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
            Console.WriteLine();

            Console.WriteLine("Q4:");
            bool allSeafoodInStock = Source.ProductList
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);
            Console.WriteLine($"All Seafood products are in stock: {allSeafoodInStock}");
            Console.WriteLine();

            Console.WriteLine("Q5:");
            int[] ids = { 3, 9, 13, 18 };
            Console.WriteLine($"The list contains 9: {ids.Contains(9)}");
            Console.WriteLine();

            Console.WriteLine("Q6:");
            var groups = Source.ProductList.GroupBy(p => p.Category);
            foreach (var group in groups)
                Console.WriteLine($"{group.Key}: {group.Count()} products");
            Console.WriteLine();

            Console.WriteLine("Q7:");
            var namesByCategory = Source.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Names = g.Select(p => p.ProductName) });
            foreach (var group in namesByCategory)
            {
                Console.WriteLine(group.Category + ":");
                foreach (var name in group.Names)
                    Console.WriteLine("  " + name);
            }
            Console.WriteLine();

            Console.WriteLine("Q8:");
            var bigCategories = Source.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3);
            foreach (var group in bigCategories)
                Console.WriteLine($"{group.Key}: {group.Count()} products");
            Console.WriteLine();

            // Q9 must be query syntax
            Console.WriteLine("Q9:");
            var customersByCountry =
                from c in Source.CustomerList
                group c by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                };
            foreach (var item in customersByCountry)
                Console.WriteLine($"{item.Country} - Count: {item.Count} - TotalOrderValue: {item.TotalOrderValue}");
            Console.WriteLine();

            Console.WriteLine("Q10:");
            int totalUnits = Source.ProductList.Sum(p => p.UnitsInStock);
            Console.WriteLine($"Total units in stock: {totalUnits}");
            Console.WriteLine();

            Console.WriteLine("Q11:");
            decimal cheapest = Source.ProductList.Min(p => p.UnitPrice);
            decimal mostExpensive = Source.ProductList.Max(p => p.UnitPrice);
            Console.WriteLine($"Cheapest price: {cheapest}");
            Console.WriteLine($"Most expensive price: {mostExpensive}");
            Console.WriteLine();

            Console.WriteLine("Q12:");
            var categories = Source.ProductList.Select(p => p.Category).Distinct();
            foreach (var category in categories)
                Console.WriteLine(category);
            Console.WriteLine();

            Console.WriteLine("Q13:");
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var onlyInA = setA.Except(setB);
            Console.WriteLine(string.Join(", ", onlyInA));
            Console.WriteLine();

            Console.WriteLine("Q14:");
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var onlyInList1 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine(string.Join(", ", onlyInList1));
            Console.WriteLine();

            Console.WriteLine("Q15:");
            Dictionary<int, Product> productsById = Source.ProductList.ToDictionary(p => p.ProductID);
            Console.WriteLine(productsById[18]);
            Console.WriteLine();

            Console.WriteLine("Q16:");
            var firstAbove50 = Source.ProductList.First(p => p.UnitPrice > 50);
            Console.WriteLine($"{firstAbove50.ProductName} - {firstAbove50.UnitPrice}");
            Console.WriteLine();

            Console.WriteLine("Q17:");
            var firstAbove500 = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 500);
            if (firstAbove500 != null)
                Console.WriteLine($"{firstAbove500.ProductName} - {firstAbove500.UnitPrice}");
            else
                Console.WriteLine("No product found with a price greater than 500");
            Console.WriteLine();

            Console.WriteLine("Q18:");
            var tableRow = Enumerable.Range(1, 10).Select(n => $"7 x {n} = {7 * n}");
            foreach (var item in tableRow)
                Console.WriteLine(item);
            Console.WriteLine();

            Console.WriteLine("Q19:");
            var evenNumbers = Enumerable.Range(1, 30).Where(n => n % 2 == 0);
            Console.WriteLine(string.Join(", ", evenNumbers));
            Console.WriteLine();

            Console.WriteLine("Q20:");
            var firstNames = Source.ProductList.Take(3).Select(p => p.ProductName)
                .Concat(Source.CustomerList.Take(3).Select(c => c.CustomerName));
            foreach (var name in firstNames)
                Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("Q21:");
            var pairs = Source.ProductList.Zip(Source.CustomerList,
                (p, c) => $"{p.ProductName} sold to {c.CustomerName}");
            foreach (var pair in pairs)
                Console.WriteLine(pair);
        }
    }
}
