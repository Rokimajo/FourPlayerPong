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
                break;
            }
            
            if (_rectangle.Intersects(paddle.boundingBox))
            {
                _speed += _speedIncrease;

                float relativeIntersect;
                float normalizedIntersect;

                if (paddle._player.PlayerNumber == 1 || paddle._player.PlayerNumber == 3)
                {
                    relativeIntersect = (_position.Y + _rectangle.Height / 2) - (paddle.boundingBox.Y + paddle.boundingBox.Height / 2);
                    normalizedIntersect = relativeIntersect / (paddle.boundingBox.Height / 2);
                    
                    _velocity.X = -_velocity.X;
                    _velocity.Y = normalizedIntersect * _speed;
                }
                else
                { 
                    relativeIntersect = (_position.X + _rectangle.Width / 2) - (paddle.boundingBox.X + paddle.boundingBox.Width / 2);
                    normalizedIntersect = relativeIntersect / (paddle.boundingBox.Width / 2);
        
                    _velocity.Y = -_velocity.Y;
                    _velocity.X = normalizedIntersect * _speed;
                }
                
                _velocity = Vector2.Normalize(_velocity) * _speed;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, _rectangle, Color.White);
    }
}