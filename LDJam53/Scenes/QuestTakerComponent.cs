using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Numerics;

namespace LDJam53.Scenes;

[ComponentConfig<QuestTakerComponentConfig>]
public class QuestTakerComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly QuestTakerComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public QuestTakerComponent(SceneNode node, QuestTakerComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        //_hint = new SceneNode(gameController.ComponentFactory, _config);
    }
    
    private SceneNode? _previousNearest = null;
    //private SceneNode? _hint = null;

    public SceneNode? FocusedActor { get => _previousNearest; }

    public void Update(Single deltaTime, Single totalTime) {
        var start = _node.WorldPosition;
        var interactables = _node.Parent.FindWithComponent<ActorComponent>();
        var nearest = default(SceneNode);
        var nearestDistance = 100 * 100;
        foreach(var c in interactables) {
            var distance = Vector2.DistanceSquared(c.WorldPosition, _node.WorldPosition);
            if (distance < nearestDistance) {
                distance = nearestDistance;
                nearest = c;
            }
        }

        if (_previousNearest != nearest) {
            //_previousNearest?.RemoveChild(_hint);
            _previousNearest = nearest;
            //_previousNearest?.AddChild(_hint);
        }
    }

    public void Remove() {

    }
}
