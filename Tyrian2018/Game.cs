using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tyrian
{
    /// <summary>
    /// This Class is used for updating game parameters and scenario settings.
    /// 
    /// <width> and <height> 
    ///     it is possible to set game window size.
    /// <asteroid_count> 
    ///     is number of asteroids (instances of Sprite.cs) 
    ///     Note: asteroids are recycled during game. 
    ///     By increasing value you can increase density of asteroids.
    /// <bullet_count>
    ///     is number of bullets available and rendered at game. It means that 
    ///     Note: bullets are recycled during game.
    /// <bullet_speed>
    ///     is constant speed of bullets. By increasing you can theorethicaly increase
    ///     damage and increase dynamic of game.
    ///     Note: Value must be negative because game flow is from the top to the bottom.
    ///     by negative value we will force opposite direction of flow.
    /// <asteroid_max_speed>
    ///     is maximum asteroid speed. Every asteroid gets random speed when constructor
    ///     of Sprite is called. This speed is not changed when steroid is recycled.
    ///     By increasing value we are able to speedup game.
    /// <lives>
    ///     Number of lives available for the player when game starts. After restarting game
    ///     number of lives are restored to this value.
    /// <score>
    ///     Total score earned during game. Of course it starts with zero.
    /// </summary>
    public class Game
    {
        public const int width = 1600;
        public const int height = 1000;
        public const int asteroid_count = 5;
        public const int bullet_count = 10;
        public const int bullet_speed = -80;
        public const int asteroid_max_speed = 20;
        public int lives = 5;
        public int score = 0;
        private Label label_score;
        private Label label_lives;

        /// <summary>
        /// This function is used as game constructors.
        /// Purpose is both reset  and update score and lives at game window
        /// and hide mouse cursore because it is replaced by fighter sprite.
        /// </summary>
        /// <param name="label_score">Access to form label showing actual score</param>
        /// <param name="label_lives">Access to form label showing actual lives</param>
        public Game(Label label_score, Label label_lives)
        {
            this.label_score = label_score;
            this.label_lives = label_lives;
            label_lives.Text = lives.ToString();
            label_score.Text = score.ToString();
            Cursor.Hide();
        }
        /// <summary>
        /// This function is called by instance of collision detector. Whenever collision
        /// is detected this function is called with parameter describing type of collision.
        /// Function change global variable of score and lives and update actual value on game window.
        /// </summary>
        /// <param name="type">Parameter describing type of collision</param>
        public void UpdateScore(string type)
        {
            switch(type)
            {
                case "shot":
                    {
                        score += 10;
                        label_score.Text = score.ToString();
                        break;
                    }
                case "death":
                    {
                        lives--;
                        label_lives.Text = lives.ToString();
                        break;
                    }
            }            
        }     
    }
}
