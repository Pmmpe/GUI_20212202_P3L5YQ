namespace King_of_the_Hill.Logic
{
    using System;
    using System.IO;
    using System.Windows.Media;
    public class SoundLogic
    {
        //two kinds of sound
        // - backgroundmusic (stoppable)
        //      - in menu
        //      - in game
        // - foreground sounds (not stoppable)
        //      - in menu (buttons, etc)
        //      - in game (players, objects, events)
        
        public enum PlayerSounds { sword_cut, bow_hit, scream, armor_hit }
        public enum MenusSounds { button_click, game_start}
        public enum Action { start, stop}
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

        public void PlayActionSound( PlayerSounds sound)
        {
            string asset = "";
            foregroundsoundplayer = new MediaPlayer(); //minden sound-hoz kulön soundplayer kell
            switch (sound)
            {
                case PlayerSounds.sword_cut:
                    asset = "sword-cutting-flesh.wav";
                    break;
                case PlayerSounds.bow_hit:
                    asset = "braqoon_arrow-damage.wav";
                    break;
                case PlayerSounds.scream:
                    asset = "effort-man-voice.wav";
                    break;
                case PlayerSounds.armor_hit:
                    asset = "sword-strikes-armor.wav";
                    break;
                default:
                    break;
            }
            foregroundsoundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", asset), UriKind.RelativeOrAbsolute));
            foregroundsoundplayer.Play();

        }

        public void PlayActionSound(MenusSounds sound)
        {
            string asset = "";
            foregroundsoundplayer = new MediaPlayer(); //minden sound-hoz kulön soundplayer kell
            switch (sound)
            {
                case MenusSounds.button_click:
                    asset = "select-click.wav";
                    break;
                case MenusSounds.game_start:
                    asset = "quick-win-video-game-notification.wav";
                    break;
                default:
                    break;
            }
            foregroundsoundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", asset), UriKind.RelativeOrAbsolute));
            foregroundsoundplayer.Play();

        }
    }
}
