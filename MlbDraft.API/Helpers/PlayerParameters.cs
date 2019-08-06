using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Helpers
{
    public class PlayerParameters
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string Team { get; set; }
        public string Position {get; set;}
        
        
    }
}
