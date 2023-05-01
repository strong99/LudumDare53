using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;

namespace LDJam53.Scenes;

[ComponentConfig<SimpleSideScrollingFollowComponentConfig>]
public class SimpleSideScrollingFollowComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly SimpleSideScrollingFollowComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public SimpleSideScrollingFollowComponent(SceneNode node, SimpleSideScrollingFollowComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public void Update(Single deltaTime, Single totalTime) {
        var following = _node.Get(_config.Follow);
        if (following is not null) {
            _node.Position = new(-following.Position.X * _config.Scale + _config.Offset.X, _node.Position.Y + _config.Offset.Y);
        }
    }

    public void Remove() {
        
    }
}
