using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;

namespace LDJam53.Scenes;

[ComponentConfig<PlayerComponentConfig>]
public class PlayerComponent : Component {
    private readonly GameController _gameController;
    private readonly PlayerComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Int32 Score { get => _config.Score; set => _config.Score = value; }
    public Int32 Deliveries { get => _config.Deliveries; set => _config.Deliveries = value; }

    public PlayerComponent(SceneNode node, PlayerComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public void Remove() {

    }
}
