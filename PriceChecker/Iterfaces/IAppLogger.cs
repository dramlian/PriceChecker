namespace PriceChecker.Iterfaces
{
    public interface IAppLogger
    {
        public void AlertSuccess(string message);
        public void AlertWarning(string message);
        public void AlertFailure(string message);
    }
}
