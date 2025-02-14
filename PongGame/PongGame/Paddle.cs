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
    public Rectangle boundingBox;
    private bool _IsHorizontal;
    private Wall _wall;
    private Player _player;

    public Paddle(int wallX, int wallY, bool isHorizontal, int offset, Player player)
    {
        _IsHorizontal = isHorizontal;
        _wall = new Wall(_IsHorizontal, wallX, wallY);
        _moveSpeed = 500f;  
        _position = !_IsHorizontal ? new Vector2(wallX + offset, Globals.ScreenHeight / 2 - Math.Abs(offset)) : new Vector2(Globals.ScreenWidth / 2 - Math.Abs(offset), wallY + offset);
        boundingBox = new Rectangle((int)_position.X, (int)_position.Y, !_IsHorizontal ? 25 : 65, !_IsHorizontal ? 65 : 25);
        _player = player;
    }

    // Each paddle needs to check intersection with previous and next player
    // e.g. p1 checks p2 and p4, p2 checks p1 and p3, etc.
    // Check wall too
    public bool IntersectsWithWallOrPaddle(Paddle[] allPlayers)
    {
        int currentPlayerIndex = _player.PlayerNumber - 1;
        return false;
    }

    public void Update(GameTime gameTime, Paddle[] allPlayers)
    {
        _wall.Update(gameTime);
        // vertical paddle
        if (!_IsHorizontal)
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height)
            {
                boundingBox.Y += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && boundingBox.Y > 0)
            {
                boundingBox.Y -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
        else
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height)
            {
                boundingBox.X += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && boundingBox.Y > 0)
            {
                boundingBox.X -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _wall.Draw(spriteBatch);
        spriteBatch.Draw(Globals.Pixel, boundingBox, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);
    }
}