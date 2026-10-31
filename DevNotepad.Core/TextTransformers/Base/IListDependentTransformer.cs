using System.Collections.Generic;
using DevNotepad.Core.Lists;

namespace DevNotepad.Core.TextTransformers
{
    public interface IListDependentTransformer
    {
        IEnumerable<IStringList> DependsOn { get; }

        void ResolveLists(IStringListsProvider listsProvider);
    }
}