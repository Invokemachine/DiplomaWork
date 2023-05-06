using ChessBoard.Controls;
using DiplomaApp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Speech.Recognition;

namespace ChessBoard
{
    public class MainViewModel : NotifyPropertyChanged
    {

        public static PlayWindow playWindow;
        public Board _board = new();
        private ICommand _newGameCommand;
        private ICommand _clearCommand;
        private ICommand _cellCommand;


        public static IEnumerable<char> Numbers => "87654321";
        public static IEnumerable<char> Letters => "ABCDEFGH";

        public int CurrentPlayer = 1;
        public bool IsItWhitesPiece;
        public static string CurrentLanguage = "English";

        public Board Board
        {
            get => _board;
            set
            {
                _board = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewGameCommand => _newGameCommand ??= new RelayCommand(parameter =>
        {
            SetupBoard();
        });

        public ICommand ClearCommand => _clearCommand ??= new RelayCommand(parameter =>
        {
            Board = new Board();
        });

        public ICommand CellCommand => _cellCommand ??= new RelayCommand(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell activeCell = Board.FirstOrDefault(x => x.Active);

            if (cell.State != State.Empty)
            {
                if (!cell.Active && activeCell != null)
                {
                    activeCell.Active = false;
                    DeleteMarks(cell.RowNumber, cell.ColumnNumber);
                }
                cell.Active = !cell.Active;
            }
            else if (activeCell != null)
            {
                if (cell.PossibleMove == true)
                {
                    activeCell.Active = false;
                    cell.State = activeCell.State;
                    activeCell.State = State.Empty;
                    DeleteMarks(cell.RowNumber, cell.ColumnNumber);
                    SwitchPlayer();
                }
            }
            else if (activeCell == null)
                DeleteMarks(cell.RowNumber, cell.ColumnNumber);
            else
            {
                activeCell.Active = true;
                cell.State = activeCell.State;
                DeleteMarks(cell.RowNumber, cell.ColumnNumber);
            }
            MarkLegalMoves(activeCell, cell);
            if (!cell.Active)
            {
                DeleteMarks(cell.RowNumber, cell.ColumnNumber);
            }
        }, parameter => parameter is Cell cell && (Board.Any(x => x.Active) || cell.State != State.Empty));

        private void SetupBoard()
        {
            Board board = new();
            board[0, 0] = State.BlackRook;
            board[0, 1] = State.BlackKnight;
            board[0, 2] = State.BlackBishop;
            board[0, 3] = State.BlackQueen;
            board[0, 4] = State.BlackKing;
            board[0, 5] = State.BlackBishop;
            board[0, 6] = State.BlackKnight;
            board[0, 7] = State.BlackRook;
            for (int i = 0; i < 8; i++)
            {
                board[1, i] = State.BlackPawn;
                board[6, i] = State.WhitePawn;
            }
            board[7, 0] = State.WhiteRook;
            board[7, 1] = State.WhiteKnight;
            board[7, 2] = State.WhiteBishop;
            board[7, 3] = State.WhiteQueen;
            board[7, 4] = State.WhiteKing;
            board[7, 5] = State.WhiteBishop;
            board[7, 6] = State.WhiteKnight;
            board[3, 3] = State.WhiteRook;
            Board = board;
        }

        public static bool InsideBorder(int i, int j)
        {
            if (i >= 8 || j >= 8 || i < 0 || j < 0)
                return false;
            else
                return true;
        }

        public void MarkLegalMoves(Cell activeCell, Cell cell)
        {
            if (CurrentPlayer == 1)
            {
                int i, j;

                switch (cell.State)
                {
                    case State.WhiteBishop:
                        if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                        {
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && j < 8 && InsideBorder(i, j) == true; i--, j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i - 8;
                            }

                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && j >= 0 && InsideBorder(i, j); i--, j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i - 8;
                            }

                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && j < 8 && InsideBorder(i, j) == true; i++, j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i + 8;
                            }

                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && j >= 0 && InsideBorder(i, j) == true; i++, j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i + 8;
                            }
                        }
                        break;

                    case State.WhiteKing:
                        i = cell.RowNumber;
                        j = cell.ColumnNumber;
                        if (InsideBorder(i - 1, j) == true && Board._area[i - 1, j].State == State.Empty)
                            Board._area[i - 1, j].PossibleMove = true;
                        if (InsideBorder(i + 1, j) == true && Board._area[i + 1, j].State == State.Empty)
                            Board._area[i + 1, j].PossibleMove = true;
                        if (InsideBorder(i, j - 1) == true && Board._area[i, j - 1].State == State.Empty)
                            Board._area[i, j - 1].PossibleMove = true;
                        if (InsideBorder(i, j + 1) == true && Board._area[i, j + 1].State == State.Empty)
                            Board._area[i, j + 1].PossibleMove = true;
                        if (InsideBorder(i - 1, j - 1) == true && Board._area[i - 1, j - 1].State == State.Empty)
                            Board._area[i - 1, j - 1].PossibleMove = true;
                        if (InsideBorder(i + 1, j + 1) == true && Board._area[i + 1, j + 1].State == State.Empty)
                            Board._area[i + 1, j + 1].PossibleMove = true;
                        if (InsideBorder(i - 1, j + 1) == true && Board._area[i - 1, j + 1].State == State.Empty)
                            Board._area[i - 1, j + 1].PossibleMove = true;
                        if (InsideBorder(i + 1, j - 1) == true && Board._area[i + 1, j - 1].State == State.Empty)
                            Board._area[i + 1, j - 1].PossibleMove = true;
                        break;

                    case State.WhitePawn:
                        if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber) == true && cell.RowNumber == 6)
                        {
                            Board._area[cell.RowNumber - 1, cell.ColumnNumber].PossibleMove = true;
                            Board._area[cell.RowNumber - 2, cell.ColumnNumber].PossibleMove = true;
                        }
                        else if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber) == true && cell.RowNumber != 6 && Board._area[cell.RowNumber - 1, cell.ColumnNumber].State == State.Empty)
                        {
                            Board._area[cell.RowNumber - 1, cell.ColumnNumber].PossibleMove = true;
                        }
                        break;

                    case State.WhiteKnight:
                        if (cell != null)
                        {
                            if (InsideBorder(cell.RowNumber + 2, cell.ColumnNumber + 1) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber + 2, cell.ColumnNumber + 1);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber + 2, cell.ColumnNumber + 1].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber + 2, cell.ColumnNumber + 1].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber + 2, cell.ColumnNumber - 1) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber + 2, cell.ColumnNumber - 1);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber + 2, cell.ColumnNumber - 1].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber + 2, cell.ColumnNumber - 1].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber - 2, cell.ColumnNumber - 1) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber - 2, cell.ColumnNumber - 1);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber - 2, cell.ColumnNumber - 1].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber - 2, cell.ColumnNumber - 1].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber - 2, cell.ColumnNumber + 1) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber - 2, cell.ColumnNumber + 1);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber - 2, cell.ColumnNumber + 1].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber - 2, cell.ColumnNumber + 1].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber + 2) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber + 1, cell.ColumnNumber + 2);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber + 1, cell.ColumnNumber + 2].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber + 1, cell.ColumnNumber + 2].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber - 2) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber + 1, cell.ColumnNumber - 2);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber + 1, cell.ColumnNumber - 2].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber + 1, cell.ColumnNumber - 2].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber - 2) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber - 1, cell.ColumnNumber - 2);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber - 1, cell.ColumnNumber - 2].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber - 1, cell.ColumnNumber - 2].PossibleMove = false;
                            }

                            if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber + 2) == true)
                            {
                                WhichPlayersPiece(cell, cell.RowNumber - 1, cell.ColumnNumber + 2);
                                if (IsItWhitesPiece == false)
                                    Board._area[cell.RowNumber - 1, cell.ColumnNumber + 2].PossibleMove = true;
                                else
                                    Board._area[cell.RowNumber - 1, cell.ColumnNumber + 2].PossibleMove = false;
                            }
                        }
                        break;

                    case State.WhiteRook:
                        if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                        {
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && InsideBorder(i, cell.ColumnNumber); i--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j == cell.ColumnNumber)
                                    i = i - 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && InsideBorder(i, cell.ColumnNumber); i++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j == cell.ColumnNumber)
                                    i = i + 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; j >= 0 && InsideBorder(cell.RowNumber, j); j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i == cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j != cell.ColumnNumber)
                                    j = j - 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; j < 8 && InsideBorder(cell.RowNumber, j); j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i == cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j != cell.ColumnNumber)
                                    j = j + 8;
                            }
                        }
                        break;

                    case State.WhiteQueen:
                        if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                        {
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && InsideBorder(i, cell.ColumnNumber); i--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j == cell.ColumnNumber)
                                    i = i - 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && InsideBorder(i, cell.ColumnNumber); i++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j == cell.ColumnNumber)
                                    i = i + 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; j >= 0 && InsideBorder(cell.RowNumber, j); j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i == cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j != cell.ColumnNumber)
                                    j = j - 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; j < 8 && InsideBorder(cell.RowNumber, j); j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i == cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = true;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j == cell.ColumnNumber)
                                    Board._area[cell.RowNumber, j].PossibleMove = false;
                                else if (IsItWhitesPiece == true && i == cell.RowNumber && j != cell.ColumnNumber)
                                    j = j + 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && j < 8 && InsideBorder(i, j) == true; i--, j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i - 8;
                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i >= 0 && j >= 0 && InsideBorder(i, j); i--, j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i - 8;

                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && j < 8 && InsideBorder(i, j) == true; i++, j++)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i + 8;

                            }
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            for (; i < 8 && j >= 0 && InsideBorder(i, j) == true; i++, j--)
                            {
                                WhichPlayersPiece(cell, i, j);
                                if (IsItWhitesPiece == false && i != cell.RowNumber && j != cell.ColumnNumber)
                                    Board._area[i, j].PossibleMove = true;
                                else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                {
                                    Board._area[i, j].PossibleMove = false;
                                }
                                else if (IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
                                    i = i + 8;
                            }
                        }
                        break;

                    case State.Empty:
                        break;

                    default:
                        break;
                }
            }
            else
            {
                int i, j;
                if (CurrentPlayer != 1)
                {
                    switch (cell.State)
                    {
                        case State.BlackBishop:
                            if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                            {
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && j < 8 && InsideBorder(i, j) == true; i--, j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i - 8;
                                }

                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && j >= 0 && InsideBorder(i, j); i--, j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i - 8;
                                }

                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && j < 8 && InsideBorder(i, j) == true; i++, j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i + 8;
                                }

                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && j >= 0 && InsideBorder(i, j) == true; i++, j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i + 8;
                                }
                            }
                            break;
                        case State.BlackKing:
                            i = cell.RowNumber;
                            j = cell.ColumnNumber;
                            if (InsideBorder(i - 1, j) == true && Board._area[i - 1, j].State == State.Empty)
                                Board._area[i - 1, j].PossibleMove = true;
                            if (InsideBorder(i + 1, j) == true && Board._area[i + 1, j].State == State.Empty)
                                Board._area[i + 1, j].PossibleMove = true;
                            if (InsideBorder(i, j - 1) == true && Board._area[i, j - 1].State == State.Empty)
                                Board._area[i, j - 1].PossibleMove = true;
                            if (InsideBorder(i, j + 1) == true && Board._area[i, j + 1].State == State.Empty)
                                Board._area[i, j + 1].PossibleMove = true;
                            if (InsideBorder(i - 1, j - 1) == true && Board._area[i - 1, j - 1].State == State.Empty)
                                Board._area[i - 1, j - 1].PossibleMove = true;
                            if (InsideBorder(i + 1, j + 1) == true && Board._area[i + 1, j + 1].State == State.Empty)
                                Board._area[i + 1, j + 1].PossibleMove = true;
                            if (InsideBorder(i - 1, j + 1) == true && Board._area[i - 1, j + 1].State == State.Empty)
                                Board._area[i - 1, j + 1].PossibleMove = true;
                            if (InsideBorder(i + 1, j - 1) == true && Board._area[i + 1, j - 1].State == State.Empty)
                                Board._area[i + 1, j - 1].PossibleMove = true;
                            break;

                        case State.BlackPawn:
                            if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber) == true && cell.RowNumber == 1)
                            {
                                Board._area[cell.RowNumber + 1, cell.ColumnNumber].PossibleMove = true;
                                Board._area[cell.RowNumber + 2, cell.ColumnNumber].PossibleMove = true;
                            }
                            else if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber) == true && cell.RowNumber != 1 && Board._area[cell.RowNumber + 1, cell.ColumnNumber].State == State.Empty)
                            {
                                Board._area[cell.RowNumber + 1, cell.ColumnNumber].PossibleMove = true;
                            }
                            break;

                        case State.BlackKnight:
                            if (cell != null)
                            {
                                if (InsideBorder(cell.RowNumber + 2, cell.ColumnNumber + 1) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber + 2, cell.ColumnNumber + 1);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber + 2, cell.ColumnNumber + 1].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber + 2, cell.ColumnNumber + 1].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber + 2, cell.ColumnNumber - 1) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber + 2, cell.ColumnNumber - 1);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber + 2, cell.ColumnNumber - 1].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber + 2, cell.ColumnNumber - 1].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber - 2, cell.ColumnNumber - 1) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber - 2, cell.ColumnNumber - 1);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber - 2, cell.ColumnNumber - 1].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber - 2, cell.ColumnNumber - 1].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber - 2, cell.ColumnNumber + 1) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber - 2, cell.ColumnNumber + 1);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber - 2, cell.ColumnNumber + 1].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber - 2, cell.ColumnNumber + 1].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber + 2) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber + 1, cell.ColumnNumber + 2);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber + 1, cell.ColumnNumber + 2].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber + 1, cell.ColumnNumber + 2].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber + 1, cell.ColumnNumber - 2) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber + 1, cell.ColumnNumber - 2);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber + 1, cell.ColumnNumber - 2].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber + 1, cell.ColumnNumber - 2].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber - 2) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber - 1, cell.ColumnNumber - 2);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber - 1, cell.ColumnNumber - 2].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber - 1, cell.ColumnNumber - 2].PossibleMove = false;
                                }

                                if (InsideBorder(cell.RowNumber - 1, cell.ColumnNumber + 2) == true)
                                {
                                    WhichPlayersPiece(cell, cell.RowNumber - 1, cell.ColumnNumber + 2);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty)
                                        Board._area[cell.RowNumber - 1, cell.ColumnNumber + 2].PossibleMove = true;
                                    else
                                        Board._area[cell.RowNumber - 1, cell.ColumnNumber + 2].PossibleMove = false;
                                }
                            }
                            break;
                        case State.BlackRook:
                            if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                            {
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && InsideBorder(i, cell.ColumnNumber); i--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        i = i - 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && InsideBorder(i, cell.ColumnNumber); i++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        i = i + 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; j >= 0 && InsideBorder(cell.RowNumber, j); j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        j = j - 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; j < 8 && InsideBorder(cell.RowNumber, j); j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        j = j + 8;
                                }
                            }
                            break;

                        case State.BlackQueen:
                            if (InsideBorder(cell.RowNumber, cell.ColumnNumber) == true)
                            {
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && InsideBorder(i, cell.ColumnNumber); i--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        i = i - 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && InsideBorder(i, cell.ColumnNumber); i++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[i, cell.ColumnNumber].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j == cell.ColumnNumber)
                                        i = i + 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; j >= 0 && InsideBorder(cell.RowNumber, j); j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        j = j - 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; j < 8 && InsideBorder(cell.RowNumber, j); j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = true;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j == cell.ColumnNumber)
                                        Board._area[cell.RowNumber, j].PossibleMove = false;
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i == cell.RowNumber && j != cell.ColumnNumber)
                                        j = j + 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && j < 8 && InsideBorder(i, j) == true; i--, j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i - 8;
                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i >= 0 && j >= 0 && InsideBorder(i, j); i--, j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i - 8;

                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && j < 8 && InsideBorder(i, j) == true; i++, j++)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i + 8;

                                }
                                i = cell.RowNumber;
                                j = cell.ColumnNumber;
                                for (; i < 8 && j >= 0 && InsideBorder(i, j) == true; i++, j--)
                                {
                                    WhichPlayersPiece(cell, i, j);
                                    if (IsItWhitesPiece == true || cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        Board._area[i, j].PossibleMove = true;
                                    else if (i == cell.RowNumber && j == cell.ColumnNumber)
                                    {
                                        Board._area[i, j].PossibleMove = false;
                                    }
                                    else if (IsItWhitesPiece == false && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
                                        i = i + 8;
                                }
                            }
                            break;
                        case State.Empty:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool IsNothingOnPath(Cell cell, int i, int j)
        {
            WhichPlayersPiece(cell, i, j);
            if (InsideBorder(i, j) == true && IsItWhitesPiece == true && i != cell.RowNumber && j != cell.ColumnNumber)
            {
                Board._area[i, j].PossibleMove = false;
                return false;
            }
            else if (InsideBorder(i, j) == true && cell.State != State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
            {
                if (IsItWhitesPiece == false)
                    Board._area[i, j].PossibleMove = true;
                else
                    Board._area[i, j].PossibleMove = false;
                return false;
            }
            else if (InsideBorder(i, j) == true && cell.State == State.Empty && i != cell.RowNumber && j != cell.ColumnNumber)
            {
                Board._area[i, j].PossibleMove = true;
                return true;
            }
            return true;
        }

        public void DeleteMarks(int i, int j)
        {
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    Board._area[i, j].PossibleMove = false;
                }
            }
        }

        public void SwitchPlayer()
        {
            if (CurrentPlayer == 1)
            {
                CurrentPlayer = 2;
                return;
            }
            else
            {
                CurrentPlayer = 1;
                return;
            }
        }



        public void WhichPlayersPiece(Cell cell, int i, int j)
        {
            if (CurrentPlayer == 1)
            {
                if (InsideBorder(i, j) == true)
                {
                    if (Board._area[i, j].State == State.WhiteKing || Board._area[i, j].State == State.WhiteKnight || Board._area[i, j].State == State.WhiteRook || Board._area[i, j].State == State.WhitePawn || Board._area[i, j].State == State.WhiteQueen || Board._area[i, j].State == State.WhiteBishop)
                        IsItWhitesPiece = true;
                    else
                        IsItWhitesPiece = false;
                }
            }
            else if (CurrentPlayer != 1)
            {
                if (InsideBorder(i, j) == true)
                {
                    if (Board._area[i, j].State == State.BlackKing || Board._area[i, j].State == State.BlackKnight || Board._area[i, j].State == State.BlackRook || Board._area[i, j].State == State.BlackPawn || Board._area[i, j].State == State.BlackQueen || Board._area[i, j].State == State.BlackBishop)
                        IsItWhitesPiece = false;
                    else
                        IsItWhitesPiece = true;
                }
            }
        }
        
        public MainViewModel()
        {
        }
    }
}