using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Processor<T> where T : IComparable
    {
        public INode<T> FindShortestPath(INode<T> source, INode<T> dest)
        {
            List<INode<T>> result = new List<INode<T>>();
            var dic = new HashSet<INode<T>>();
            

            // 1. calculate metric for first node, put in to dicionary
            var currentNode = source;
            currentNode.CalculateHeuristicalMetric(dest);
            currentNode.TotalDistance = currentNode.Distance + currentNode.Heuristic;
            dic.Add(currentNode);

            // 2. repeatedly check until get result
            while (currentNode.Content.CompareTo(dest.Content) != 0)
            {   
                // get the closest node to the destination
                currentNode = dic.Where(n => !n.Checked)
                                 .OrderBy(n => n.TotalDistance)
                                 .FirstOrDefault();

                // if not null
                if (currentNode != null)
                {
                    if (currentNode.Content.CompareTo(dest.Content) == 0)
                        return currentNode;

                    // create new nodes from current node
                    var newNodes = currentNode.GetNextNodes();
                    foreach (var node in newNodes)
                    {
                        // increase distance
                        node.Distance = currentNode.Distance + 1;
                        // update heuristic
                        node.CalculateHeuristicalMetric(dest);
                        // update total distance
                        node.TotalDistance = node.Distance + node.Heuristic;

                        var checkedNode = dic.FirstOrDefault(n => n.Content.Equals(node.Content));
                        if (checkedNode == null)                         
                            dic.Add(node);                        
                        else if (checkedNode.TotalDistance > node.TotalDistance)
                        {
                            dic.Remove(checkedNode);
                            dic.Add(node);
                        }   
                    }

                    // mark as checked node
                    currentNode.Checked = true;
                }
                else break;
            }

            if (currentNode.Content.Equals(dest.Content))
                return currentNode;

            return null;
        }
    }
}
