using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTM.WPF.Models
{
    public class RestResponse<T>
    {
    public T Content { get; set; }
    public string ContentRaw { get; set; }
    public bool IsSucceeded { get; set; }
    public string ErrorMessage { get; set; }
    }
}
