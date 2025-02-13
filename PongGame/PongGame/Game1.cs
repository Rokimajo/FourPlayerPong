using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Paddle _p1;
    private Paddle _p2;
    private Paddle _p3;
    private Paddle _p4;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
        _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
        _graphics.ApplyChanges();

        _p1 = new Paddle(500, 113, false, Color.Red);
        _p2 = new Paddle( 500, 100,true, Color.Blue);
        _p3 = new Paddle(1166, 100, false, Color.Green);
        _p4 = new Paddle(500, 773, true, Color.Yellow);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // Set white pixel texture to use in paddles and balls, etc.
        Globals.Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Globals.Pixel.SetData(new[] { Color.White });
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _p1.Update(gameTime);
        _p2.Update(gameTime);
        _p3.Update(gameTime);
        _p4.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _p1.Draw(_spriteBatch);
        _p2.Draw(_spriteBatch);
        _p3.Draw(_spriteBatch);
        _p4.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}