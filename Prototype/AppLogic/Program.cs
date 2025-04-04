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
        lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
        GetLastInputInfo(ref lastInPut);

        return ((uint)Environment.TickCount - lastInPut.dwTime);
    }
}

namespace Prototype
{
    static class Program
    {
        public static int idleCooldown = Convert.ToInt32(ConfigurationManager.AppSettings.Get("idleCooldown"));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthForm());
        }

        delegate void CloseMethod(Form form);
        static private void CloseForm(Form form)
        {
            if (!form.IsDisposed)
            {
                if (form.InvokeRequired)
                {
                    CloseMethod method = new CloseMethod(CloseForm);
                    form.Invoke(method, new object[] { form });
                }
                else
                {
                    form.Dispose();
                    form.Close();
                }
            }
        }

        delegate void OpenMethod(Form form);
        static private void OpenForm(Form form)
        {
            if (!form.IsDisposed)
            {
                if (form.InvokeRequired)
                {
                    OpenMethod method = new OpenMethod(OpenForm);
                    form.Invoke(method, new Form[] { form });
                }
                else
                {
                    form.ShowDialog();
                }
            }
        }
        public async static void EnableTimer()
        {
            while (true)
            {
                await Task.Delay(1000 * idleCooldown);
                if (Win32.GetIdleTime() >= 1000 * idleCooldown && AppData.ActiveUser != null)
                {
                    AppData.ActiveUser = null;
                    std.bShowExitMessage = false;
                    List<Form> forms = Application.OpenForms.Cast<Form>().ToList();
                    AuthForm aForm = null;
                    foreach (Form form in forms)
                    {
                        if (form.Name == "AuthForm")
                        {
                            aForm = (AuthForm)form;
                            continue;
                        }
                        CloseForm(form);
                    }
                    Application.Restart();
                    CloseForm(aForm);
                }
            }
        }
    }
}
