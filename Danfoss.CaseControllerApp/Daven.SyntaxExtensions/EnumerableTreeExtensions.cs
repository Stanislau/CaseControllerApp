using System;
using System.Collections.Generic;

namespace Daven.SyntaxExtensions
{
    public static class EnumerableTreeExtensions
    {
        public static IEnumerable<T> GetAllNodes<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> getChildren)
        {
            return self.GetExpandedNodes(model => true, getChildren);
        }

        public static IEnumerable<T> GetExpandedNodes<T>(this IEnumerable<T> self, Func<T, bool> isExpanded,
            Func<T, IEnumerable<T>> getChildren)
        {
            var result = new List<T>();
            
            FillExpandedNodes(result, self, isExpanded, getChildren);

            return result;
        }

        private static void FillExpandedNodes<T>(List<T> result, IEnumerable<T> nodes, Func<T, bool> needToDive,
            Func<T, IEnumerable<T>> getChildren)
        {
            foreach (var treeNodeViewModel in nodes.SelfOrEmpty())
            {
                result.Add(treeNodeViewModel);
                if (needToDive(treeNodeViewModel))
                {
                    FillExpandedNodes(result, getChildren(treeNodeViewModel), needToDive, getChildren);
                }
            }
        }
    }
}