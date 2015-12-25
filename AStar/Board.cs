using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Board : IComparable
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Content { get; set; }

        public Board(int[,] content, int width, int height)
        {
            this.Content = content;
            this.Width = width;
            this.Height = height;
            
        }

        public int GetDistance(Board dest)
        {
            var distance = 0;

            for (int col = 0; col < Width; col++)
            {
                for (int row = 0; row < Height; row++)
                {
                    if (Content[row, col] != dest.Content[row, col])
                        distance++;
                }
            }

            return distance;
        }

        public Board Clone()
        {
            var newBoard = new Board(new int[Height, Width], Width, Height);
            
            for (int col = 0; col < Width; col++)
            {
                for (int row = 0; row < Height; row++)
                {
                    newBoard.Content[row, col] = Content[row, col];
                }
            }

            return newBoard;
        }

        public int CompareTo(object obj)
        {
            var dest = obj as Board;
            
            return GetDistance(dest);
        }
    }
}
