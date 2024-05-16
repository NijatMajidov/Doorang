using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangMVC.Business.Exceptions
{
    public class FileSizeException : Exception
    {
        public FileSizeException(string Name,string? message) : base(message)
        {
            PropertyName = Name;
        }

        public string PropertyName { get; set; }

    }
}
