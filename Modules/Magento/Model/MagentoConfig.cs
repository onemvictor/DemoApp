using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Infrastructure;

namespace Magento.Model
{
    public class MagentoConfig:AbstractNotifier
    {
        private Guid _id;
        private string _name;
        private string _url;
        private string _apiKey;
        private string _apiPass;

        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                SetValue(ref _id, value);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetValue(ref _name, value);
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                SetValue(ref _url, value);
            }
        }

        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                SetValue(ref _apiKey, value);
            }
        }

        public string ApiPass
        {
            get
            {
                return _apiPass;
            }
            set
            {
                SetValue(ref _apiPass, value);
            }
        }
    }
}
