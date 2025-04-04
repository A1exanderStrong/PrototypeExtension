using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;

internal struct LastInputInfo
{
    public uint cbSize;
    public uint dwTime;
}

public class Win32
{

    [DllImport("User32.dll")]
    private static extern bool
            GetLastInputInfo(ref LastInputInfo ptrLastInputInfo);

    public static uint GetIdleTime()
    {
        LastInputInfo lastInPut = new LastInputInfo();
        lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
        GetLastInputInfo(ref lastInPut);

        return ((uint)Environment.TickCount - lastInPut.dwTime);
    }
}

namespace Prototype
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread timerThread = new Thread(new ThreadStart(EnableTimer));
            Application.Run(new AuthForm());
        }
    }
}
