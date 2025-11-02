using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Security;

namespace TicTacToe
{
    public class TicTacToeGame
    {
        LinkedList<Player> players;
        Board gameBoard;

        public TicTacToeGame()
        {
            initializeGame();
        }

        public void initializeGame()
        {
            players = new LinkedList<Player>();
            PlayingPieceX crossPiece = new PlayingPieceX();
            Player player1 = new Player("player1", crossPiece);

            PlayingPieceO noughtsPiece = new PlayingPieceO();
            Player player2 = new Player("player2", noughtsPiece);

            players.AddLast(player1);
            players.AddLast(player2);

            gameBoard = new Board(3);
        }
        public string StartGame()
        {
            bool noWinner = true;
            while (noWinner)
            {
                Player playerTurn = players.First.Value;
                players.RemoveFirst();
                gameBoard.PrintBoard();
                List<(int, int)> freeSpace = gameBoard.GetFreeCells();

                if (freeSpace.Count == 0)
                {
                    noWinner = false;
                    continue;
                }

                // read the user input
                Console.Write("Player: " + playerTurn.name + "Enter row, column: ");
                int[] values = Console.ReadLine().Split(",").Select(int.Parse).ToArray();
                int inputRow = values[0];
                int inputColumn = values[1];

                // place the piece
                bool pieceAddedSuccessfully = gameBoard.AddPiece(inputRow, inputColumn, playerTurn.playingPiece);
                if (!pieceAddedSuccessfully)
                {
                    Console.WriteLine("Incorrect position chosen, try again");
                    players.AddFirst(playerTurn);
                    continue;
                }
                players.AddLast(playerTurn);

                bool winner = IsThereWinner(inputRow, inputColumn, playerTurn.playingPiece.pieceType);
                if (winner)
                {
                    gameBoard.PrintBoard();
                    return playerTurn.name;
                }
            }
            return "tie";
        }

        public bool IsThereWinner(int row, int column, PieceType pieceType)
        {
            bool rowMatch = true;
            bool columnMatch = true;
            bool diagonalMatch = true;
            bool antiDiagonalMatch = true;

            for (int i = 0; i < gameBoard.board.Count; i++)
            {
                if (gameBoard.board[row][i].pieceType == PieceType.NULL || gameBoard.board[row][i].pieceType != pieceType)
                {
                    rowMatch = false;
                }
            }

            for (int i = 0; i < gameBoard.board.Count; i++)
            {
                if (gameBoard.board[i][column].pieceType == PieceType.NULL || gameBoard.board[i][column].pieceType != pieceType)
                {
                    columnMatch = false;
                }
            }

            for (int i = 0; i < gameBoard.board.Count; i++)
            {
                if (gameBoard.board[i][i].pieceType == PieceType.NULL || gameBoard.board[i][i].pieceType != pieceType)
                {
                    diagonalMatch = false;
                }
            }

            for (int i = 0, j = gameBoard.board.Count - 1; i < gameBoard.board.Count; i++, j--)
            {
                if (gameBoard.board[i][j].pieceType == PieceType.NULL || gameBoard.board[i][j].pieceType != pieceType)
                {
                    antiDiagonalMatch = false;
                }
            }
            return rowMatch || columnMatch || diagonalMatch || antiDiagonalMatch;
        }
    }
}