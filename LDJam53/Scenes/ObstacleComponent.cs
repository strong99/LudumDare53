using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;

namespace LDJam53.Scenes;

[ComponentConfig<ObstacleComponentConfig>]
public class ObstacleComponent : Component {
    private readonly GameController _gameController;
    private readonly ObstacleComponentConfig _config;
    private readonly SceneNode _node;

    public Int32 LanesHeight { get => _config.LanesHeight; }
    public Int32 Lane { get => _config.Lane; set => _config.Lane = value; }
    public Single Width { get => _config.Width; }

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }
    public Single MinX { get => _node.Position.X - _config.Origin.X * _config.Width; }
    public Single MaxX { get => _node.Position.X + (1 - _config.Origin.X) * _config.Width; }

    public ObstacleComponent(SceneNode node, ObstacleComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public Boolean IsInside(Single minX, Single maxX, Int32 lane) {
        return maxX > _node.Position.X - _config.Origin.X * _config.Width
            && minX < _node.Position.X + (1 - _config.Origin.X) * _config.Width
            && lane >= _config.Lane 
            && lane < _config.Lane + _config.LanesHeight;
    }

    public void Remove() {
        
    }
}
