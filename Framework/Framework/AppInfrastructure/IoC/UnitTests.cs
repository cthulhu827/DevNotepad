using System;
using System.Linq;

namespace Framework.AppInfrastructure
{
    public static class UnitTests
    {
        private static bool? isTestEnvironment;

        private static bool CheckTestEnvironment()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Any(assembly => assembly.FullName.ToLowerInvariant().StartsWith("nunit.framework"));
        }

        public static bool IsTestEnvironment
        {
            get
            {
                if (isTestEnvironment == null)
                {
                    isTestEnvironment = CheckTestEnvironment();
                }

                return isTestEnvironment.Value;
            }
        }
    }
}