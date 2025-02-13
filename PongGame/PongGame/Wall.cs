using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Wall
{
    private Color _color;
    private Vector2 _position;
    private Rectangle _rectangle;
    private bool _IsHorizontal;

    public Wall(bool isHorizontal, Color color, int posX = 0, int posY = 0)
    {
        _color = color;
        _IsHorizontal = isHorizontal;
        _rectangle = new Rectangle(posX, posY, (!_IsHorizontal ? 15 : (int)(0.75 * Globals.ScreenHeight)), (!_IsHorizontal ? (int)(0.75 * Globals.ScreenHeight) : 15));
    }

    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, _rectangle, null, _color);
    }
}