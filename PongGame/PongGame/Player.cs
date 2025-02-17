using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Player
{
    private int startingLife = 10;
    public int PlayerNumber;
    public Keys KeyOne;
    public Keys KeyTwo;
    public int Life;
    
    public Player(int num, Keys one, Keys two)
    {
        PlayerNumber = num;
        KeyOne = one;
        KeyTwo = two;
        Life = startingLife;
    }

    public void DecreaseLife() => Life = Math.Max(Life - 1, 0);

    public void ResetPlayer() => Life = startingLife;
    
    public bool IsEliminated() => Life == 0;
    
    public void Draw(SpriteBatch spriteBatch, SpriteFont font, Wall wall)
    {
        var scoreOffsets = new int[]
        {
            48,
            22,
            -53,
            -55
        };
        var posX = wall.IsHorizontal ? wall.boundingBox.Center.X : wall.boundingBox.Center.X - scoreOffsets[PlayerNumber - 1];
        var posY = wall.IsHorizontal ? wall.boundingBox.Center.Y - scoreOffsets[PlayerNumber - 1] : wall.boundingBox.Center.Y;
        spriteBatch.DrawString(font, Life.ToString(), new Vector2(posX, posY), Color.White * 0.2f, 0, font.MeasureString(Life.ToString()) / 2, 1.0f, SpriteEffects.None, 1f);
    }
    
}