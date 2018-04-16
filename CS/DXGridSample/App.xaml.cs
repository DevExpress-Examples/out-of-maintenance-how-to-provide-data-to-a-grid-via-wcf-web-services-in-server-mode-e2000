using System.Windows;
using DevExpress.Data.Linq.Helpers;

namespace DXGridSample {

    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            LinqServerModeKeyedCache.CanSkip = true;
        }
    }
}
