using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceState = new int[,] {{1, 2, 3},
                                          {4, 5, 6},
                                          {7, 8, 0}};

            var destState = new int[,] {{4, 3, 5},
                                        {8, 7, 1},
                                        {0, 2, 6}};

            var sourceBoard = new Board(sourceState, 3, 3);
            var destBoard = new Board(destState, 3, 3);

            Processor<Board> processor = new Processor<Board>();
            var path = processor.FindShortestPath(new Node {Content = sourceBoard}, new Node{Content = destBoard});

            var route = "";
            var step = 0;
            while (path != null)
            {
                step++;
                route = step.ToString() + "\r\n" + (path as Node).ToDisplayString() + "\r\n" + route;
                path = path.Previous;
            }

            Console.WriteLine(route);
            Console.Read();
        }
    }
}
