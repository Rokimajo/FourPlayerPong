using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Player
{
    public int PlayerNumber;
    public Keys KeyOne;
    public Keys KeyTwo;
    public int Life;
    
    public Player(int num, Keys one, Keys two)
    {
        PlayerNumber = num;
        KeyOne = one;
        KeyTwo = two;
        Life = 10;
    }
    
}