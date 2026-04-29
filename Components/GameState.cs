using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectFour
{
    public class GameState
    {
        static GameState()
        {
            CalculateWinningPlaces();
        }

        public enum WinState
        {
            No_Winner = 0,
            Player1_Wins = 1,
            Player2_Wins = 2,
            Tie = 3
        }

        public int PlayerTurn => TheBoard.Count(x => x != 0) % 2 + 1;
        public int CurrentTurn => TheBoard.Count(x => x != 0);
        public List<int> TheBoard { get; private set; } = new List<int>(new int[42]);

        public void ResetBoard()
        {
            TheBoard = new List<int>(new int[42]);
        }

        public int PlayPiece(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (TheBoard[column + (row * 7)] == 0)
                {
                    TheBoard[column + (row * 7)] = PlayerTurn;
                    return row;
                }
            }
            throw new ArgumentException("Column is full");
        }

        public WinState CheckForWin()
        {
            foreach (var scenario in WinningPlaces)
            {
                if (TheBoard[scenario[0]] != 0 &&
                    TheBoard[scenario[0]] == TheBoard[scenario[1]] &&
                    TheBoard[scenario[1]] == TheBoard[scenario[2]] &&
                    TheBoard[scenario[2]] == TheBoard[scenario[3]])
                {
                    return (WinState)TheBoard[scenario[0]];
                }
            }
            if (TheBoard.Count(x => x != 0) == 42) return WinState.Tie;
            return WinState.No_Winner;
        }

        private static readonly List<int[]> WinningPlaces = new();

        private static void CalculateWinningPlaces()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int index = col + (row * 7);
                    WinningPlaces.Add(new[] { index, index + 1, index + 2, index + 3 });
                }
            }
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    int index = col + (row * 7);
                    WinningPlaces.Add(new[] { index, index + 7, index + 14, index + 21 });
                }
            }
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int index = col + (row * 7);
                    WinningPlaces.Add(new[] { index, index + 8, index + 16, index + 24 });
                }
            }
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    int index = col + (row * 7);
                    WinningPlaces.Add(new[] { index, index + 6, index + 12, index + 18 });
                }
            }
        }
    }
}