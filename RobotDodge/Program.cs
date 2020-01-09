using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Robot Dodge", 800, 420);
        RobotDodge game = new RobotDodge(gameWindow);

        while (!gameWindow.CloseRequested)// & !game.Quit)
        {
            game.HandleInput();
            game.Draw();
            game.Update();
        }
    }
}


