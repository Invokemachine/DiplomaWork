using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ChessBoard
{
    public class Board : IEnumerable<Cell>
    {
        public readonly Cell[,] _area;

        public State this[int row, int column]
        {
            get => _area[row, column].State;
            set => _area[row, column].State = value;
        }

        public Board()
        {
            _area = new Cell[8, 8];
            for (int i = 0; i < _area.GetLength(0); i++)
                for (int j = 0; j < _area.GetLength(1); j++)
                    _area[i, j] = new Cell(i,j);
        }

        public IEnumerator<Cell> GetEnumerator() 
            => _area.Cast<Cell>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => _area.GetEnumerator();
    }
}