using LDJam53.Configs;
using LDJam53.Configs.Loaders;
using LDJam53.Graphics;
using LDJam53.Scenes;
using LDJam53.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53;

public class LD53Game : Game {
    private readonly GraphicsDeviceManager _graphics;

    private ComponentFactory _componentFactory;
    private GameController _gameController = default!;
    private SpriteBatch _spriteBatch = default!;
    private AppConfig _appConfig = default!;
    private Scene _scene = default!;
    private StateRepository _states = default!;

    public Scene Scene { get => _scene; }

    public LD53Game() {
        _graphics = new(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        _appConfig = new YamlConfigLoader().LoadFromFile("Configs/App.yaml");

        _graphics.PreferredBackBufferWidth = (Int32)_appConfig.Resolution.X;
        _graphics.PreferredBackBufferHeight = (Int32)_appConfig.Resolution.Y;
        if (_graphics.IsFullScreen != _appConfig.FullScreen) _graphics.ToggleFullScreen();
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var componentFactory = new DefaultComponentFactory();
        _componentFactory = componentFactory;
        _gameController = new(this, _appConfig, _componentFactory, new MonoGameVideoDriver(_spriteBatch, Content, _graphics), new InputManager(_appConfig));
        componentFactory.GameController = _gameController; // Design flaw

        SetScene(_appConfig.StartScene);
    }

    public Scene SetScene(String sceneId) {
        var config = new YamlSceneLoader().LoadFromFile($"Configs/{sceneId}Scene.yaml");
        _scene = new Scene(
            _componentFactory,
            config
        );
        _states = new StateRepository(
            _gameController,
            new YamlStatesLoader().LoadFromFile(String.IsNullOrWhiteSpace(config.States) ? $"Configs/{sceneId}States.yaml" : config.States)
        );
        _gameController.StateRepository = _states;
        return _scene;
    }

    protected override void Update(GameTime gameTime) {
        _gameController.InputManager.Update((Single)gameTime.ElapsedGameTime.TotalMilliseconds, (Single)gameTime.TotalGameTime.TotalMilliseconds);

        _scene?.Update((Single)gameTime.ElapsedGameTime.TotalMilliseconds, (Single)gameTime.TotalGameTime.TotalMilliseconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        var queue = new Dictionary<DrawableComponent, Single>();
        _scene?.Draw(queue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, DepthStencilState.Default);

        var sortedQueue = queue.OrderBy(p=>p.Value).Select(p=>p.Key).ToList();
        foreach (var q in sortedQueue) {
            q.Draw((Single)gameTime.ElapsedGameTime.TotalMilliseconds, (Single)gameTime.TotalGameTime.TotalMilliseconds);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
