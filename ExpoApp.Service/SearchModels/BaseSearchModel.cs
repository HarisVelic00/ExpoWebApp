using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.SearchModels
{
    public class BaseSearchModel
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
