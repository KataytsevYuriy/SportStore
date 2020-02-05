using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models.Pages
{
    public class QueryOptions
    {
        public int CurrentPge { get; set; } = 1;
        public int PageSie { get; set; } = 10;
    }
}
