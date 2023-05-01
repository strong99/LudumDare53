using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Linq;

namespace LDJam53.Scenes;

[ComponentConfig<SimpleSideScrollingTileComponentConfig>]
public class SimpleSideScrollingTileComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly SimpleSideScrollingTileComponentConfig _config;
    private readonly SceneNode _node;
    private Single _minX = 0;
    private Single _maxX = 0;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public SimpleSideScrollingTileComponent(SceneNode node, SimpleSideScrollingTileComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    private readonly Random _random = new Random();
    public void Update(Single deltaTime, Single totalTime) {
        if (_node.WorldPosition.X + _minX > 0) {
            var config = GetRandom();
            Create(config, -1);
        }
        if (_node.WorldPosition.X - 640 + _maxX < 1280) {
            var config = GetRandom();
            Create(config, 1);
        }

        if (_node.WorldPosition.X + _minX < -2560) {
            Remove(-1);
        }
        if (_node.WorldPosition.X - 640 + _maxX > 2560) {
            Remove(1);
        }
    }

    private SimpleSideScrollingTileConfig GetRandom() {
        var maxWeight = 0.0f;
        foreach (var t in _config.Tiles) {
            maxWeight += t.Weight;
        }
        var expectedWeight = _random.NextDouble() * maxWeight;
        var weight = 0.0f;
        foreach (var t in _config.Tiles) {
            var startWeight = weight; 
            var nextWeight = startWeight + t.Weight;
            if (startWeight < expectedWeight 
             && nextWeight >= expectedWeight) {
                return t;
            }
            weight = nextWeight;
        }
        throw new Exception();
    }

    private void Create(SimpleSideScrollingTileConfig config, Int32 direction) {
        config = config.DeepClone();
        Single position;
        if (direction == -1) {
            _minX -= config.Width;
            position = _minX + config.Width / 2.0f;
        }
        else {
            _maxX += config.Width;
            position = _maxX - config.Width / 2.0f;
        }

        var node = new SceneNode(_gameController.ComponentFactory, new() {
            Id = "<generated>",
            Children = new(),
            Position = new(position, 0),
            Components = config.Components
        });
        _node.AddChild(node);
    }

    private void Remove(Int32 direction) {
        if (!_node.Children.Any()) return;

        var furthest = direction == -1 ? _node.Children.OrderBy(p => p.Position.X).First() : _node.Children.OrderBy(p => p.Position.X).Last();
        if (furthest is not null) {
            _node.RemoveChild(furthest);
            if (direction == -1) _minX += 1280;
            else if (direction == 1) _maxX -= 1280;
        }
    }

    public void Remove() {

    }
}
