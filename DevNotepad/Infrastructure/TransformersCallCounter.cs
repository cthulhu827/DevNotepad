namespace DevNotepad.Infrastructure;

public class TransformersCallCounter
{
    private readonly string filePath;

    public TransformersCallCounter(string filePath)
    {
        this.filePath = filePath;
    }

    public void Increment(Guid transformerId)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        var counts = ReadAll();
        counts[transformerId] = counts.TryGetValue(transformerId, out var current) ? current + 1 : 1;
        WriteAll(counts);
    }

    public Dictionary<Guid, int> ReadAll()
    {
        var result = new Dictionary<Guid, int>();
        if (!File.Exists(filePath)) return result;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=');
            if (parts.Length != 2) continue;
            if (!Guid.TryParse(parts[0].Trim(), out var id)) continue;
            if (int.TryParse(parts[1].Trim(), out var count)) result[id] = count;
        }

        return result;
    }

    private void WriteAll(Dictionary<Guid, int> counts)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllLines(filePath, counts.Select(kv => $"{kv.Key}={kv.Value}"));
    }
}