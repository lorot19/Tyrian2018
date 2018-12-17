using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tyrian
{
    /// <summary>
    /// Main program loop. Creates Game window and initialize all objects.
    /// Contains almost all objects include timers.
    /// 
    /// Author: Tomas Lorinc
    /// Version: 1.0 Stable
    /// </summary>
    public partial class Form1 : Form
    {
        public Phisics phisics;
        public Render render;
        public Collision colision;
        public Game game;
        public Sounds sound;
        public Bitmap start_img = new Bitmap(Tyrian.Properties.Resources.strt);

        public bool active_game = false;

        /// <summary>
        /// Initialize new game, restart all objects in game.
        /// </summary>
        public void StartGame()
        {
            game = new Game(label_score, label_lives);
            render = new Render(pictureBox1, game);
            render.InitAsteroids();
            render.InitBullet();
            phisics = new Phisics(render);
            sound = new Sounds();
            colision = new Collision(render, game, sound);
            Cursor.Hide();
            sound.Start();
            active_game = true;
        }

        /// <summary>
        /// Initialize Form window and all child components and adjust game window size.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(Game.width, Game.height);
        }

        /// <summary>
        /// Used as SysTick. If active_game flag is True colision detector, 
        /// phisics and next frame are triggered with speed specified by interval parameter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (active_game)
            {
                phisics.NextFrame();
                render.NextFrame(pictureBox1);
                colision.Detect();
            }
        }
        /// <summary>
        /// Render textures and intro and game over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(start_img, (Game.width / 2) - 400, 150, 800, 600);
            if (active_game)
            {
                panel1.Visible = false;
                render.Display(e);
                if (game.lives <= 0)
                {
                    render.game_over.visible = true;
                    render.player.visible = false;
                    phisics.NextFrame();
                    render.NextFrame(pictureBox1);
                    render.Display(e);
                    //Cursor.Show(); //nefunguje poriadne  
                }
            }
        }
        /// <summary>
        /// Track mouse and move player according to mouse coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (active_game)
            {
                if (game.lives>0)
                {
                    phisics.MovePlayer(e);
                }  
            }          
        }

        /// <summary>
        /// Track mouse click than fire or start new game depending on active_game flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (active_game)
            {
                if (game.lives > 0)
                {
                    sound.Fire();
                    phisics.Fire(e);
                }
                else
                {
                    StartGame();
                }               
            }
            else if(!active_game)
            {
                StartGame();
            }
        }

        /// <summary>
        /// Close application when ESC is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Start new game after mouse button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;
            StartGame();
        }
    }
}
