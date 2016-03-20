using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Linq
{
    class Linq_01_Select
    {
        public static void Calis()
        {
            var customer = new[]
            {
                new{CustomerID = 1,FirstName = "Orlando",LastName = "Gee", CompanyName = "A Bike Store"},
                new{CustomerID = 2,FirstName = "Keith",LastName = "Harris", CompanyName = "Bike World"},
                new{CustomerID = 3,FirstName = "Donna",LastName = "Carreras", CompanyName = "A Bike Store"},
                new{CustomerID = 4,FirstName = "Janet",LastName = "Gates", CompanyName = "Fitness Hotel"},
                new{CustomerID = 5,FirstName = "Lucy",LastName = "Harrington", CompanyName = "Grand Industries"},
                new{CustomerID = 6,FirstName = "David",LastName = "Liu", CompanyName = "Bike World"},
                new{CustomerID = 7,FirstName = "Donald",LastName = "Blanton", CompanyName = "Grand Industries"},
                new{CustomerID = 8,FirstName = "Jackie",LastName = "Blackwell", CompanyName = "Fitness Hotel"},
                new{CustomerID = 9,FirstName = "Elsa",LastName = "Leavitt", CompanyName = "Grand Industries"},
                new{CustomerID = 10,FirstName = "Eric",LastName = "Lang", CompanyName = "Distant Inn"}
            };
            var adress = new[] 
            {
                new{CompanyName ="A Bike Store",City = "New York", Country = "United States"},
                new{CompanyName ="Bike World",City = "Chicago", Country = "United States"},
                new{CompanyName ="Fitness Hotel",City = "Otawwa", Country = "Canada"},
                new{CompanyName ="Grand Industries",City = "London", Country = "United Kingdom"},
                new{CompanyName ="Distant Inn",City = "Tetbury", Country = "United Kingdom"}
            };

            var a = customer.Select1(arg => arg.FirstName);
            var grps = customer.GroupBy(c => c.CompanyName);
        }

    }

    static class Benim
    {
        public static IEnumerable<TR> Select1<TS, TR>(this IEnumerable<TS> t, Func<TS, TR> f)
        {
            var select1 = new List<TR>();
            foreach (var source in t)
            {
                select1.Add(f(source));
            }
            return select1;
        }
    }
}
