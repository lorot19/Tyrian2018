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
    /// Purpose of this class is manage movement and create effect of animation. 
    /// Position of every sprite is recalculated according to Sprite.cs and Game.cs 
    /// parameters every next frame triggered by Timer1_Tick. according to Sprite.cs 
    /// and Game.cs parameters
    /// </summary>
    public class Phisics
    {
        private Render render;
        Random r = new Random();

        /// <summary>
        /// Constructor of phisics object.
        /// </summary>
        /// <param name="render">Access to Render.cs object </param>

        public Phisics(Render render)
        {
            this.render = render;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="speed"></param>
        public void Animate(Sprite p)
        {
            // Sprite is outside game window under bottom edge means that it is ASTEROID
            if ((p.speed > 0) & (p.position_y > Game.height)) 
            {
                p.position_y = -20;
                p.position_x = r.Next(p.width, (Game.width - p.width));
                p.visible = true;
            }
            // Sprite is outside game window befor top edge means that it is BULLET
            else if ((p.speed < 0) & (p.position_y < 0))
            {
                p.visible = false;
            }
            p.position_y += 1 * p.speed; 
        }
        /// <summary>
        /// Change position of player sprite according to mouse coordinates.
        /// </summary>
        /// <param name="mouse">mouse coordinates</param>
        public void MovePlayer(MouseEventArgs mouse)
        {
            render.player.position_x = mouse.X - (render.player.width / 2);
            render.player.position_y = mouse.Y - (render.player.height / 2);
        }
        /// <summary>
        /// Set visibility and position of next available bullet sprite to mouse coordinates 
        /// </summary>
        /// <param name="mouse">mouse coordinates</param>
        public void Fire(MouseEventArgs mouse)
        {
            for (int i = 0; i < Game.bullet_count; i++)
            {
                if (render.bullet[i].visible==false)
                {
                    render.bullet[i].position_x = (mouse.X - (render.player.width / 2));
                    render.bullet[i].position_y = mouse.Y - (render.bullet[i].height / 2);
                    render.bullet[i].visible = true;
                    render.bullet[i].life = true;
                    break;
                }

            }
        }

        /// <summary>
        /// Recalculate position of every sprite accornidn to Game.cs constants
        /// </summary>
        public void NextFrame()
        {
            for (int i = 0; i < Game.asteroid_count; i++)
            {
                Animate(render.asteroid[i]);
            }
            

            for (int i = 0; i < Game.bullet_count; i++)
            {
                Animate(render.bullet[i]);
            }

            for (int i = 1; i< 400; i++)
            {
                render.explosion.width = i;
                render.explosion.height = i;
            }
            render.explosion.visible = false;

            
        }
    }
}
