using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{

    public class ChessBoardManager
    {
        #region Properties
        private Panel chessBoard;

        public Panel ChessBoard { get => this.chessBoard; set => this.chessBoard = value; }


        private List<Player> Player;

        public List<Player> Player1 { get => Player; set => Player = value; }

        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        public PictureBox PlayerMark { get => playerMark; set => playerMark = value; }

        private TextBox playerName;

        private PictureBox playerMark;

        private List<List<Button>> Matrix;

        private event EventHandler playerMarked;

        public event EventHandler PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }
        private event EventHandler endedGame;

        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }

        #endregion



        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark)
        {
            this.chessBoard = chessBoard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            this.Player = new List<Player>()
            {
                new Player("Trần Đức Trí", Image.FromFile(Application.StartupPath + "\\Image\\x.jpg")),
                new Player("Nguyễn Lệ Thương", Image.FromFile(Application.StartupPath + "\\Image\\o.jpg"))
            };

            CurrentPlayer = 0;
            ChangePlayer();
        }

        #endregion



        #region Method

        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();
            Matrix = new List<List<Button>>();
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT_NUM; i++)
            {
                Matrix.Add(new List<Button>());
                Button oldButton = new Button()
                {
                    Width = 0,
                    Height = 0,
                    Location = new Point(0, i * Cons.CHESS_HEIGHT)
                };
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH_NUM; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    Matrix[i].Add(btn);
                    btn.Click += Btn_Click;
                    chessBoard.Controls.Add(btn);
                    oldButton = btn;
                }
            }
        }

        public void EndGame()
        {
            if (endedGame!=null)
            {
                endedGame(this,new EventArgs());
            }
        }
        private Point GetChessPoint(Button btn)
        {
            
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal,vertical);
            return point;
        }
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X;i>=0;i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                {
                    break;
                }
            }
            int countRight = 0;
            for (int i = point.X+1; i <Cons.CHESS_BOARD_WIDTH_NUM; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }
            return countLeft + countRight == 5;
        }
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countUp = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countUp++;
                }
                else
                {
                    break;
                }
            }
            int countDown = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT_NUM; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countDown++;
                }
                else
                {
                    break;
                }
            }
            return countUp + countDown == 5;
        }
        private bool isEndPrimary(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countUp = 0;


            for (int i=0;i<=point.X;i++)
            {
                if (point.X-i <0 || point.Y-i<0)
                {
                    break;
                }
                if (Matrix[point.Y-i][point.X-i].BackgroundImage == btn.BackgroundImage)
                {
                    countUp++;
                }
                else
                {
                    break;
                }
            }
            int countDown = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH_NUM-point.X; i++)
            {
                if (point.Y+i>=Cons.CHESS_BOARD_HEIGHT_NUM || point.X+i >=Cons.CHESS_BOARD_WIDTH_NUM)
                { 
                    break;
                }
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countDown++;
                }
                else
                {
                    break;
                }
            }
            return countUp + countDown == 5;
        }
        private bool isEndSub(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countUp = 0;


            for (int i = 0; i <= point.Y; i++)
            {
                if (point.X + i >= Cons.CHESS_BOARD_WIDTH_NUM || point.Y - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countUp++;
                }
                else
                {
                    break;
                }
            }
            int countDown = 0;
            for (int i = 1; i <=  point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT_NUM || point.X - i <0)
                {
                    break;
                }
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countDown++;
                }
                else
                {
                    break;
                }
            }
            return countUp + countDown == 5;
        }
        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn);
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn= sender as Button;

            if (btn.BackgroundImage != null)
                return;
            else
            {
                Mark(btn);
                ChangePlayer();
                if (playerMarked != null)
                {
                    playerMarked(this, new EventArgs());
                }
                if (isEndGame(btn))
                {
                    EndGame();         
                }


            }  
        }
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            playerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion

    }
}
