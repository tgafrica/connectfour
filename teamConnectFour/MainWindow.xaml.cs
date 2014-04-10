using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace teamConnectFour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public const int gameWidth = 7;
        public const int gameHeight = 6;
        Circle[,] gameBoard = new Circle[gameWidth, gameHeight];
        int player = 1;

        public MainWindow()
        {
            InitializeComponent();

            /************************************************************************/
            /*                  Create and draw a blank game board                  */
            /************************************************************************/
            // Create and draw a blank game board
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    Point circleLoc = new Point(((55 * x) + 10), ((55 * y) + 10));
                    gameBoard[x, y] = new Circle(50, Colors.White);
                    gameBoard[x, y].SetLocation((int)circleLoc.X, (int)circleLoc.Y);
                    gameBoard[x, y].Draw(gameCanvas);
                }
            }
        }

        // Do some action when the canvas is clicked on
        private void drawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /************************************************************************/
            /*           Set the column clicked on from the mouse location          */
            /************************************************************************/
            Point mouseLocation = e.GetPosition(this.gameCanvas);
            int playerColumn = (int)((mouseLocation.X - 10) / 55);

            /************************************************************************/
            /*        Keep the right edge in the bounds of the playing field        */
            /************************************************************************/
            if (playerColumn == 7)
                playerColumn = 6;

            /************************************************************************/
            /*  Place the game token for the current player in the selected column  */
            /************************************************************************/
            placeToken(playerColumn, player);

            /************************************************************************/
            /*                 Swap players after a token is placed                 */
            /************************************************************************/
            if (player == 1)
                player = 2;
            else
                player = 1;
        }

        /****************************************************************************/
        /*                      Redraw the game board (refresh)                     */
        /****************************************************************************/
        public void drawGameBoard()
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    Point circleLoc = new Point(((55 * x) + 10), ((55 * y) + 10));
                    gameBoard[x, y].SetLocation((int)circleLoc.X, (int)circleLoc.Y);
                    gameBoard[x, y].Draw(gameCanvas);
                }
            }
        }

        /****************************************************************************/
        /*                            Place a game token                            */
        /****************************************************************************/
        public void placeToken(int column, int player)
        {
            for (int y = 0; y < 6; y++)
            {
                if (gameBoard[column, y].GetColor() == Colors.White)
                {
                    /****************************************************************/
                    /*       Place a token for player 1 and check for the win       */
                    /****************************************************************/
                    if (player == 1)
                    {
                        gameBoard[column, y].SetColor(Colors.Red);
                        drawGameBoard();
                        if (checkWin(column, y))
                        {
                            MessageBox.Show("Player " + player.ToString() + " wins!");
                        }
                        break;
                    }
                    /****************************************************************/
                    /*       Place a token for player 2 and check for the win       */
                    /****************************************************************/
                    else if (player == 2)
                    {
                        gameBoard[column, y].SetColor(Colors.Black);
                        drawGameBoard();
                        if (checkWin(column, y))
                        {
                            MessageBox.Show("Player " + player.ToString() + " wins!");
                        }
                        break;
                    }
                }
            }
        }

        /****************************************************************************/
        /*               Check to see if the last token won the game                */
        /****************************************************************************/
        public bool checkWin(int column, int row)
        {
            int target = 3; // The maximum numbers of tokens to be in line,
                            // not counting the current token
            int count = 0;  // The count of tokens in a line
            int check = 0;  // The amount to add or remove from the token to check
            Color playerColor;

            /************************************************************************/
            /*                     1. Check the current player                      */
            /************************************************************************/
            if (player == 1)
                playerColor = Colors.Red;
            else
                playerColor = Colors.Black;

            /************************************************************************/
            /*                          2. Check vertical                           */
            /************************************************************************/

            do
            {
                count++;
            }
            while (row - count < 6 && row - count >= 0 &&
                        gameBoard[column, row - count].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            /************************************************************************/
            /*                         3. Check horizontal                          */
            /************************************************************************/

            /************************************************************************/
            /*                           3a. First left                             */
            /************************************************************************/
            do
            {
                count++;
            }
            while (column - count >= 0 &&
                        gameBoard[column - count, row].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            //Make sure you dont double count the center piece. Deduct it here.
            count -= 1;

            /************************************************************************/
            /*                           3b. Then right                             */
            /************************************************************************/
            do
            {
                count++;
                check++;
            }
            while (column + check < 7 &&
                        gameBoard[column + check, row].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            /************************************************************************/
            /*                         4. Check diagonal                            */
            /*                         Lefttop - rightbottom                        */
            /************************************************************************/

            /************************************************************************/
            /*                           4a. To the top                             */
            /************************************************************************/
            do
            {
                count++;
            }
            while (column - count >= 0 && row - count >= 0 &&
                gameBoard[column - count, row - count].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            //Make sure you dont double count the center piece. Deduct it here.
            count -= 1;

            /************************************************************************/
            /*                          4b. To the bottom                           */
            /************************************************************************/
            do
            {
                count++;
                check++;
            }
            while (column + check < 7 && row + check < 6 &&
                gameBoard[column + check, row + check].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            /************************************************************************/
            /*                         5. Check diagonal                            */
            /*                         Righttop - leftbottom                        */
            /************************************************************************/

            /************************************************************************/
            /*                           5a. To the top                             */
            /************************************************************************/
            do
            {
                count++;
            }
            while (column + count < 7 && row - count >= 0 &&
                gameBoard[column + count, row - count].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            // Make sure you dont double count the center piece. Deduct it here.
            count -= 1;

            /************************************************************************/
            /*                          5b. To the bottom                           */
            /************************************************************************/
            do
            {
                count++;
                check++;
            }
            while (column - check >= 0 && row + check < 6 &&
                gameBoard[column - check, row + check].GetColor() == playerColor);

            if (count > target)
            {
                return true;
            }

            return false;
        }

        /****************************************************************************/
        /*                           Clear the game board                           */
        /****************************************************************************/
    }
}
