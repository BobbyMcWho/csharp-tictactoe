using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static RobertMcDonaldHelpers.Helpers;


namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool game = true;
            Console.Clear();
            do
            {
                int[,] board = new int[3, 3];
                int[,] totals = new int[3, 3];
                char[,] prettyBoard = new char[3, 3];
                string input;
                int[] bestMove;
                bool cont = true;
                bool?[] gameStatus = new bool?[] {false,false};
                string difficultyString = "";
                int difficulty = 50;
                bool invalidInput = true;
                char player = 'H';
                bool playFirst = true;



                Console.WriteLine(@"  _______ _        _______           _______         ");
                Console.WriteLine(@" |__   __(_)      |__   __|         |__   __|        ");
                Console.WriteLine(@"    | |   _  ___     | | __ _  ___     | | ___   ___ ");
                Console.WriteLine(@"    | |  | |/ __|    | |/ _` |/ __|    | |/ _ \ / _ \");
                Console.WriteLine(@"    | |  | | (__     | | (_| | (__     | | (_) |  __/");
                Console.WriteLine(@"    |_|  |_|\___|    |_|\__,_|\___|    |_|\___/ \___|");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("  Select E (Easy) M (Medium) H (Hard) I (Impossible)");
                Console.CursorVisible = false;
                do
                {
                    char pressed = Console.ReadKey().KeyChar;
                    switch (pressed)
                    {
                        case 'e':
                            difficulty = 50;
                            difficultyString = "Easy";
                            cont = false;
                            break;
                        case 'm':
                            difficulty = 80;
                            difficultyString = "Medium";
                            cont = false;
                            break;
                        case 'h':
                            difficulty = 93;
                            difficultyString = "Hard";
                            cont = false;
                            break;
                        case 'i':
                            difficulty = 100;
                            difficultyString = "Impossible";
                            cont = false;
                            break;
                        default:
                            Console.Write("\b \b");
                            break;
                    }
                } while (cont);
                cont = true;
                Console.WriteLine($"\b   You selected {difficultyString}");
                Console.WriteLine("  Would you like to be X's or O's?");
                do
                {
                    char pressed = Console.ReadKey().KeyChar;
                    switch (pressed)
                    {
                        case 'x':
                            player = 'x';
                            cont = false;
                            break;
                        case 'o':
                            player = 'o';
                            playFirst = false;
                            cont = false;
                            break;
                        default:
                            Console.Write("\b \b");
                            break;
                    }
                } while (cont);
                cont = true;
                Console.Clear();
                Console.WriteLine($"Starting {difficultyString} game. You are {player.ToString().ToUpper()[0]}. Enter Q to quit at any time.");
                do
                {
                    if (playFirst)
                    {
                        Console.Write("Where would you like to move? ");
                        Console.WriteLine();
                        prettyBoard = ConvertBoard(board, player);
                        PrintPrettyBoard(prettyBoard);
                        totals = UpdateTotals(board);
                        gameStatus = EndGameCheck(totals);
                        if (gameStatus[0] == true)
                        {
                            string winLose = "draw";
                            if (gameStatus[1] == true)
                            {
                                winLose = "win";
                            }
                            else if (gameStatus[1] == false)
                            {
                                winLose = "lose";
                            }
                            Console.SetCursorPosition(0, 0);
                            WriteLineInColor($"You {winLose}! Play again? Y/N ", ConsoleColor.Cyan);
                            Console.SetCursorPosition(26, 0);
                            invalidInput = true;
                            do
                            {
                                char pressed = Console.ReadKey().KeyChar;
                                switch (pressed)
                                {
                                    case 'y':
                                        cont = false;
                                        invalidInput = false;
                                        goto end;
                                    case 'n':
                                        invalidInput = false;
                                        cont = false;
                                        game = false;
                                        goto end;
                                    default:
                                        Console.Write("\b \b");
                                        break;
                                }
                            } while (invalidInput);
                        }
                        Console.SetCursorPosition(29, 1);
                        Console.CursorVisible = true;
                        invalidInput = true;
                        do
                        {
                            input = (Console.ReadLine()).ToLower();
                            if (input == "q")
                            {
                                cont = false;
                                game = false;
                                goto end;
                            }

                            else if (input.Length > 2 || input.Length < 2)
                            {
                                Console.SetCursorPosition(0, 0);
                                Console.Write(new String(' ', Console.BufferWidth - 1));
                                Console.SetCursorPosition(0, 0);
                                WriteLineInColor("Enter two characters!", ConsoleColor.Red);
                                Console.SetCursorPosition(29, 1);
                                Console.Write(new String(' ', Console.BufferWidth - 29));
                                Console.SetCursorPosition(29, 1);
                            }
                            else
                            {
                                int x;
                                int y;
                                if (int.TryParse(input[1].ToString(), out x))
                                {
                                    if (x > 3)
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        WriteLineInColor(
                                            $"Invalid input! Please enter a letter A-C followed by a number 1-3!",
                                            ConsoleColor.Red);
                                        Console.SetCursorPosition(29, 1);
                                        Console.Write(new String(' ', Console.BufferWidth - 29));
                                        Console.SetCursorPosition(29, 1);
                                        continue;
                                    }
                                    switch (input[0])
                                    {
                                        case 'a':
                                            y = 0;
                                            break;
                                        case 'b':
                                            y = 1;
                                            break;
                                        case 'c':
                                            y = 2;
                                            break;
                                        default:
                                            Console.SetCursorPosition(0, 0);
                                            WriteLineInColor(
                                                $"Invalid input! Please enter a letter A-C followed by a number 1-3!",
                                                ConsoleColor.Red);
                                            Console.SetCursorPosition(29, 1);
                                            Console.Write(new String(' ', Console.BufferWidth - 29));
                                            Console.SetCursorPosition(29, 1);
                                            continue;
                                    }
                                    if (board[y, x - 1] == 0)
                                    {
                                        board[y, x - 1] = 3;
                                        invalidInput = false;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        WriteLineInColor(
                                            $"You can't overwrite already made moves!",
                                            ConsoleColor.Red);
                                        Console.SetCursorPosition(29, 1);
                                        Console.Write(new String(' ', Console.BufferWidth - 29));
                                        Console.SetCursorPosition(29, 1);
                                    }

                                }
                                else
                                {
                                    Console.SetCursorPosition(0, 0);
                                    WriteLineInColor(
                                        $"Invalid input! Please enter a letter A-C followed by a number 1-3!",
                                        ConsoleColor.Red);
                                    Console.SetCursorPosition(29, 1);
                                    Console.Write(new String(' ', Console.BufferWidth - 29));
                                    Console.SetCursorPosition(29, 1);
                                }
                            }
                        } while (invalidInput);

                        totals = UpdateTotals(board);
                        prettyBoard = ConvertBoard(board, player);
                        Console.Clear();
                        Console.CursorVisible = false;
                        WriteLineInColor($"You played {char.ToUpper(input[0])}{input[1]}", ConsoleColor.Cyan);
                        Console.Write(".");
                        System.Threading.Thread.Sleep(200); //Pause for thinking effect.
                        Console.Write(".");
                        System.Threading.Thread.Sleep(200);
                        Console.Write(".");
                        System.Threading.Thread.Sleep(200);
                        Console.WriteLine();
                        PrintPrettyBoard(prettyBoard);
                        gameStatus = EndGameCheck(totals);
                        if (gameStatus[0] == true)
                        {
                            string winLose = "draw";
                            if (gameStatus[1] == true)
                            {
                                winLose = "win";
                            }
                            else if (gameStatus[1] == false)
                            {
                                winLose = "lose";
                            }
                            Console.SetCursorPosition(0, 0);
                            WriteLineInColor($"You {winLose}! Play again? Y/N ", ConsoleColor.Cyan);
                            Console.SetCursorPosition(26, 0);
                            invalidInput = true;
                            do
                            {
                                char pressed = Console.ReadKey().KeyChar;
                                switch (pressed)
                                {
                                    case 'y':
                                        cont = false;
                                        invalidInput = false;
                                        goto end;
                                    case 'n':
                                        invalidInput = false;
                                        cont = false;
                                        game = false;
                                        goto end;
                                    default:
                                        Console.Write("\b \b");
                                        break;
                                }
                            } while (invalidInput);
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    bestMove = GetBestMove(totals, board, difficulty);
                    board[bestMove[0], bestMove[1]] = 5;
                    string computerMove = "";
                    switch (bestMove[0])
                    {
                        case 0:
                            computerMove = "A" + (bestMove[1] + 1).ToString();
                            break;
                        case 1:
                            computerMove = "B" + (bestMove[1] + 1).ToString();
                            break;
                        case 2:
                            computerMove = "C" + (bestMove[1] + 1).ToString();
                            break;
                    }
                    Console.Clear();
                    if(cont)
                    WriteLineInColor($"Computer played {computerMove}.", ConsoleColor.Cyan);
                    end:
                    if(!cont)
                    Console.Clear();
                    playFirst = true;
                } while (cont);
            } while (game);
        }
        /// <summary>
        /// <para>Takes the board and returns values for each straight line row.</para>
        /// <para>[0,0] = sum board[0,0] to board[0,2] | [0,1] = sum board[1,0] to board[1,2] | [0,2] = sum board[2,0] to board[2,2], etc.</para>
        /// </summary>
        /// <param name="board"></param>
        /// <returns>int[[3,3]</returns>
        private static int[,] UpdateTotals(int[,] board)
        {
            int[,] totals = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                totals[0, i] = board[i, 0] + board[i, 1] + board[i, 2];
            }
            for (int i = 0; i < 3; i++)
            {
                totals[1, i] = board[0, i] + board[1, i] + board[2, i];
            }
            totals[2, 0] = board[0, 0] + board[1, 1] + board[2, 2];
            totals[2, 1] = board[0, 2] + board[1, 1] + board[2, 0];
            return totals;
        }
        /// <summary>
        /// <para>Checks the passed totals array for the best move, and passes the index coordinates back.</para>
        /// <para>Passes difficulty to determine if the computer should make a non optimal move.</para>
        /// </summary>
        /// <param name="totals"></param>
        /// <param name="board"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        private static int[] GetBestMove(int[,] totals, int[,] board, int diff)
        {
            int max = 0;
            int[] maxLineIndex = new int[2];
            int[] bestMove = new int[2];
            Random random = new Random();
            int randomMove = random.Next(1, 101);
            if (randomMove>diff)
            {
                int possibleMoves = 0;
                foreach (int item in board)
                {
                    if (item == 0)
                    { possibleMoves++; }
                }
                int[,] possibleMovesIndexes = new int[possibleMoves, 2];
                for (int q = 0; q < possibleMoves; q++) { possibleMovesIndexes[q, 0] = 9; }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            for (int k = 0; k < possibleMoves; k++)
                            {
                                if (possibleMovesIndexes[k, 0] == 9)
                                {
                                    possibleMovesIndexes[k, 0] = i;
                                    possibleMovesIndexes[k, 1] = j;
                                }
                            }
                        }
                    }
                }
                bestMove[0] = possibleMovesIndexes[random.Next(0,possibleMoves), 0];
                bestMove[1] = possibleMovesIndexes[random.Next(0, possibleMoves), 1];
                return bestMove;
            }    

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 2 && j == 2 && max == 3 && board[1, 1] == 3 && board[0,0] == 0) 
                    {
                        bestMove[0] = 0;
                        bestMove[1] = 0;
                        return bestMove;
                    }
                    else if (board[1, 1] == 0 && randomMove < diff&& max <6)
                    {
                        bestMove[0] = 1;
                        bestMove[1] = 1;
                        return bestMove;
                    }
                    else if (totals[i, j] > max && totals[i, j] != 8 && !(totals[i, j] > 10))
                    {
                        max = totals[i, j];
                        maxLineIndex[0] = i;
                        maxLineIndex[1] = j;
                    }                 
                }
            }
            if (maxLineIndex[0] == 0)
            {
                switch (maxLineIndex[1])
                {
                    case 0:
                        bestMove[0] = 0;
                        bestMove[1] = board[0, 0] == 0 ? 0 : board[0, 2] == 0 ? 2 : 1;
                        break;
                    case 1:
                        bestMove[0] = 1;
                        bestMove[1] = board[1, 0] == 0 ? 0 : board[1, 2] == 0 ? 2 : 1;
                        break;
                    case 2:
                        bestMove[0] = 2;
                        bestMove[1] = board[2, 0] == 0 ? 0 : board[2, 2] == 0 ? 2 : 1;
                        break;
                }

            }
            else if (maxLineIndex[0] == 1)
            {
                switch (maxLineIndex[1])
                {
                    case 0:
                        bestMove[0] = board[0, 0] == 0 ? 0 : board[1, 0] == 0 ? 1 : 2;
                        bestMove[1] = 0;
                        break;
                    case 1:
                        bestMove[0] = board[0, 1] == 0 ? 0 : board[1, 1] == 0 ? 1 : 2;
                        bestMove[1] = 1;
                        break;
                    case 2:
                        bestMove[0] = board[0, 2] == 0 ? 0 : board[1, 2] == 0 ? 1 : 2;
                        bestMove[1] = 2;
                        break;
                }

            }
            else if (maxLineIndex[0] == 2)
            {
                switch (maxLineIndex[1])
                {
                    case 0:
                        bestMove[0] = board[0, 0] == 0 ? 0 : board[1, 1] == 0 ? 1 : 2;
                        bestMove[1] = board[0, 0] == 0 ? 0 : board[1, 1] == 0 ? 1 : 2;
                        break;
                    case 1:
                        bestMove[0] = board[0, 2] == 0 ? 0 : board[1, 1] == 0 ? 1 : 2;
                        bestMove[1] = board[0, 2] == 0 ? 2 : board[1, 1] == 0 ? 1 : 0;
                        break;
                    case 2:
                        //This index is unused and should never be seen. 
                        Console.WriteLine($"You shouldn't be seeing this!");
                        break;
                }
            }
            return bestMove;
        }
        private static char[,] ConvertBoard(int[,] board, char player)
        {
            char[,] charArray = new char[3, 3];
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (board[i, j])
                    {
                        case 0: charArray[i, j] = '#';break;
                        case 3: charArray[i, j] = player.ToString().ToUpper()[0]; break;
                        case 5: charArray[i, j] = player=='x'?'O':'X'; break;
                    }
                }
            }
            return charArray;
        }
        private static void PrintPrettyBoard(char[,] prettyBoard)
        {
            Console.WriteLine($"  1 2 3");
            Console.WriteLine($"A {prettyBoard[0, 0]} {prettyBoard[0, 1]} {prettyBoard[0, 2]}");
            Console.WriteLine($"B {prettyBoard[1, 0]} {prettyBoard[1, 1]} {prettyBoard[1, 2]}");
            Console.WriteLine($"C {prettyBoard[2, 0]} {prettyBoard[2, 1]} {prettyBoard[2, 2]}");
        }
        private static bool?[] EndGameCheck(int[,] totals)
        {
            int moves = 0;
            for (int i = 0;i<3;i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (totals[i,j] == 9)
                    {
                        return new bool?[] { true, true };
                    }
                    else if (totals[i, j] == 15)
                    {
                        return new bool?[] { true, false };
                    }
                    else if ((totals[i, j] == 0 && !(i==2 && j==2) )|| totals[i, j] == 3 || totals[i, j] == 5 || totals[i, j] == 6 || totals[i, j] == 10 || totals[i, j] == 8)
                    {
                        moves++;
                    }
                }

            }
            if(moves==0)
                return new bool?[] { true, null };
            else
                return new bool?[] {false, false};
        }
    }

}
