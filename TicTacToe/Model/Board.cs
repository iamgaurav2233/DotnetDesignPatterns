using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace TicTacToe
{
    public class Board
    {
        public int size;
        public List<List<PlayingPiece>> board;
        public Board(int size)
        {
            this.size = size;
            board = Enumerable.Range(0, size).Select(_ => Enumerable.Range(0, size).Select(_ => new PlayingPiece(PieceType.NULL)).ToList()).ToList();
        }

        public bool AddPiece(int row, int column, PlayingPiece playingPiece)
        {
            if (board[row][column].pieceType != PieceType.NULL)
            {
                return false;
            }
            board[row][column] = playingPiece;
            return true;
        }

        public List<(int, int)> GetFreeCells()
        {
            List<(int, int)> freeCells = new List<(int, int)>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i][j].pieceType == PieceType.NULL)
                    {
                        freeCells.Add((i, j));
                    }
                }
            }

            return freeCells;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(board[i][j].pieceType + " ");
                }
                Console.WriteLine();
            }
        }
    }
}