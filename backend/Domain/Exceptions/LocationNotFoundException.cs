using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException(long id)
        : base($"Location with ID {id} does not exist")
        {
        }
    }
}
