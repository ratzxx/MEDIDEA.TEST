using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MEDIDEA.Domain.Entities;

namespace MEDIDEA.Infrastructure
{
    public static class MedideaContextSeed
    {
        public static async Task SeedAsync()
        {
            using (var ctx = new MedideaContext())
            {
                ctx.Phones.RemoveRange(ctx.Phones.ToList());
                ctx.Customers.RemoveRange(ctx.Customers.ToList());
                
                await ctx.SaveChangesAsync();
                var rnd = new Random();

                if (!ctx.Customers.Any())
                {
                    IList<Customer> customers = new List<Customer>();

                    var firstNames = new List<string> { "Alice", "Bob", "John", "Ann", "Tim", "Olivia", "Liam", "David" };
                    var lastNames = new List<string> { "Smith", "Williams", "Harris", "Scott", "Neeson" };
                    for (var i = 0; i < 20; ++i)
                    {
                        int indexFirstName = rnd.Next(firstNames.Count);
                        int indexLastName = rnd.Next(lastNames.Count);
                        DateTime start = new DateTime(1989, 2, 7);
                        int range = (DateTime.Today - start).Days;
                        var birthday = start.AddDays(rnd.Next(range));

                        var customer = new Customer()
                        {
                            Name = $"{firstNames[indexFirstName]} {lastNames[indexLastName]}",
                            Birthday = birthday,
                            Gender = i % 2 == 0 ? Gender.Male : Gender.Female

                        };
                        customers.Add(customer);
                    }


                    ctx.Customers.AddRange(customers);
                    await ctx.SaveChangesAsync();
                }

                if (!ctx.Phones.Any())
                {
                    IList<Phone> phones = new List<Phone>();
                    var customers = await ctx.Customers.ToListAsync();
                    foreach (var customer in customers)
                    {
                        phones.Add(new Phone
                        {
                            CustomerId = customer.Id,
                            Type = PhoneType.Mobile,
                            Number = $"+7({rnd.Next(900, 999)}){rnd.Next(100, 999)}-{rnd.Next(10, 99)}-{rnd.Next(10, 99)}"
                        });
                    }

                    ctx.Phones.AddRange(phones);
                    await ctx.SaveChangesAsync();
                }

                if (!ctx.Visits.Any())
                {
                    IList<Visit> visits = new List<Visit>();
                    var customers = await ctx.Customers.ToListAsync();
                    foreach (var customer in customers)
                    {
                        visits.Add(new Visit
                        {
                            CustomerId = customer.Id,
                            Description = customer.Id %2 == 0 ? "He claims he suffered a stroke" : "Dropped like a stone no warning, supposedly a heart attack.",
                            Type = customer.Id % 2 == 0 ? VisitType.First : VisitType.Condition
                        });
                        visits.Add(new Visit
                        {
                            CustomerId = customer.Id,
                            Description = customer.Id %2 != 0 ? "He claims he suffered a stroke" : "Dropped like a stone no warning, supposedly a heart attack.",
                            Type = customer.Id % 2 != 0 ? VisitType.First : VisitType.Second
                        });
                    }

                    ctx.Visits.AddRange(visits);
                    await ctx.SaveChangesAsync();
                }
            }
        }
    }
}