using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Ball
{
    private float _speed = 3f;
    private float _angle;
    private Vector2 _position;
    private Rectangle _rectangle;
    public Ball()
    {
        _position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        _rectangle = new Rectangle((int)_position.X, (int)_position.Y, 10, 10);
        Random random = new Random();
        _angle = random.Next(0, 360);
    }
    public void Update(GameTime gameTime)
    {
        _rectangle.X += (int)(_speed * Math.Cos(_angle));
        _rectangle.Y += (int)(_speed * Math.Sin(_angle));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, _rectangle, null, Color.White);
    }
}