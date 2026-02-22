using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace System
{
    public static class XmlHelper
    {
        public static string UnsafeAttrValue(this XmlNode node, string attrName)
        {
            var attributes = node.Attributes;
            if (attributes == null)
            {
                throw new AppException("Node {0} doesn't have attributes", node.Name);
            }
            return attributes[attrName].Value;
        }

        public static IEnumerable<XmlNode> UnsafeNodes(this XmlNode node, string path)
        {
            var nodes = node.SelectNodes(path);
            if (nodes == null)
            {
                throw new AppException("Path {0} for node {1} not found", path, node.Name);
            }
            return nodes.OfType<XmlNode>();
        }
    }
}