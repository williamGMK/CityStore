using CityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Views
{
    // Views/ToolView.cs
    public static class ToolView
    {
        public static void ShowToolsGrouped(IEnumerable<Tool> tools)
        {
            var groups = tools.GroupBy(t => t.Category);
            foreach (var g in groups)
            {
                Console.WriteLine($"\n=== {g.Key} ===");
                foreach (var t in g)
                {
                    var status = t.IsBorrowed ? "[BORROWED]" : "[AVAILABLE]";
                    Console.WriteLine($"ID:{t.ToolId} - {t.Condition} {status}");
                }
            }
        }

        public static (string category, string condition) PromptCreate()
        {
            Console.Write("Category: ");
            var cat = Console.ReadLine();
            Console.Write("Condition (New/Good/Fair/Damaged): ");
            var cond = Console.ReadLine();
            return (cat, cond);
        }
    }

}
