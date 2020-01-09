using System;
using SplashKitSDK;

public class Player
{
    private Bitmap _PlayerBitmap;
    //These are auto properties dont need fields
    //X and Y coordinates for Player
    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Quit { get; private set; }

    public int Score;
    public int Lives = 5;

    //read only
    public int Width
    {
        get { return _PlayerBitmap.Width; }
    }
    //read only property
    public int Height
    {
        get { return _PlayerBitmap.Height; }
    }

    //add constructor which creates a new bitmap, sets the player X and player Y
    public Player(Window gameWindow)
    {
        Quit = false;
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;
    }

    public void Draw()
    {
        SplashKit.ProcessEvents();
        SplashKit.DrawBitmap(_PlayerBitmap, X, Y);
    }
    public void HandleInput()
    {

        const int SPEED = 5;

        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X = X + SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X = X - SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y = Y - SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y = Y + SPEED;
        }
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }
        if (Lives <= 0)
        {
            SplashKit.ClearScreen(Color.Black);
            SplashKit.DrawText("GAME OVER! ", Color.White, 300, 300);
            //Quit = true;
        }
    }
    public void StayOnWindow(Window gameWindow)
    {
        const int GAP = 10;

        if (X < GAP)
        {
            X = GAP;
        }
        else if (X > gameWindow.Width - GAP - Width)
        {
            X = gameWindow.Width - GAP - Width;
        }
        else if (Y < GAP)
        {
            Y = GAP;
        }
        else if (Y > gameWindow.Height - GAP - Height)
        {
            Y = gameWindow.Height - GAP - Height;
        }

    }
    public bool CollidedWith(Robot robot)
    {
        return _PlayerBitmap.CircleCollision(X, Y, robot.CollisionCircle);
    }

}