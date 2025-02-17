using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Wall
{
    private Color _color;
    private Vector2 _position;
    public Rectangle boundingBox;
    public bool IsHorizontal;

    public Wall(bool isHorizontal, int posX = 0, int posY = 0)
    {
        IsHorizontal = isHorizontal;
        boundingBox = new Rectangle(posX, posY, (!IsHorizontal ? 15 : (int)(0.75 * Globals.ScreenHeight)), (!IsHorizontal ? (int)(0.75 * Globals.ScreenHeight) : 15));
    }
    

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Globals.Pixel, boundingBox, null, Color.White);
    }
}