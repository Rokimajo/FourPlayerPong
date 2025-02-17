using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Ball
{
    private float _baseSpeed = 2.5f;
    private float _speedIncrease = 0.35f;
    public float _speed;
    private float _angle;
    private Vector2 _position;
    private Vector2 _velocity;
    public Rectangle _rectangle;
    private Random random = new Random();

    public Ball()
    {
        _speed = _baseSpeed;
        _position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        _rectangle = new Rectangle((int) _position.X, (int) _position.Y, 10, 10);
        
        // start with random angle
        _angle = (float) (random.Next(0, 360) * Math.PI / 180);
        
        _velocity = new Vector2(
            (float) Math.Cos(_angle) * _speed,
            (float) Math.Sin(_angle) * _speed
        );
    }

    private void ResetBall()
    {
        _speed = _baseSpeed;
        _position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        _rectangle.X = (int) _position.X;
        _rectangle.Y = (int) _position.Y;
        
        _angle = (float) (random.Next(0, 360) * Math.PI / 180);
        _velocity = new Vector2(
            (float) Math.Cos(_angle) * _speed,
            (float) Math.Sin(_angle) * _speed
        );
    }

    public void Update(GameTime gameTime, Paddle[] allPlayers)
    {
        _position += _velocity;
        _rectangle.X = (int) _position.X;
        _rectangle.Y = (int) _position.Y;

        foreach (Paddle paddle in allPlayers)
        {
            if (_rectangle.Intersects(paddle.paddleWall.boundingBox))
            {
                if (!paddle._player.IsEliminated())
                    paddle._player.DecreaseLife();
                ResetBall();
            }
            
            if (_rectangle.Intersects(paddle.boundingBox))
            {
                Vector2 normal;
                float hitPosition;

                _speed += _speedIncrease; // Speed ball up on bounce
                
                if (paddle._player.PlayerNumber == 1) // Left Paddle
                {
                    normal = -Vector2.UnitX;
                    _position.X = paddle.boundingBox.Right;
                    hitPosition = (_position.Y - paddle.boundingBox.Center.Y) / (paddle.boundingBox.Height / 2);
                }
                else if (paddle._player.PlayerNumber == 2) // Top Paddle
                {
                    normal = -Vector2.UnitY;
                    _position.Y = paddle.boundingBox.Bottom;
                    hitPosition = (_position.X - paddle.boundingBox.Center.X) / (paddle.boundingBox.Width / 2);
                }
                else if (paddle._player.PlayerNumber == 3) // Right Paddle
                {
                    normal = Vector2.UnitX;
                    _position.X = paddle.boundingBox.Left - _rectangle.Width;
                    hitPosition = (_position.Y - paddle.boundingBox.Center.Y) / (paddle.boundingBox.Height / 2);
                }
                else // Bottom Paddle
                {
                    normal = Vector2.UnitY;
                    _position.Y = paddle.boundingBox.Top - _rectangle.Height;
                    hitPosition = (_position.X - paddle.boundingBox.Center.X) / (paddle.boundingBox.Width / 2);
                }

                // Calculate ball reflection
                float deflectionAngle = hitPosition * MathHelper.ToRadians(45);
                Matrix rotationMatrix = Matrix.CreateRotationZ(deflectionAngle);
                normal = Vector2.Transform(normal, rotationMatrix);
                normal.Normalize();

                _velocity = Vector2.Reflect(_velocity, normal);
                _velocity = Vector2.Normalize(_velocity) * _speed;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, _rectangle, Color.White);
    }
}