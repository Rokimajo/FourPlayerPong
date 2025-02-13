using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Ball
{
    private float _speed = 250f;
    private Vector2 _position;
    private Vector2 _angle;
    private Rectangle _rectangle;
    public Ball()
    {
        _position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        _rectangle = new Rectangle((int)_position.X, (int)_position.Y, 10, 10);
        Random random = new Random();
    }
    public void Update(GameTime gameTime)
    {
        _rectangle.X -= (int)(_speed * (float) gameTime.ElapsedGameTime.TotalSeconds);
        _rectangle.Y += (int)(_speed * (float) gameTime.ElapsedGameTime.TotalSeconds);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, _rectangle, null, Color.White);
    }
}