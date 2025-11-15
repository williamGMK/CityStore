using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Models
{
    // Models/Tool.cs
    public class Tool
    {
        public int ToolId { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; } // New, Good, Fair, Damaged
        public bool IsBorrowed { get; set; }
    }

}
