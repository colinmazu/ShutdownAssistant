using System.Runtime.InteropServices;

namespace ShutdownAssistant.Services
{
    public class Suspend
    {
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public static async Task<bool> Hibernate(int delay)
        {
            await Task.Delay(delay);
            SetSuspendState(true, true, true);
            return true;
        }

        public static async Task<bool> Sleep(int delay)
        {
            await Task.Delay(delay);
            SetSuspendState(false, true, true);
            return true;
        }
    }
}
