using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.BL.Extensions
{
    public static class OtherExtensions
    {
        public static TOut TryDo<TObj, TOut>(this TObj obj, Func<TObj, TOut> func) where 
            TOut : class
        {
            try
            {
                return func(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
