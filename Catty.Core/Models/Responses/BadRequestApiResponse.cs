using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catty.Core.Models.Responses
{
    public class BadRequestApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string SubCode { get; set; }
        public List<string> Errors { get; set; }
        public BadRequestApiResponse(string message, string code, string subCode, List<string> errors) =>
            (Message, Code, SubCode, Errors) = (message, code, subCode, errors);
    }
}
