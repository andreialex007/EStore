using System;
using System.Web;
using OpenQA.Selenium.PhantomJS;

namespace EStore.BL.Utils
{
    public static class PhantomJsUtils
    {
        private static PhantomJSDriver _phantomJsDriver;
        private static readonly object Locker = new object();

        private static void Check()
        {
            if (_phantomJsDriver != null) return;

            foreach (var process in System.Diagnostics.Process.GetProcessesByName("phantomjs"))
                process.Kill();
            _phantomJsDriver = new PhantomJSDriver(HttpContext.Current.Request.PhysicalApplicationPath + "\\Resources");
        }

        public static TOut Process<TOut>(Func<PhantomJSDriver, TOut> func) where TOut : class
        {
            try
            {
                lock (Locker)
                {
                    Check();
                    return func(_phantomJsDriver);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}