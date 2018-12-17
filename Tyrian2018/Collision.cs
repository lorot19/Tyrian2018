using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tyrian
{
    /// <summary>
    /// This class is used for collision detection.
    /// Basic behaviour is that it compare player position with all rendered objects (both bullets and asteroids)
    /// If collision is detected,
    ///     UpdateScore() is called to update lives and score
    ///     sound.Explosion(); is called to play sound of explosion
    ///     Updated visibility flags for asteroid, bullet, player and explosion.
    /// </summary>
    public class Collision
    {
        private Render render;
        private Game game;
        private Sounds sound;

        /// <summary>
        /// Constructor of collision detector. Used for accessing instances of different classes.
        /// </summary>
        /// <param name="render">Access to the render object</param>
        /// <param name="game">Access to the game object</param>
        /// <param name="sound">Access to the sound object</param>
        public Collision(Render render, Game game, Sounds sound)
        {
            this.render = render;
            this.game = game;
            this.sound = sound;
        }
        /// <summary>
        /// Detects colision between asteroids, bullets and player. 
        /// Than plays sound, hide death object and update score and lives.
        /// </summary>
        public void Detect()
        {
            for (int j = 0; j < Game.asteroid_count; j++)
            {
                //check position of [j] asteroid with all bullets
                for (int i = 0; i < Game.bullet_count; i++)
                {
                    if (
                        ((render.bullet[i].position_x +(render.bullet[i].width/2)) >= render.asteroid[j].position_x) &
                        ((render.bullet[i].position_x + (render.bullet[i].width / 2))<= (render.asteroid[j].position_x + render.asteroid[j].width)) &
                        (render.bullet[i].position_y >= render.asteroid[j].position_y) &
                        (render.bullet[i].position_y <= (render.asteroid[j].position_y + render.asteroid[j].height))
                        )                       
                    {
                        render.asteroid[j].position_y = Game.height; //put asteroid outside game window = hide it
                        render.bullet[i].position_y = 0; //put bullet outside game window = hide it
                        game.UpdateScore("shot"); //update score
                        render.bullet[i].visible = false; //hide bullet
                        render.explosion.position_y = render.bullet[i].position_y-render.explosion.height/2; //set explosion position to colision with bullet position
                        render.explosion.position_x = render.bullet[i].position_x - render.bullet[i].width/2; //set explosion position to colision with bullet position
                        render.explosion.visible = true; //show explosion
                    }
                }
                //check position of [j] asteroid with player
                if ( 
                   ((render.player.position_x + (render.player.width / 2)) >= render.asteroid[j].position_x) &
                   ((render.player.position_x + (render.player.width / 2)) <= (render.asteroid[j].position_x + render.asteroid[j].width)) &
                   (render.player.position_y >= render.asteroid[j].position_y) &
                   (render.player.position_y <= (render.asteroid[j].position_y + render.asteroid[j].height))
                   )

                {
                    sound.Explosion(); //play explosion sound
                    render.asteroid[j].position_y = Game.height; //put asteroid outside game window = hide it
                    game.UpdateScore("death"); //update lives
                    render.explosion.position_y = render.player.position_y - render.explosion.height / 2; //set explosion position to colision with player position
                    render.explosion.position_x = render.player.position_x - render.player.width / 2; //set explosion position to colision with player position
                    render.explosion.visible = true; //show explosion
                }
            }
        }  
    }
}
