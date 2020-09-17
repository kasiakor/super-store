using SuperStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            //authenticate method validates credentials supplied by the user
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                //adds cookie to the response to the browser so the user doesnt have to auth every time they make a request
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}