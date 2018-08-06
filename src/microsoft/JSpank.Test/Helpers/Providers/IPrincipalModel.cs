using System.Security.Principal;

namespace JSpank.Test.Helpers.Providers
{
    internal class IPrincipalModel : IPrincipal
    {
        private IIdentity identity;
        public IIdentity Identity => identity;

        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

        public IPrincipalModel(string name)
        {
            this.identity = new ClaimsIdentityModel(name);
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
