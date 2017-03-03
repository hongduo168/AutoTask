using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper.Model
{
    public class RequestInfo : IReturnVoid
    {
        public RequestHeader Header
        { get; set; }

        public RequestBody Body
        { get; set; }
    }

    public class RequestHeader
    {
        public string Auth { get; set; }
        public string Method { get; set; }
        public string Ver { get; set; }
        public string Source { get; set; }
        public string Act { get; set; }
    }

    public class RequestBody
    {
        public string Data { get; set; }
    }
}
