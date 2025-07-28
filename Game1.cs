using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Under_the_shade_of_sakura.Controllers;
using MonoGame_Under_the_shade_of_sakura.Managers;
using MonoGame_Under_the_shade_of_sakura.Models;
using MonoGame_Under_the_shade_of_sakura.Screens;
using MonoGame_Under_the_shade_of_sakura.Views;
using System;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static Game1 Instance { get; private set; }
    public ScreenManager ScreenManager { get; private set; }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Instance = this;
        ScreenManager = ScreenManager.Instance;
    }

    protected override void Initialize()
    {


        ScreenManager.ChangeScreen(new MainMenuController());

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ScreenManager.LoadContent(Content);

        if (Instance == null) Instance = this;
    }

    protected override void Update(GameTime gt)
    {
        ScreenManager.Update(gt);
        base.Update(gt);
    }

    protected override void Draw(GameTime gt)
    {
        GraphicsDevice.Clear(Color.Black);

        var current = ScreenManager.CurrentScreen;

        if (current is GameController gameCtrl)
        {
            _spriteBatch.Begin();
            gameCtrl.DrawUICanvas(_spriteBatch);
            _spriteBatch.End();
            _spriteBatch.Begin(transformMatrix: gameCtrl.CameraTransform);
            gameCtrl.Draw(_spriteBatch);

            _spriteBatch.End();
        }
        else
        {
            _spriteBatch.Begin();
            ScreenManager.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        base.Draw(gt);
    }
}