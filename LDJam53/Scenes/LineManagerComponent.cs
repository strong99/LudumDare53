using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53.Scenes;

[ComponentConfig<LineManagerComponentConfig>]
public class LineManagerComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly LineManagerComponentConfig _config;
    private readonly SceneNode _node;

    private Single _nextSpawnInterval;
    private Single _nextDifficultyIncrease;
    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }
    public Int32 Count { get => _config.Count; }
    public Single CurrentDifficulty { get => _config.Difficulty; set => _config.Difficulty = value; }

    public LineManagerComponent(SceneNode node, LineManagerComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        _nextSpawnInterval = _config.SpawnIntervalMs;
        _nextDifficultyIncrease = _config.DifficultyIntervalMs;
    }

    public void Update(Single deltaTime, Single totalTime) {
        _nextDifficultyIncrease -= deltaTime;
        if (_nextDifficultyIncrease < 0) {
            _nextDifficultyIncrease += _config.DifficultyIntervalMs;
            CurrentDifficulty++;
        }

        _nextSpawnInterval -= deltaTime;
        if (_nextSpawnInterval < 0) {
            _nextSpawnInterval += _config.SpawnIntervalMs;

            var available = new List<ObstacleTemplateComponentConfig>();
            var maxWeight = 0.0f;
            foreach (var d in _config.Obstacles) {
                if (d.Difficulty <= CurrentDifficulty) {
                    available.Add(d);
                    maxWeight += d.Weight;
                }
            }
            if (available.Any()) {
                var expectedWeight = maxWeight * _random.NextDouble();
                var currentWeight = 0.0f;
                foreach(var a in available) {
                    var startWeight = currentWeight; 
                    var endWeight = currentWeight + a.Weight;
                    currentWeight = endWeight;
                    if (startWeight < expectedWeight 
                     && endWeight >= expectedWeight) {
                        Create(a);
                        break;
                    }
                }
            }
        }
    }

    private Random _random = new();
    private void Create(ObstacleTemplateComponentConfig config) {
        var sceneNode = new SceneNode(_gameController.ComponentFactory, config.DeepClone());
        var lane = _random.Next(_config.Count);
        var offsetX = _gameController.VideoDriver.ViewportSize.X / 2.0f;
        sceneNode.Order = config.Order - lane / 10f;
        sceneNode.Position = new(-_node.Parent.Position.X + offsetX + config.Width * config.Origin.X, -lane * 32);
        sceneNode.AddComponent(new ObstacleComponent(sceneNode, new() { LanesHeight = config.LanesHeight, Lane = lane, Width = config.Width, Origin = config.Origin }, _gameController));
        _node.AddChild(sceneNode);
    }

    public void Remove() {

    }

    public Boolean HasCollision(Single minX, Single maxX, Int32 lane) {
        foreach(var c in _node.Children) {
            var o = c.GetComponent<ObstacleComponent>();
            if (o is null) continue;

            if (o.IsInside(minX, maxX, lane)) {
                return true;
            }
        }
        return false;
    }
}
