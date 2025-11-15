using CityStore.Data;
using CityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Controllers
{
    // Controllers/ToolController.cs
    public class ToolController
    {
        private ToolRepo repo = new ToolRepo();

        public IEnumerable<Tool> ListAll() => repo.GetAll();

        public bool CreateTool(string category, string condition)
        {
            var t = new Tool { Category = category, Condition = condition, IsBorrowed = false };
            repo.Create(t);
            return true;
        }

        public bool UpdateTool(Tool t) => repo.Update(t);

        public bool DeleteTool(int id) => repo.Delete(id);
    }

}
