using SoftUniStoreApp.Data;

namespace SoftUniStoreApp.Data
{
    public class Data
    {
        private static SoftUniStoreContext context;

        public static SoftUniStoreContext Context => context ?? (context = new SoftUniStoreContext());
    }
}
