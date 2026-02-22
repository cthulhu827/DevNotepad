using DevNotepad.Core.TextTransformers;
using Framework.MVC;
using Framework.UI;

namespace DevNotepad.Dialogs.TPBase;

public class c_TPBase<TModel, TView> : ModalDialogController<TModel, TView>, ITPDlgController
    where TModel : MVC_Model, ITPDlgModel, new()
    where TView : ModalDialog
{
    public bool Edit(IParametrizedTextTransformer transformer, string[] source, ITransformerEditSession editSession)
    {
        var model = new TModel();
        model.Init(transformer, source, editSession);
        return ShowDialog(model);
    }
}