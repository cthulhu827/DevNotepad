using System;
using Framework.AppInfrastructure;

namespace Framework.Date
{
    [Singleton, DefaultImplementation(IoCMode.ProdOnly, typeof(INow))]
    internal class Now : INow
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}