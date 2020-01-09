using System;
using SplashKitSDK;

public class Bullet
{
    //bullet bitmap
    private Bitmap _BulletBitmap;
    private double X { get; set; }
    private double Y { get; set; }

    private Vector2D Velocity { get; set; }

    public int Width
    {
        get { return _BulletBitmap.Width; }
    }
    //read only property
    public int Height
    {
        get { return _BulletBitmap.Height; }
    }


    public Bullet(Window gameWindow, Player _Player)
    {
        _BulletBitmap = new Bitmap("Bullet", "Fire.png");

        //position the Bullet
        //create the bullet only if mouse is clicked!
        //the bullet needs to originate from player and travel towards where the mouse is clicked
        const int SPEED = 8;
        //initiate the bullet from center of the player

        X = _Player.X + _Player.Width/2;
        Y = _Player.Y + _Player.Height/2;
        //Get a point to track the bullet
        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        //get the location of the mouse
        Point2D mousePt = SplashKit.MousePosition();

        //Calculate the direction to head (from player to mouse)
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, mousePt));

        //Set the speed and assign the velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public void Draw()
    {
        SplashKit.ProcessEvents();
        SplashKit.DrawBitmap(_BulletBitmap, X, Y);
    }
     public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
    }
    public bool IsOffscreen(Window screen)
    {
        return (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height);
    }
    public bool BulletCollidedWith(Robot robot)
    {
        return _BulletBitmap.CircleCollision(X, Y, robot.CollisionCircle);
    }

}
