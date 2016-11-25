using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.FrameWork.DbContextFactory.Implementations
{
    public class SingletoneInstance
    {
        public static DbContextScopeFactory dbContextScopeFactory;
        private static object lockobj = new object();

        public static DbContextScopeFactory GetDbContextScopeFactory()
        {
            if (dbContextScopeFactory == null)
            {
                lock (lockobj)
                {
                    //double check
                    if (dbContextScopeFactory == null)
                    {
                        dbContextScopeFactory = new DbContextScopeFactory();
                    }
                }
            }
            return dbContextScopeFactory;

        }
    }
}
