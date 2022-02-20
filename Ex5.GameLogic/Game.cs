using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex5.GameLogic
{
    enum eCellType
    {
        Player1,
        Player2,
        Empty
    }

    public enum eGameStatus
    {
        Player1Won,
        Player2Won,
        Tie,
        InAction
    }

    public class Game
    {
        private eCellType[,] m_Board;
        public readonly int r_BoardSize;
        Player m_Player1;
        Player m_Player2;
        Random m_rand;
        eGameStatus m_GameStatus;

        public Game(int i_BoardSize, Player i_Player1, Player i_Player2)
        {
            m_rand = new Random();
            m_GameStatus = eGameStatus.InAction;
            r_BoardSize = i_BoardSize;
            m_Board = new eCellType[r_BoardSize, r_BoardSize];
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;

            initializeBoard();
        }

        private void initializeBoard()
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_Board[i, j] = eCellType.Empty;
                }
            }
        }

        public int[] ComputerFindMove()
        {
            HashSet<int[]> availableCells = new HashSet<int[]>();

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if (m_Board[i, j] == eCellType.Empty)
                    {
                        int[] cell = { i, j };
                        availableCells.Add(cell);
                    }
                }
            }

            return availableCells.ElementAt(m_rand.Next(availableCells.Count));
        }

        public void MakeMove(int i_Row, int i_Col, Player i_Player)
        {
            switch (i_Player.r_PlayerNumber)
            {
                case ePlayer.Player1:
                    m_Board[i_Row, i_Col] = eCellType.Player1;
                    break;
                case ePlayer.Player2:
                    m_Board[i_Row, i_Col] = eCellType.Player2;
                    break;
            }

            checkGameStatus(i_Player);
            
        }

        private void checkGameStatus(Player i_Player)
        {
            eCellType diagonalRightCell = m_Board[0, 0];
            eCellType diagonalLeftCell = m_Board[0, r_BoardSize - 1];
            bool wonDiagonalRight = diagonalRightCell != eCellType.Empty;
            bool wonDiagonalLeft = diagonalLeftCell != eCellType.Empty;
            bool boardIsFull = true;
            bool gameOver = false;

            for (int i = 0; i < r_BoardSize; i++)
            {
                eCellType rowCell = m_Board[i, 0];
                eCellType colCell = m_Board[0, i];
                bool wonRow = rowCell != eCellType.Empty;
                bool wonCol = colCell != eCellType.Empty;
                eCellType currentDiagonalRightCell = m_Board[i, i];
                eCellType currentDiagonalLeftCell = m_Board[i, r_BoardSize - 1 - i];

                if (diagonalRightCell != currentDiagonalRightCell)
                {
                    wonDiagonalRight = false;
                }

                if (diagonalLeftCell != currentDiagonalLeftCell)
                {
                    wonDiagonalLeft = false;
                }

                for (int j = 0; j < r_BoardSize; j++)
                {
                    eCellType currentRowCell = m_Board[i, j];
                    eCellType currentColCell = m_Board[j, i];

                    if (currentRowCell == eCellType.Empty)
                    {
                        boardIsFull = false;
                    }

                    if (rowCell != currentRowCell)
                    {
                        wonRow = false;
                    }

                    if (colCell != currentColCell)
                    {
                        wonCol = false;
                    }
                }

                if (wonRow || wonCol)
                {
                    if (i_Player.r_PlayerNumber == ePlayer.Player1)
                    {
                        m_Player2.m_Wins += 1;
                        m_GameStatus = eGameStatus.Player2Won;
                    }
                    else
                    {
                        m_Player1.m_Wins += 1;
                        m_GameStatus = eGameStatus.Player1Won;
                    }

                    gameOver = true;
                    break;
                }
            }

            if (!gameOver && (wonDiagonalRight || wonDiagonalLeft))
            {
                if (i_Player.r_PlayerNumber == ePlayer.Player1)
                {
                    m_Player2.m_Wins += 1;
                    m_GameStatus = eGameStatus.Player2Won;
                }
                else
                {
                    m_Player1.m_Wins += 1;
                    m_GameStatus = eGameStatus.Player1Won;
                }
            }
            else if (!gameOver && boardIsFull)
            {
                m_GameStatus = eGameStatus.Tie;
            }
        }

        public eGameStatus GameStatus
        {
            get { return m_GameStatus; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }
    }   
}
