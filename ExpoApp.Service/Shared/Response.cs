using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Shared
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class Response<TEntity> : Response 
    {
        public TEntity Data { get; set; }
    }
}
