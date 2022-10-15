namespace AmazonUK.UITests
{
    using System.Threading;

    public class Helper
    {
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}