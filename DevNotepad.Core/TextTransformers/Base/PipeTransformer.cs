using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevNotepad.Core.TextTransformers
{
    public abstract class PipeTransformer : BaseTextTransformer, ICombinedTextTransformer
    {
        private readonly ITextTransformer[] transformers;

        protected PipeTransformer(IEnumerable<ITextTransformer> transformers)
            : this(transformers.ToArray())
        {
        }

        protected PipeTransformer(params ITextTransformer[] transformers)
        {
            this.transformers = transformers;
            AddCaptionToTransformers();
        }

        public override string[] Transform(string[] lines)
        {
            var result = lines;
            foreach (var transformer in transformers)
            {
                result = transformer.Transform(result);
            }

            return result;
        }

        public ITextTransformer[] Components => transformers;

        private void AddCaptionToTransformers()
        {
            foreach (var transformer in transformers)
            {
                if (!string.IsNullOrWhiteSpace(transformer.Caption)) continue;

                var type = transformer.GetType();
                var attr = type.GetCustomAttribute<TextTransformerAttribute>();
                if (attr != null)
                    transformer.Init(attr.Id, attr.Caption);
                else
                    throw new Exception($"{type.Name} must be marked with " +
                                        $"{nameof(TextTransformerAttribute)} " +
                                        $"to be used in {nameof(PipeTransformer)}");
            }
        }
    }
}