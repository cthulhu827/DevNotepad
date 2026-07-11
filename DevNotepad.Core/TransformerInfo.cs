using System;
using System.Reflection;
using DevNotepad.Core.TextTransformers;
using Type = System.Type;

namespace DevNotepad.Core
{
    public class TransformerInfo
    {
        private readonly MemberInfo memberInfo;

        public TransformerInfo(Guid id, string caption, MemberInfo memberInfo, int shortCut, string shortCutStr)
        {
            this.memberInfo = memberInfo;
            Id = id;
            Caption = caption;
            ShortCut = shortCut;
            ShortCutStr = shortCutStr;
        }

        public Guid Id { get; }
        public string Caption { get; }
        public int ShortCut { get; }
        public string ShortCutStr { get; }

        public ITextTransformer Build()
        {
            var result = memberInfo switch
            {
                Type type => BuildByCtor(type),
                MethodInfo methodInfo => BuildByMethod(methodInfo),
                _ => throw new Exception("Unsupported member info")
            };

            result.Init(Id, Caption);
            return result;
        }

        private static ITextTransformer BuildByCtor(Type type)
        {
            return (ITextTransformer)Activator.CreateInstance(type);
        }

        private static ITextTransformer BuildByMethod(MethodInfo methodInfo)
        {
            return (ITextTransformer)methodInfo.Invoke(null, Array.Empty<object>());
        }
    }
}