using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Paddle
{
    private float _baseSpeed = 500f;
    private float _moveSpeed;
    private Vector2 _position;
    public Rectangle boundingBox;
    public bool _IsHorizontal;
    public Wall paddleWall;
    public Player _player;
    public bool AI;

    public Paddle(int wallX, int wallY, bool isHorizontal, int offset, Player player)
    {
        AI = true;
        _IsHorizontal = isHorizontal;
        paddleWall = new Wall(_IsHorizontal, wallX, wallY);
        _moveSpeed = _baseSpeed;  
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
        // Check Paddles
        if (boundingBox.Intersects(allPlayers[currentPlayerIndex - 1 < 0 ? allPlayers.Length - 1 : currentPlayerIndex - 1].boundingBox) ||
            boundingBox.Intersects(allPlayers[currentPlayerIndex - 1 < 0 ? allPlayers.Length - 1 : currentPlayerIndex - 1].paddleWall.boundingBox))
        {
            return true;
        } 
        if (boundingBox.Intersects(allPlayers[currentPlayerIndex + 1 > allPlayers.Length - 1 ? 0 : currentPlayerIndex + 1].boundingBox) ||
            boundingBox.Intersects(allPlayers[currentPlayerIndex + 1 > allPlayers.Length - 1 ? 0 : currentPlayerIndex + 1].paddleWall.boundingBox))
        {
            return true;
        }

        return false;
    }

    public void MoveAIPaddle(GameTime gameTime, Paddle[] allPlayers, Ball ball)
    {
        if (ball._speed < _moveSpeed)
        {
            _moveSpeed = Math.Min(ball._speed * 100, _baseSpeed);
        }
        
         if (!_IsHorizontal)
        {
            var hD2 = boundingBox.Height / 2;
            if (ball._rectangle.Center.Y > boundingBox.Center.Y && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height &&
                ball._rectangle.Center.Y > boundingBox.Center.Y + hD2)
            {
                var oldPos = boundingBox;
                boundingBox.Y += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox = oldPos;
                }
            }
            else if (ball._rectangle.Center.Y < boundingBox.Center.Y && boundingBox.Y > 0 &&
                     ball._rectangle.Center.Y < boundingBox.Center.Y - hD2)
            {
                var oldPos = boundingBox;
                boundingBox.Y -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox = oldPos;
                }
            }
        }
        // horizontal paddle
        else
        {
            var wD2 = boundingBox.Width / 2;

            if (ball._rectangle.Center.X > boundingBox.Center.X && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height &&
                ball._rectangle.Center.X > boundingBox.Center.X + wD2)
            {
                var oldPos = boundingBox.X;
                boundingBox.X += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox.X = oldPos;
                }
            }
            else if (ball._rectangle.Center.X < boundingBox.Center.X && boundingBox.Y > 0 &&
                     ball._rectangle.Center.X < boundingBox.Center.X - wD2)
            {
                var oldPos = boundingBox.X;
                boundingBox.X -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox.X = oldPos;
                }
            }
        }
    }

    public void DisableAI()
    {
        AI = !AI;
        _moveSpeed = _baseSpeed;
    }

    public void Update(GameTime gameTime, Paddle[] allPlayers, Ball ball)
    {
        if (_player.IsEliminated())
        {
            return;
        }

        if (AI)
        {
            MoveAIPaddle(gameTime, allPlayers, ball);
        }
        // Paddle Movement (cannot move further if it intersects with another paddle or wall)
        // vertical paddle
        if (!_IsHorizontal)
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height)
            {
                // If AI is enabled and key is pressed, disable it
                if (AI)
                    DisableAI();
                var oldPos = boundingBox;
                boundingBox.Y += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox = oldPos;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && boundingBox.Y > 0)
            {
                if (AI)
                    DisableAI();
                var oldPos = boundingBox;
                boundingBox.Y -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox = oldPos;
                }
            }
        }
        // horizontal paddle
        else
        {
            if (Keyboard.GetState().IsKeyDown(_player.KeyTwo) && boundingBox.Y < Globals.ScreenHeight - boundingBox.Height)
            {
                if (AI)
                    DisableAI();
                var oldPos = boundingBox.X;
                boundingBox.X += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox.X = oldPos;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(_player.KeyOne) && boundingBox.Y > 0)
            {
                if (AI)
                    DisableAI();
                var oldPos = boundingBox.X;
                boundingBox.X -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (IntersectsWithWallOrPaddle(allPlayers))
                {
                    boundingBox.X = oldPos;
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        _player.Draw(spriteBatch, font, paddleWall);
        paddleWall.Draw(spriteBatch);
        spriteBatch.Draw(Globals.Pixel, boundingBox, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);
    }
}