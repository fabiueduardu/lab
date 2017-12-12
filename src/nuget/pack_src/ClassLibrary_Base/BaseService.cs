namespace ClassLibrary_Base
{
    public abstract class BaseService
    {
        public string FullName
        {
            get
            {
                return this.GetType().FullName;
            }
        }
    }
}
