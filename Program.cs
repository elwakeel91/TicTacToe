using System.Collections.Generic;

namespace TicTacToe
{
	// TODO:
	// Check for invalid first move i.e. only an 'O' on the board
	// Check for invalid number of moves made by a player
    // Write some unit tests
	
    class Program
    {
        private enum BoardState
        {
            NAUGHTS_WIN, CROSSES_WIN, DRAW, STILL_PLAYING, NOT_STARTED, INVALID_BOARD
        }

        private static BoardState GetStateOfBoard(string board)
        {			
            BoardState boardState = BoardState.INVALID_BOARD;
			
			// Process the board if there are no errors
			if (!CheckError(board))
			{
				// Use the ProcessBoard method to return the state
				switch(ProcessBoard(board))
				{
					case 'X':
						boardState = BoardState.CROSSES_WIN;
						break;
					case 'O':
						boardState = BoardState.NAUGHTS_WIN;
						break;
					case 'D':
						boardState = BoardState.DRAW;
						break;
					case 'N':
						boardState = BoardState.NOT_STARTED;
						break;
					case 'P':
						boardState = BoardState.STILL_PLAYING;
						break;
					default:
						break;
				}
			}
			
            return boardState;
        }
		
		private static bool CheckError(string board)
		{
			bool error = false;
			
			// Check for incorrect length
            if (board.Length != 9)
            {
                error = true;
				System.Console.Write("Error: Invalid number of characters! ");
            }
            else // Check for an incorrect character 
            {
                foreach (char c in board)
                {
                    if (c != '_' && c != 'X' && c != 'O')
                    {
                        error = true;
                        System.Console.Write("Error: Invalid character '" + c + "'. ");
                    }
                }
            }
			
			return error;
		}
		
		private static bool CheckWinner(string board, ref char winner)
		{
            bool hasWon = false;

            // List of all the possible winning sets of 3 positions
            List<int[]> winningSets = new List<int[]>() 
			{
                // Rows
				new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},

                // Columns
                new int[] {0, 3, 6},
                new int[] {1, 4, 7},
                new int[] {2, 5, 8},

                // Diagonals
                new int[] {0, 4, 8},
                new int[] {2, 4, 6}
			};

            // Loop through the winning sets
            foreach (int[] set in winningSets)
            {
                // Check if there is a winner in the set
                if (board[set[0]] != '_' && board[set[0]] == board[set[1]] && board[set[1]] == board[set[2]])
                {
                    // Set the winning character and break out of the loop
                    winner = board[set[0]];
                    hasWon = true;
                    break;
                }
            }	
			
			return hasWon;
		}

        private static char ProcessBoard(string board)
        {
            char state = '#';
            
            // Check for an empty board i.e. there are no Xs or Os
            if (!(board.Contains('X') || board.Contains('O')))
            {
                state = 'N';
            }
            // Check for any winners
            else if (CheckWinner(board, ref state))
            {
                // All logic is done in the CheckWinner function
            }
            // Check for a draw (i.e. full board with no winners)
            else if (!board.Contains('_'))
            {
                state = 'D';
            }
            // Else they are still playing
            else 
            {
                state = 'P';
            }

            return state;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                System.Console.WriteLine(GetStateOfBoard(args[i]));
            }
        }
    }
}
