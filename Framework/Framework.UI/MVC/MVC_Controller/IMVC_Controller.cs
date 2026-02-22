using Framework.MVC;
using System.Windows.Forms;

namespace Framework.UI
{
    public interface IMVC_Controller
    {
        MVC_Model? ModelNullable { get; set; }
        MVC_Model Model { get; set; }
        Control? ViewNullable { get; set; }
        Control View { get; set; }
        Control CreateView(Control parent);
        void StartListeningDomain();
        void StopListeningDomain();
        ActionScope? Scope { get; }
    }
}
