using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaCourier.API.Models
{
    public class BaseResponse<T>
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string mensaje { get; set; }
        public T result { get; set; }
        public BaseResponse()
        {
            success = true;
            code = "0000";
            mensaje = "OK";
        }
    }
}
