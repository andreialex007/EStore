using System.Collections.Generic;
using System.Linq;

namespace EStore.BL.Models._Common
{
    public class SearchParams
    {
        public int draw { get; set; }
        public List<Column> columns { get; set; }
        public List<Order> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
        public string OrderBy => columns[order.First().column].data;
        public bool IsAsc => order.First().dir == "asc";
    }
}