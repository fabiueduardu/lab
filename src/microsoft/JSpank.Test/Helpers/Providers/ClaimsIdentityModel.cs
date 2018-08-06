namespace JSpank.Test.Helpers.Providers
{
    internal class ClaimsIdentityModel : System.Security.Claims.ClaimsIdentity
    {
        private string name;

        public ClaimsIdentityModel(string name)
        {
            this.name = name;
        }

        public override string Name
        {
            get
            {
                return this.name;
            }
        }


        public override bool IsAuthenticated => !string.IsNullOrEmpty(name);
    }
}
