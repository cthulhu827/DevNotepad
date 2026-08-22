using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevNotepad.Core.TextTransformers;
using Framework.AppInfrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DevNotepad.Core
{
    public static class Domain
    {
        private static readonly Lazy<TransformerInfo[]> all = new Lazy<TransformerInfo[]>(BuildAll);

        public static IShortcutConverter? ShortcutConverter { get; set; }

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
                    var shortCut = ShortcutConverter?.Convert(attr.ShortCut) ?? 0;
                    result.Add(new TransformerInfo(attr.Id, attr.Caption, memberInfo, shortCut, attr.ShortCut));
                }
            }
        }

        public static TransformerInfo[] All => all.Value;

        public static ITextTransformer CreateById(Guid id)
        {
            return All.SingleOrDefault(i => i.Id == id)?.Build()
                   ?? throw new Exception($"{nameof(ITextTransformer)} with id = {id} not found");
        }

        public static ITextTransformer Copy(this ITextTransformer src)
        {
            return src is IParametrizedTextTransformer
                ? src.ToJson().ToTransformer()
                : CreateById(src.Id);
        }

        public static string ToJson(this ITextTransformer transformer)
        {
            var jObject = JObject.FromObject(transformer);
            jObject.AddFirst(new JProperty(nameof(ITextTransformer.Id), transformer.Id.ToString()));
            return jObject.ToString(Formatting.Indented);
        }

        public static ITextTransformer ToTransformer(this string json, ITextTransformer? transformer = null)
        {
            if (transformer == null)
            {
                var jObject = JObject.Parse(json);
                var id = jObject[nameof(ITextTransformer.Id)]?.ToObject<Guid>()
                         ?? throw new JsonException("Missing 'Id' field in JSON.");
                transformer = CreateById(id);
            }

            JsonConvert.PopulateObject(json, transformer);
            return transformer;
        }
    }
}