using Microsoft.EntityFrameworkCore;
using MapFramework.Core.Extensions;
using MapFramework.Data;
using MapFramework.Data.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dbseeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Arguments must be provided to seed the database. Your options are as follows:");
                Console.WriteLine("[connectionString] [mapData]");
                Console.WriteLine();
                throw new Exception("No connection string provided");
            }

            var connection = args[0];
            var mapData = args[1];

            while (string.IsNullOrEmpty(connection))
            {
                Console.WriteLine("Please provide a connection string:");
                connection = Console.ReadLine();
                Console.WriteLine();
            }

            while (string.IsNullOrEmpty(mapData))
            {
                Console.WriteLine("Please provide a path to map metadata JSON");
                mapData = Console.ReadLine();
                Console.WriteLine();
            }

            try
            {
                Console.WriteLine($"Connection: {connection}");
                Console.WriteLine($"Map Data: {mapData}");

                var builder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connection);

                using (var db = new AppDbContext(builder.Options))
                {
                    Console.WriteLine("Verifying DB Connection");
                    await db.Database.CanConnectAsync();
                    Console.WriteLine("Connection Succeeded");
                    Console.WriteLine();
                    await db.Initialize(mapData);
                }

                Console.WriteLine();
                Console.WriteLine("Database seeding completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while seeding the database:");
                Console.WriteLine(ex.GetExceptionChain());
            }
        }
    }
}
