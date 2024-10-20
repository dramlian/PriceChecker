using PriceChecker.Iterfaces;

namespace PriceChecker.Helpers
{
    public class ConsoleColorHelper : IAppLogger
    {
        public void AlertSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now}:{message}");
            Console.ResetColor();
        }

        public void AlertWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now}:{message}");
            Console.ResetColor();
        }

        public void AlertFailure(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now}:{message}");
            Console.ResetColor();
        }
    }
}
