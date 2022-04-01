namespace King_of_the_Hill.Logic
{
    using System;
    using System.Drawing;

    public interface IGameModel
    {
        double PosX { get; set; }
        double PosY { get; set; }

        Rectangle playerRect { get; set; }

        event EventHandler Changed;
    }
}
