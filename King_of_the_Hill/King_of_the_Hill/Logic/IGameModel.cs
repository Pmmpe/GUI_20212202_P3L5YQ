namespace King_of_the_Hill.Logic
{
    using System;
    public interface IGameModel
    {
        double PosX { get; set; }
        double PosY { get; set; }

        event EventHandler Changed;
    }
}
