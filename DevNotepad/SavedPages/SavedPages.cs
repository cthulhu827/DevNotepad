using DevNotepad.Controls.Page;
using DevNotepad.Controls.PipeControl;
using DevNotepad.Controls.WorkArea;
using DevNotepad.Core;
using DevNotepad.Core.Lists;
using DevNotepad.Dialogs.SavedPagesDlg;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevNotepad.SavedPages
{
    public static class SavedPages
    {
        public static VM_SavedPage[] GetPages()
        {
            return Directory.EnumerateFiles(Program.Settings.SavedPagesPath, "*.json")
                .Select(BuildViewModel)
                .ToArray();
        }

        public static string ToJson(this m_Page page)
        {
            var jObject = new JObject();
            jObject.Add(new JProperty(nameof(m_Page.Caption), page.Caption));

            var graph = new StringListGraph(((IStringListsProvider)page).GetAll());
            var saveOrder = graph.GetAll();

            var workAreasArray = new JArray();
            foreach (var workAreaId in saveOrder)
            {
                var workArea = page.WorkAreas.Single(wa => wa.Id == workAreaId);
                workAreasArray.Add(JToken.Parse(workArea.ToJson()));
            }

            jObject.Add(new JProperty(nameof(m_Page.WorkAreas), workAreasArray));
            return jObject.ToString(Formatting.Indented);
        }

        public static m_Page ToPage(this string json)
        {
            var jObject = JObject.Parse(json);
            var caption = jObject[nameof(m_Page.Caption)]?.Value<string>() ?? string.Empty;
            var result = new m_Page(false, caption);

            if (jObject[nameof(m_Page.WorkAreas)] is JArray workAreasArray)
            {
                foreach (var workAreaToken in workAreasArray)
                {
                    workAreaToken.ToString().ToWorkArea(result);
                }
            }

            return result;
        }

        private static string ToJson(this m_WorkArea model)
        {
            var jObject = new JObject
            {
                new JProperty(nameof(m_WorkArea.Id), model.Id),
                new JProperty(nameof(m_WorkArea.Caption), model.Caption)
            };

            var transformersArray = new JArray();
            foreach (var pipeItem in model.Pipe.Items)
            {
                var transformerJson = JToken.Parse(pipeItem.Transformer.ToJson());
                transformersArray.Add(transformerJson);
            }

            jObject.Add(new JProperty(nameof(m_WorkArea.Pipe), transformersArray));
            return jObject.ToString(Formatting.Indented);
        }

        private static m_WorkArea ToWorkArea(this string json, m_Page page)
        {
            var jObject = JObject.Parse(json);

            var id = jObject[nameof(m_WorkArea.Id)]?.ToObject<Guid>();
            var caption = jObject[nameof(m_WorkArea.Caption)]?.Value<string>() ?? string.Empty;
            var result = new m_WorkArea(page, id) { Caption = caption };

            page.AddWorkArea(result);

            if (jObject[nameof(m_WorkArea.Pipe)] is JArray transformersArray)
            {
                foreach (var transformerToken in transformersArray)
                {
                    var transformer = transformerToken.ToString().ToTransformer(page);
                    result.Pipe.AddItem(new VM_PipeItem(transformer));
                }

                result.PipeIndex = transformersArray.Count - 1;
            }

            return result;
        }

        private static VM_SavedPage BuildViewModel(string fileName)
        {
            var caption = Path.GetFileNameWithoutExtension(fileName);
            return new VM_SavedPage(caption, fileName);
        }
    }
}