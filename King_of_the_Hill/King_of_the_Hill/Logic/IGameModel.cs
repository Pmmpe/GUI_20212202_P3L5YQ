using System;

namespace King_of_the_Hill.Logic
{
    public interface IGameModel
    {
        double PosX { get; set; }
        double PosY { get; set; }

        event EventHandler Changed; 
    }
}