using System.Collections.Generic;

namespace EStore.BL.Models._Common
{
    public class SearchModel<T> where T : class
    {
        public SearchModel()
        {
            data = new List<T>();
        }

        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
}