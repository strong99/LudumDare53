using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;

namespace LDJam53.Scenes;

[ComponentConfig<MusicComponentConfig>]
public class MusicComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly MusicComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    private Music _music;

    public MusicComponent(SceneNode node, MusicComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        _music = _gameController.AudioDriver.GetMusic(_config.Song);
    }

    public void Draw(Single deltaTime, Single totalTime) {
        _gameController.AudioDriver.Play(_music, 1, true);
    }

    public void Remove() { }
}