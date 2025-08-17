using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderAttribute:Attribute
    {
        public string Header { get;}
        
        public HeaderAttribute(string header) 
        {
            Header = header;
        }
    }
}
