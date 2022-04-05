namespace King_of_the_Hill.Logic
{
    using System;
    using System.Drawing;

    public interface IGameModel
    {
        Rectangle playerRect { get; }

        event EventHandler Changed;
    }
}
