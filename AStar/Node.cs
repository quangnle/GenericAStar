using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace AStar
{
    public class Node : INode<Board>
    {
        public Board Content { get; set; }

        public double Distance { get; set; }
        public double Heuristic { get; set; }
        public double TotalDistance { get; set; }

        public INode<Board> Next { get; set; }
        public INode<Board> Previous { get; set; }

        public bool Checked { get; set; }

        public List<INode<Board>> GetNextNodes()
        {
            var dx = new int[]{ -1, 0, 1, 0 };
            var dy = new int[] { 0, -1, 0, 1 };
            var list = new List<INode<Board>>();

            // look for empty block marked as 0 value
            for (int col = 0; col < Content.Width; col++)
            {
                for (int row = 0; row < Content.Height; row++)    
                {
                    // if found, generate all possible moves
                    if (this.Content.Content[row , col] == 0)
                    {
                        // check 4 blocks around
                        for (int i = 0; i < 4; i++)
                        {
                            var nx = col + dx[i];
                            var ny = row + dy[i];

                            // validate position
                            if ((nx >= 0 && nx < Content.Width)&&(ny >= 0 && ny < Content.Height))
                            {
                                var newBoard = this.Content.Clone();
                                newBoard.Content[ny, nx] = 0;
                                newBoard.Content[row, col] = this.Content.Content[ny, nx];

                                var newNode = new Node { Content = newBoard };

                                newNode.Previous = this;
                                this.Next = newNode;

                                list.Add(newNode);
                            }
                        }

                        break;
                    }
                }
            }

            return list;
        }

        public double CalculateHeuristicalMetric(INode<Board> destination)
        {
            Heuristic = Content.GetDistance(destination.Content);
            return Heuristic;
        }

        public string ToDisplayString()
        {
            var st = "";

            for (int row = 0; row < Content.Height; row++)
            {
                for (int col = 0; col < Content.Width; col++)
                {
                    st += Content.Content[row, col].ToString() + "  ";
                }
                st += "\r\n";
            }

            return st;
        }
    }
}
