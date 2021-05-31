using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using XML_Generator.Context;
using XML_Generator.Model;
using XML_Generator.Service;

namespace XML_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            SeedData();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("create_xml <path> - Creates an XML file with all the table data");
            Console.WriteLine("create_xml_with_filter <options> <path> - Creates an XML file with a filter on FirstName or LastName fields");
            Console.WriteLine("read_xml <path> - Reads the XML file");
            Console.WriteLine("exit- Exit from the app");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                var command = Console.ReadLine();
                switch (command.Split(' ').FirstOrDefault())
                {
                    case "create_xml":
                        CreateXML(
                            command.Split(' ').Skip(1).FirstOrDefault());
                        break;
                    case "create_xml_with_filter":
                        CreateXML(
                            command.Split(' ').Skip(1).FirstOrDefault(),
                            command.Split(' ').Skip(2).FirstOrDefault());
                        break;
                    case "read_xml":
                        ReadXML(
                            command.Split(' ').Skip(1).FirstOrDefault());
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Command not found!");
                        break;

                }
            }
        }

        static void SeedData()
        {
            using (var db = new PersonDbContext())
            {
                db.AddRange(
                new Person
                {
                    Id = 5,
                    Email = "test1@test.com",
                    FirstName = "Andrz",
                    LastName = "Lock",
                    Phone = "0981234539"
                },
                new Person
                {
                    Id = 1,
                    Email = "test2@test.com",
                    FirstName = "Petro",
                    LastName = "Andrev",
                    Phone = "09812312339"
                },
                new Person
                {
                    Id = 2,
                    Email = "test3@test.com",
                    FirstName = "Kiva",
                    LastName = "Androv",
                    Phone = "0974077981"
                },
                new Person
                {
                    Id = 3,
                    Email = "test4@test.com",
                    FirstName = "Nina",
                    LastName = "Jako",
                    Phone = "09812312339"
                },
                new Person
                {
                    Id = 4,
                    Email = "test5@test.com",
                    FirstName = "Maria",
                    LastName = "Bereza",
                    Phone = "09812992339"
                });

                db.SaveChanges();
            }
        }

        static async void CreateXML(string filePath)
        {
            try
            {
                using (var db = new PersonDbContext())
                {
                    var service = new XMLService();
                    var people = await db.People.ToListAsync();
                    service.CreateFile(filePath, people);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("File created successfully!");
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        static async void CreateXML(string filter, string filePath)
        {
            try
            {
                using (var db = new PersonDbContext())
                {
                    var service = new XMLService();
                    var people = (await db.People.ToListAsync()).Where(p => 
                        p.FirstName.StartsWith(filter, StringComparison.OrdinalIgnoreCase) ||
                        p.LastName.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).ToList();

                    service.CreateFile(filePath, people);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("File created successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        static void ReadXML(string filePath)
        {
            try
            {
                var service = new XMLService();
                var result = service.ReadFile(filePath);
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine();
                Console.WriteLine("ID\tFIRST NAME\t\tLAST NAME\t\tEMAIL\t\tPHONE");
                foreach (var item in result)
                    Console.WriteLine("{0}\t{1}\t\t{2}\t\t{3}\t\t{4}", item.Id, item.FirstName, item.LastName, item.Email, item.Phone);
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
