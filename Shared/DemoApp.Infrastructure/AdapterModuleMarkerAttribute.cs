using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure
{
    public class AdapterModuleMarkerAttribute:Attribute
    {
        private Guid _id;
        private string _name;
        private string _version;

        public AdapterModuleMarkerAttribute(string guid,string name, string version)
        {
            this._id = Guid.Parse(guid);
            this._name = name;
            this._version = version;
        }

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Version
        {
            get
            {
                return _version;
            }
        }
    }
}
