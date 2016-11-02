using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.FrameWork.Utils
{
    public class ReflectionUtil
    {

        public static TDerived ToDerived<TBase, TDerived>(TBase tBase)
        where TDerived : TBase, new()
        {
            TDerived tDerived = new TDerived();
            foreach (PropertyInfo propBase in typeof(TBase).GetProperties())
            {
                PropertyInfo propDerived = typeof(TDerived).GetProperty(propBase.Name);
                propDerived.SetValue(tDerived, propBase.GetValue(tBase, null), null);
            }
            return tDerived;
        }

        public static TReturn CopyProperties<TSource, TReturn>(TSource tSource)
        where TReturn : new()
        {
            TReturn tReturn = new TReturn();
            foreach (PropertyInfo propBase in typeof(TSource).GetProperties())
            {
                PropertyInfo propDerived = typeof(TReturn).GetProperty(propBase.Name);
                propDerived.SetValue(tReturn, propBase.GetValue(tSource, null), null);
            }
            return tReturn;
        }


        public static void CopyProperties<T>(T source, T target)
        {
            foreach (PropertyInfo propBase in typeof(T).GetProperties())
            {
                PropertyInfo propDerived = typeof(T).GetProperty(propBase.Name);
                propDerived.SetValue(target, propBase.GetValue(source, null), null);
            }
        }

    }
}
