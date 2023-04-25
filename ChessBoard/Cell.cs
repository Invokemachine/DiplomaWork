namespace ChessBoard
{
    public class Cell : NotifyPropertyChanged
    {
        private State _state;
        private bool _active;
        private bool _possiblemove;
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

        public State State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                OnPropertyChanged();
            }
        }
        public bool PossibleMove
        {
            get => _possiblemove;
            set
            {
                _possiblemove = value;
                OnPropertyChanged();
            }
        }

        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
        }
    }
}