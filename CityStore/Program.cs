using CityStore.Controllers;
using CityStore.Data;
using CityStore.Models;
using CityStore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore
{
    internal class Program
    {
        static AuthController auth = new AuthController();
        static ToolController toolController = new ToolController();
        static LoanController loanController = new LoanController();
        static void Main(string[] args)
        {
            Console.WriteLine("City Makerspace Lending System");
            while (true)
            {
                Console.WriteLine("\n1) Login\n2) Exit");
                var opt = Console.ReadLine();
                if (opt == "2") break;
                if (opt == "1")
                {
                    Console.Write("Username: "); var u = Console.ReadLine();
                    Console.Write("Password: "); var p = ReadPassword();
                    var user = auth.Login(u, p);
                    if (user == null) { Console.WriteLine("Invalid credentials."); continue; }
                    Console.WriteLine($"Welcome {user.Username} ({user.Role})");
                    if (user.Role == "Admin") AdminLoop(user);
                    else MemberLoop(user);
                }
            }
        }
        static void AdminLoop(Member user)
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu: 1) View Tools 2) Create Tool 3) Update Tool 4) Delete Tool 5) View Loans 9) Logout");
                var c = Console.ReadLine();
                if (c == "9") return;
                if (c == "1") { ToolView.ShowToolsGrouped(toolController.ListAll()); }
                if (c == "2")
                {
                    var (cat, cond) = ToolView.PromptCreate();
                    toolController.CreateTool(cat, cond);
                    Console.WriteLine("Tool created.");
                }
                if (c == "5")
                {
                    // show all loans - implement LoanRepo.GetAllLoans and format in LoanView
                }
                // implement update/delete paths...
            }
        }

        static void MemberLoop(Member user)
        {
            while (true)
            {
                Console.WriteLine("\nMember Menu: 1) View Tools 2) Borrow Tool 3) Return Tool 4) My Loans 9) Logout");
                var c = Console.ReadLine();
                if (c == "9") return;
                if (c == "1") ToolView.ShowToolsGrouped(toolController.ListAll());
                if (c == "2")
                {
                    // ask for toolId, record condition and date, call loanController.Borrow(user.MemberId, toolId, DateTime.Now)
                }
                // implement return and my loans...
            }
        }
        static string ReadPassword()
        {
            var pass = new StringBuilder();
            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.Backspace && pass.Length > 0) { pass.Length--; Console.Write("\b \b"); }
                else if (key != ConsoleKey.Backspace) { pass.Append(((char)Console.ReadKey(true).KeyChar)); Console.Write("*"); }
            }
            Console.WriteLine();
            return pass.ToString();
        }

    }
}
