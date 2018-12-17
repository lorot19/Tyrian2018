using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyrian
{
  /// <summary>
  /// This class is used for playing sounds during game.
  /// At this time only one object is used so only one sound effect can by played at the time.
  /// It can be improved by threading.
  /// </summary>
    public class Sounds
    {
        private WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public Sounds()
        {
        }
        /// <summary>
        /// Play fire sound (used when bullet is released)
        /// </summary>
        public void Fire()
        {
            wplayer.URL = "fire.wav";
            wplayer.controls.play();
        }
        /// <summary>
        /// Play explosion sound (used at collision with player)
        /// </summary>
        public void Explosion()
        {
            wplayer.URL = "explosion.wav";
            wplayer.controls.play();
        }
        /// <summary>
        /// Play start sound. (used when game starts or restarts)
        /// </summary>
        public void Start()
        {
            wplayer.URL = "start.wav";
            wplayer.controls.play();
        }
        /// <summary>
        /// Play game over sound. (used when player lives meet zero)
        /// </summary>
        public void BackGroundSound()
        {
            wplayer.URL = "gameover.wav";
            wplayer.controls.play();
        }
    }   
}
