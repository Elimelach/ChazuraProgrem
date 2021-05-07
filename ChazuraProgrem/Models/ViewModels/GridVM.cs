using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class GridVM<T>
    {
        public IEnumerable<T> Items { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
