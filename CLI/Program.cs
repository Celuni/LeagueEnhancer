using Library;
using System;
using System.Runtime.InteropServices;

namespace CLI
{
    class Program
    {
        static LeagueEnhancer leagueEnhancer;
        static void Main(string[] args)
        {
            #region Lifecylemanagement

            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            #endregion

             leagueEnhancer = new LeagueEnhancer();

            while (true)
                Console.ReadKey();
        }

        #region Lifecylemanagement

        // This region cleanly handles the shutdown of the application (Disposing notifyicon for example)

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
                leagueEnhancer.Shutdown();
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        #endregion
    }
}
