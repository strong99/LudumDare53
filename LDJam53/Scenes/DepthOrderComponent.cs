using LDJam53.Configs.Components;
using LDJam53.Graphics;

namespace LDJam53.Scenes;

[ComponentConfig<DepthOrderComponentConfig>]
public class DepthOrderComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly DepthOrderComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public DepthOrderComponent(SceneNode node, DepthOrderComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public void Update(System.Single deltaTime, System.Single totalTime) {
        _node.Order = 10 + _node.Position.Y / 1000f;
    }

    public void Remove() {

    }
}
