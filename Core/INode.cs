using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface INode<T> where T : IComparable
    {
        T Content { get; set; }
        
        double Distance { get; set; }
        double Heuristic { get; set; }
        double TotalDistance { get; set; }   

        INode<T> Next { get; set; }
        INode<T> Previous { get; set; }

        bool Checked { get; set; }
        
        List<INode<T>> GetNextNodes();
        double CalculateHeuristicalMetric(INode<T> destination);
    }
}
