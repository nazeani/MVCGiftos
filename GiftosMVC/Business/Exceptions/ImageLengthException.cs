using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ImageLengthException : Exception
    {
        public ImageLengthException(string? message) : base(message)
        {
        }
    }
}
