using System;
using SplashKitSDK;
using System.IO;
using System.Collections.Generic;

/*
* class used to create an object that plays the role of the game itself
* keeps track of the player
* keeps track of the robot
*/
public class RobotDodge
{
    private Player _Player;
    private Window _GameWindow;
    private List<Robot> _Robots = new List<Robot>();
    private List<Robot> _removedRobots = new List<Robot>();

    private List<Bullet> _Bullets = new List<Bullet>();

    private List<Bullet> _removedBullets = new List<Bullet>();

    public Timer myTimer;

    //The heart Bitmap
    private Bitmap HeartBitmap = new Bitmap("Heart", "heart.png");

    /*
    * read only property
    * Ask the player if they have quit
    * returning the answer it gets from player
    */
    public bool Quit
    {
        get
        {
            return _Player.Quit;
        }
    }

    //constructor
    public RobotDodge(Window window)
    {
        _GameWindow = window;
        _Player = new Player(window);
        SplashKit.LoadMusic("background", "background.mp3");
        SplashKit.LoadSoundEffect("gameover", "gameover.wav");
        SplashKit.LoadSoundEffect("1", "1.wav");
        SplashKit.PlayMusic("background");
        myTimer = new Timer("My Timer");
        myTimer.Start();
    }
    public void HandleInput()
    {
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }
    public void Draw()
    {
        _GameWindow.Clear(Color.Black);
        foreach (Robot robot in _Robots)
        {
            robot.Draw();
        }
        _Player.Draw();
        foreach (Bullet bullet in _Bullets)
        {
            bullet.Draw();
        }
        DisplayHUD();
        if (_Player.Lives <= 0)
        {
            _GameWindow.Clear(Color.Black);
            Bitmap _GameOver = new Bitmap("Game Over", "gamover.png");
            SplashKit.DrawBitmap(_GameOver, 200, 100);
            SplashKit.StopMusic();
            SplashKit.PlaySoundEffect("gameover");
        }
        _GameWindow.Refresh(60);

    }
    public void Update()
    {
        CheckCollisions();
        _Player.Score = Convert.ToInt32(myTimer.Ticks / 1000);
        foreach (Robot robot in _Robots)
        {
            robot.Update();
        }
        //add random number of robots into the list
        double randomNumber = SplashKit.Rnd(1000);
        if (randomNumber < 25)
        {
            _Robots.Add(RandomRobot());
        }

        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            _Bullets.Add(AddBullet());

            SplashKit.PlaySoundEffect("1");
        }
        foreach (Bullet bullet in _Bullets)
        {
            bullet.Update();
        }
    }
    //return a new robot object
    public Robot RandomRobot()
    {
        Robot _RandomRobotOne = new Boxy(_GameWindow, _Player);
        Robot _RandomRobotTwo = new Roundy(_GameWindow, _Player);
        Robot _RandomRobotThree = new Custom(_GameWindow, _Player);

        double randomNumber = SplashKit.Rnd(900);
        if (randomNumber < 300)
        {
            return _RandomRobotOne;
        }
        else if (randomNumber > 300 & randomNumber < 600)
        {
            return _RandomRobotTwo;
        }
        else
        {
            return _RandomRobotThree;
        }
    }

    public Bullet AddBullet()
    {
        Bullet _RandomBullet = new Bullet(_GameWindow, _Player);
        return _RandomBullet;
    }

    //checks the collision between robot and player
    private void CheckCollisions()
    {

        foreach (Robot robot in _Robots)
        {
            //Check the player and robot collision
            if (_Player.CollidedWith(robot) & _Player.Lives > 0)
            {
                _Player.Lives = _Player.Lives - 1;
            }
            //check the player and robot collision to remove the robot from main list
            if (_Player.CollidedWith(robot) || robot.IsOffscreen(_GameWindow))
            {
                _removedRobots.Add(robot);
            }
            //check the bullet and robot collision
            foreach (Bullet bullet in _Bullets)
            {
                if (bullet.BulletCollidedWith(robot))
                {
                    _removedBullets.Add(bullet);
                    _removedRobots.Add(robot);
                }
                if (bullet.IsOffscreen(_GameWindow))
                {
                    _removedBullets.Add(bullet);
                }
            }
        }
        foreach (Robot robot in _removedRobots)
        {
            _Robots.Remove(robot);
        }
        foreach (Bullet bullet in _removedBullets)
        {
            _Bullets.Remove(bullet);
        }
    }

    //Draws lives left and score onto the screen
    public void DisplayHUD()
    {
        DrawHearts(_Player.Lives);
        SplashKit.DrawText("SCORE: " + _Player.Score, Color.White, 0, 40);
    }

    public void DrawHearts(int numberOfHearts)
    {
        int heartX = 0;
        for (int i = 0; i < numberOfHearts; i ++ )
        {
            if (heartX < 300)
            {
                SplashKit.DrawBitmap(HeartBitmap, heartX, 0);
                heartX = heartX + 40;
            }
        }
    }

}
