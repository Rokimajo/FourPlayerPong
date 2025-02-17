using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Paddle _p1;
    private Paddle _p2;
    private Paddle _p3;
    private Paddle _p4;
    private Paddle[] _allPlayers;
    private Ball _ball;

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

        _ball = new Ball();
        // Base offset to center walls (walls done manually for now (could not get auto centered walls to work while centering the origin for some reason))
        int baseOffset = 447; 
        _p1 = new Paddle(baseOffset, 113, false, 35, new Player(1, Keys.Q, Keys.A));
        _p2 = new Paddle( baseOffset + 15, 113,true, 35, new Player(2, Keys.Z, Keys.C));
        _p3 = new Paddle(baseOffset + 690, 113, false, -45, new Player(3, Keys.E, Keys.D));
        _p4 = new Paddle(baseOffset + 15, 773, true, -45, new Player(4, Keys.Left, Keys.Right));
        _allPlayers = new[] {_p1, _p2, _p3, _p4};
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("PongFont");
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
        _ball.Update(gameTime, _allPlayers);
        _p1.Update(gameTime, _allPlayers, _ball);
        _p2.Update(gameTime, _allPlayers, _ball);
        _p3.Update(gameTime, _allPlayers, _ball);
        _p4.Update(gameTime, _allPlayers, _ball);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _ball.Draw(_spriteBatch);
        _p1.Draw(_spriteBatch, _font);
        _p2.Draw(_spriteBatch, _font);
        _p3.Draw(_spriteBatch, _font);
        _p4.Draw(_spriteBatch, _font);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}