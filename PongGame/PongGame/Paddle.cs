using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Paddle
{
    private int offset = 25;
    private float _moveSpeed;
    private Vector2 _position;
    private Rectangle _rectangle;
    private bool _IsHorizontal;
    private Wall _wall;
    private Player _player;

    public Paddle(int wallX, int wallY, bool isHorizontal, int offset, Player player)
    {
        _IsHorizontal = isHorizontal;
        _wall = new Wall(_IsHorizontal, wallX, wallY);
        _moveSpeed = 500f;  
        _position = !_IsHorizontal ? new Vector2(wallX + offset, Globals.ScreenHeight / 2 - Math.Abs(offset)) : new Vector2(Globals.ScreenWidth / 2 - Math.Abs(offset), wallY + offset);
        _rectangle = new Rectangle((int)_position.X, (int)_position.Y, !_IsHorizontal ? 25 : 65, !_IsHorizontal ? 65 : 25);
        _player = player;
    }

    public void Update(GameTime gameTime, Paddle[] allPlayers)
    {
        _wall.Update(gameTime);
        // vertical paddle
        if (!_IsHorizontal)
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && _rectangle.Y < Globals.ScreenHeight - _rectangle.Height)
            {
                _rectangle.Y += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && _rectangle.Y > 0)
            {
                _rectangle.Y -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
        else
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && _rectangle.Y < Globals.ScreenHeight - _rectangle.Height)
            {
                _rectangle.X += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && _rectangle.Y > 0)
            {
                _rectangle.X -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _wall.Draw(spriteBatch);
        spriteBatch.Draw(Globals.Pixel, _rectangle, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);
    }
}