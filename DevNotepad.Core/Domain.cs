using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevNotepad.Core.TextTransformers;
using Framework.AppInfrastructure;

namespace DevNotepad.Core
{
    public static class Domain
    {
        private static readonly Lazy<TransformerInfo[]> all = new Lazy<TransformerInfo[]>(BuildAll);

        private static TransformerInfo[] BuildAll()
        {
            var result = new List<TransformerInfo>();
            var implementingTypes = AnnotatedClasses.AllImplementing<ITextTransformer>().ToArray();
            foreach (var type in implementingTypes)
            {
                AddAttrIfExist(type);

                var methods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
                foreach (var method in methods) AddAttrIfExist(method);
            }

            return result.ToArray();

            void AddAttrIfExist(MemberInfo memberInfo)
            {
                var attr = memberInfo.GetCustomAttribute<TextTransformerAttribute>();
                if (attr != null)
                {
                    result.Add(new TransformerInfo(attr.Id, attr.Caption, memberInfo));
                }
            }
        }

        public static TransformerInfo[] All => all.Value;

        public static ITextTransformer CreateById(Guid id)
        {
            return All.SingleOrDefault(i => i.Id == id)?.Build()
                   ?? throw new Exception($"{nameof(ITextTransformer)} with id = {id} not found");
        }
    }
}