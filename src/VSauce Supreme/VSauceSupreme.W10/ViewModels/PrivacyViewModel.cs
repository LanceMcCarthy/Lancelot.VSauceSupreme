using System;
using AppStudio.Common;

namespace VSauceSupreme.ViewModels
{
    public class PrivacyViewModel : ObservableBase
    {
        public Uri Url
        {
            get
            {
                return new Uri(UrlText, UriKind.RelativeOrAbsolute);
            }
        }
        public string UrlText
        {
            get
            {
                return "http://w8privacy.azurewebsites.net/privacy?dev=Lancelot%20Software&app=VSauce%20Supreme&mail=bGFuY2Vsb3RAb3V0bG9vay5jb20=&lng=En";
            }
        }
    }
}

