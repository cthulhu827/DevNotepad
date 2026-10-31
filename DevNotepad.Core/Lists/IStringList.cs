using System;

namespace DevNotepad.Core.Lists
{
    public interface IStringList
    {
        Guid Id { get; }
        string Caption { get; }
        string[] Items { get; }
        IStringList[] DependsOn { get; }
    }
}