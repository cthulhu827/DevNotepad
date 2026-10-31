using System;
using System.Collections.Generic;
using System.Linq;
using DevNotepad.Core.Lists;
using Newtonsoft.Json;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("List operations", "00c2f75d-6c2a-4a39-904f-06fd4721dc3f")]
    public class ListTransformer : BaseTextTransformer, IParametrizedTextTransformer, IListDependentTransformer
    {
        private Guid? list1Id;
        private Guid? list2Id;

        [JsonProperty]
        public ListOperation Operation { get; set; }

        [JsonProperty]
        public Guid CurrenListId { get; set; }

        [JsonIgnore]
        public IStringList? List1 { get; set; }

        [JsonIgnore]
        public IStringList? List2 { get; set; }

        // Проекция для сериализации: getter вычисляется из List1, а setter
        // заполняется при десериализации (PopulateObject) и хранит Id до момента,
        // пока ResolveLists не восстановит ссылку List1.
        [JsonProperty]
        public Guid? List1Id
        {
            get => List1?.Id;
            set => list1Id = value;
        }

        [JsonProperty]
        public Guid? List2Id
        {
            get => List2?.Id;
            set => list2Id = value;
        }

        public override string[] Transform(string[] lines)
        {
            if (List1 == null || List2 == null) return lines;

            var list1 = List1.Id == CurrenListId ? lines : List1.Items;
            var list2 = List2.Id == CurrenListId ? lines : List2.Items;

            return Operation switch
            {
                ListOperation.Exclude => list1.Except(list2).ToArray(),
                ListOperation.Intersect => list1.Intersect(list2).ToArray(),
                ListOperation.Union => list1.Union(list2).ToArray(),
                ListOperation.Concat => list1.Concat(list2).ToArray(),
                _ => throw new UnsupportedEnumValueException<ListOperation>(Operation)
            };
        }

        #region IListDependentTransformer implementation

        public IEnumerable<IStringList> DependsOn
        {
            get
            {
                if (List1 != null && List1.Id != CurrenListId) yield return List1;
                if (List2 != null && List2.Id != CurrenListId) yield return List2;
            }
        }

        public void ResolveLists(IStringListsProvider listsProvider)
        {
            var lists = listsProvider.GetAll();
            List1 = lists.FirstOrDefault(list => list.Id == list1Id);
            List2 = lists.FirstOrDefault(list => list.Id == list2Id);
        }

        #endregion
    }
}