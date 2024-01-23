using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catty.Core.Models.Responses
{
    public class ModelErrorResponse
    {
        public  string  Field { get; set; }
        public  string  ErrorMessage { get; set; }
        public ModelErrorResponse(string field, string errMessage)
        {
            Field = field;
            ErrorMessage = errMessage;
        }
    };
}
