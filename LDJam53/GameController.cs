using LDJam53.Configs;
using LDJam53.Graphics;
using LDJam53.Scenes;
using LDJam53.States;
using System;

namespace LDJam53;

public class GameController {
    private readonly LD53Game _game;

    public ComponentFactory ComponentFactory { get; }
    public VideoDriver VideoDriver { get; }
    public InputManager InputManager { get; }
    public StateRepository StateRepository { get; set; }
    public AudioDriver AudioDriver { get; set; }

    private AppConfig _appConfig;

    public GameController(LD53Game game, AppConfig appConfig, ComponentFactory componentFactory, VideoDriver videoDriver, InputManager inputManager) {
        _game = game;
        _appConfig = appConfig;
        ComponentFactory = componentFactory;
        VideoDriver = videoDriver;
        InputManager = inputManager;
        AudioDriver = new MonoAudioDriver(game.Content);
    }

    public void Exit() {
        _game.Exit();
    }

    public Scene SetScene(String sceneId) {
        return _game.SetScene(sceneId);
    }

    public Scene GetScene() {
        return _game.Scene;
    }
}
