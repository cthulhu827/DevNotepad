using System.Collections.Generic;
using System;
using System.Linq;

namespace DevNotepad.Core.Lists
{
    public class StringListGraph
    {
        private readonly IDictionary<Guid, GraphNode> graph = new Dictionary<Guid, GraphNode>();

        public StringListGraph(IStringList[] allLists)
        {
            BuildGraph(allLists);
        }

        public Guid[] GetDependentLists(Guid id)
        {
            return graph.TryGetValue(id, out var start)
                ? start.GetChildren().Select(node => node.Id).ToArray()
                : Array.Empty<Guid>();
        }

        public Guid[] GetDependensOnLists(Guid id)
        {
            return graph.TryGetValue(id, out var start)
                ? start.GetParents().Select(node => node.Id).ToArray()
                : Array.Empty<Guid>();
        }

        public Guid[] GetAll()
        {
            return graph.Values
                .OrderBy(node => node.Level)
                .Select(node => node.Id)
                .ToArray();
        }

        private void BuildGraph(IStringList[] allLists)
        {
            foreach (var stringList in allLists)
            {
                var node = FindOrCreateNode(stringList.Id);
                foreach (var parent in stringList.DependsOn)
                {
                    var parentNode = FindOrCreateNode(parent.Id);
                    parentNode.Children.Add(node);
                    node.Parents.Add(parentNode);
                }
            }

            graph.Values
                .Where(node => node.Parents.IsEmpty())
                .ForEach(parent => parent.SetChildrenLevelRecursively());
        }

        private GraphNode FindOrCreateNode(Guid id)
        {
            if (graph.TryGetValue(id, out var result)) return result;

            result = new GraphNode(id);
            graph.Add(id, result);
            return result;
        }

        private class GraphNode
        {
            public GraphNode(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }

            public IList<GraphNode> Parents { get; } = new List<GraphNode>();

            public IList<GraphNode> Children { get; } = new List<GraphNode>();

            public int Level { get; private set; }

            public GraphNode[] GetChildren()
            {
                var result = new List<GraphNode>();
                AddChildrenRecursively(result);
                return result.Distinct().OrderBy(n => n.Level).ToArray();
            }

            public GraphNode[] GetParents()
            {
                var result = new List<GraphNode>();
                AddParentsRecursively(result);
                return result.Distinct().OrderByDescending(n => n.Level).ToArray();
            }

            private void AddChildrenRecursively(IList<GraphNode> result)
            {
                foreach (var child in Children)
                {
                    result.Add(child);
                    child.AddChildrenRecursively(result);
                }
            }

            private void AddParentsRecursively(IList<GraphNode> result)
            {
                foreach (var parent in Parents)
                {
                    result.Add(parent);
                    parent.AddParentsRecursively(result);
                }
            }

            public void SetChildrenLevelRecursively()
            {
                foreach (var child in Children)
                {
                    child.Level = Math.Max(child.Level, Level + 1);
                    child.SetChildrenLevelRecursively();
                }
            }
        }
    }
}