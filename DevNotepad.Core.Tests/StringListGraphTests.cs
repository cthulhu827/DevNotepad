using DevNotepad.Core.Lists;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DevNotepad.Core.Tests.TextTransformers;

public class StringListGraphTests
{
    [TestCase("1", "1", 1)]
    [TestCase("1, 2>1", "1, 2", 1)]
    [TestCase("1, 2>1, 3>1", "1, 2|3", 1)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "1, 2|3, 4", 1)]
    [TestCase("1, 2>1, 3>2", "1, 2, 3", 1)]
    [TestCase("1, 2>1, 3>1, 4>1, 5>2, 5>3, 5>4", "1, 2|3|4, 5", 1)]
    [TestCase("1, 2>1, 3", "1, 2", 1)]
    [TestCase("1, 2>1, 3", "2", 2)]
    [TestCase("1, 2>1, 3>1, 3>2, 4", "1, 2, 3", 1)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "2, 4", 2)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "3, 4", 3)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>2, 6>3", "1, 2|3, 4|5|6", 1)]
    [TestCase("1, 2, 3>1, 4>2, 5>3, 6", "1, 3, 5", 1)]
    [TestCase("1, 2, 3>1, 4>2, 5>3, 6", "2, 4", 2)]
    [TestCase("1, 2>1, 2>3, 3>1, 4>2, 4>3", "1, 3, 2, 4", 1)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "1, 2|3, 4, 5|6, 7", 1)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "2, 4, 5|6, 7", 2)]
    public void TestDependentLists(string graph, string expectedOrder, int updatedList)
    {
        var lists = ParseGraph(graph);

        var idByGuid = lists.ToDictionary(kv => kv.Value.Id, kv => kv.Key);
        var all = lists.Values.Cast<IStringList>().ToArray();

        // GetDependentLists возвращает только зависимые списки (без самого изменённого),
        // поэтому полный порядок = изменённый список + результат GetOrder.
        var actual = new StringListGraph(all)
            .GetDependentLists(lists[updatedList].Id)
            .Select(id => idByGuid[id])
            .ToList();
        actual.Insert(0, updatedList);

        AssertOrder(actual, ParseExpectedOrder(expectedOrder));
    }

    [TestCase("1", "1", 1)]
    [TestCase("1, 2>1", "2, 1", 2)]
    [TestCase("1, 2>1, 3>1", "2, 1", 2)]
    [TestCase("1, 2>1, 3>1", "3, 1", 3)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "4, 2|3, 1", 4)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "3, 1", 3)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3", "2, 1", 2)]
    [TestCase("1, 2>1, 3>2", "3, 2, 1", 3)]
    [TestCase("1, 2>1, 3>1, 4>1, 5>2, 5>3, 5>4", "5, 2|3|4, 1", 5)]
    [TestCase("1, 2>1, 3", "2, 1", 2)]
    [TestCase("1, 2>1, 3", "3", 3)]
    [TestCase("1, 2>1, 3>1, 3>2, 4", "3, 2, 1", 3)]
    [TestCase("1, 2>1, 3>1, 3>2, 4", "2, 1", 2)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>2, 6>3", "4, 2|3, 1", 4)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>2, 6>3", "5, 2, 1", 5)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>2, 6>3", "6, 3, 1", 6)]
    [TestCase("1, 2, 3>1, 4>2, 5>3, 6", "5, 3, 1", 5)]
    [TestCase("1, 2, 3>1, 4>2, 5>3, 6", "4, 2", 4)]
    [TestCase("1, 2>1, 2>3, 3>1, 4>2, 4>3", "4, 2, 3, 1", 4)]
    [TestCase("1, 2>1, 2>3, 3>1, 4>2, 4>3", "3, 1", 3)]
    [TestCase("1, 2>1, 2>3, 3>1, 4>2, 4>3", "2, 3, 1", 2)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "7, 5|6, 4, 2|3, 1", 7)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "6, 4, 2|3, 1", 6)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "5, 4, 2|3, 1", 5)]
    [TestCase("1, 2>1, 3>1, 4>2, 4>3, 5>4, 6>4, 7>5, 7>6", "4, 2|3, 1", 4)]
    public void TestDependensOnLists(string graph, string expectedOrder, int updatedList)
    {
        var lists = ParseGraph(graph);

        var idByGuid = lists.ToDictionary(kv => kv.Value.Id, kv => kv.Key);
        var all = lists.Values.Cast<IStringList>().ToArray();

        // GetDependentLists возвращает только зависимые списки (без самого изменённого),
        // поэтому полный порядок = изменённый список + результат GetOrder.
        var actual = new StringListGraph(all)
            .GetDependensOnLists(lists[updatedList].Id)
            .Select(id => idByGuid[id])
            .ToList();
        actual.Insert(0, updatedList);

        AssertOrder(actual, ParseExpectedOrder(expectedOrder));
    }

    private static Dictionary<int, TestStringList> ParseGraph(string graph)
    {
        var lists = new Dictionary<int, List<int>>();

        foreach (var token in graph.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var arrow = token.IndexOf('>');
            if (arrow >= 0)
            {
                var dependent = int.Parse(token[..arrow].Trim());
                var dependency = int.Parse(token[(arrow + 1)..].Trim());

                Ensure(lists, dependent).Add(dependency);
                Ensure(lists, dependency);
            }
            else
            {
                Ensure(lists, int.Parse(token));
            }
        }

        var result = lists.Keys.ToDictionary(id => id, _ => new TestStringList());
        foreach (var (id, deps) in lists)
        {
            result[id].DependsOn = deps.Select(d => result[d]).Cast<IStringList>().ToArray();
        }

        return result;
    }

    private static List<int> Ensure(Dictionary<int, List<int>> map, int id)
    {
        if (!map.TryGetValue(id, out var list))
            map[id] = list = new List<int>();
        return list;
    }

    private static List<int[]> ParseExpectedOrder(string expected) =>
        expected.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(group => group
                .Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray())
            .ToList();

    private static void AssertOrder(List<int> actual, List<int[]> expectedSteps)
    {
        var total = expectedSteps.Sum(s => s.Length);
        Assert.That(actual.Count, Is.EqualTo(total),
            $"Число обновлений не совпало. Actual: [{string.Join(", ", actual)}]");

        var index = 0;
        for (var i = 0; i < expectedSteps.Count; i++)
        {
            var step = expectedSteps[i];
            var slice = actual.Skip(index).Take(step.Length);

            Assert.That(slice.OrderBy(x => x).ToArray(), Is.EqualTo(step.OrderBy(x => x).ToArray()),
                $"Шаг {i} не совпал. Actual: [{string.Join(", ", actual)}], " +
                $"Expected step: [{string.Join("|", step)}]");

            index += step.Length;
        }
    }

    private class TestStringList : IStringList
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Caption => "";

        public string[] Items { get; } = Array.Empty<string>();

        public IStringList[] DependsOn { get; set; } = Array.Empty<IStringList>();
    }
}