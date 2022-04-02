using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace King_of_the_Hill.Logic
{
    public class SoundLogic
    {

        public enum PlayerSounds { sword_hit, bow_hit, scream, armor_hit }
        MediaPlayer backgroundsoundplayer;
        MediaPlayer foregroundsoundplayer;
        public void BackgroundMusicMenu(string action)
        {
            
            if (action == "start")
            {
                backgroundsoundplayer = new MediaPlayer();
                backgroundsoundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", "hatterzene_lol.mp3"), UriKind.RelativeOrAbsolute));
                backgroundsoundplayer.MediaEnded += Soundplayer_MediaEnded;
                backgroundsoundplayer.Play();
            }
            else if (action == "stop")
            {
                backgroundsoundplayer.Stop();
            }
        }

        private void Soundplayer_MediaEnded(object? sender, EventArgs e) //looping sound
        {
            backgroundsoundplayer.Position = TimeSpan.Zero;
            backgroundsoundplayer.Play();
        }

        public void BackgroundMusicGame(string action)
        {
            if (action == "start")
            {
                backgroundsoundplayer = new MediaPlayer();
                backgroundsoundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", "game_bcg_music.mp3"), UriKind.RelativeOrAbsolute));
                backgroundsoundplayer.MediaEnded += Soundplayer_MediaEnded;
                backgroundsoundplayer.Play();
            }
        }

        public void PlayerSound(string action, PlayerSounds sound)
        {
            string asset = "";
            backgroundsoundplayer = new MediaPlayer();
            switch (sound)
            {
                case PlayerSounds.sword_hit:
                    break;
                case PlayerSounds.bow_hit:
                    break;
                case PlayerSounds.scream:
                    asset = "effort-man-voice.wav";
                    break;
                case PlayerSounds.armor_hit:
                    asset = "sword-strikes-armor";
                    break;
                default:
                    break;
            }
            backgroundsoundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", asset), UriKind.RelativeOrAbsolute));
            backgroundsoundplayer.Play();
        }
    }
}
