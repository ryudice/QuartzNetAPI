using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.ViewEngines.Razor;

namespace Quartz.API.Admin
{
    class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            throw new NotImplementedException();
        }

        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
