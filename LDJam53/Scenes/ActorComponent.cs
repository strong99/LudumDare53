using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Linq;

namespace LDJam53.Scenes;

[ComponentConfig<ActorComponentConfig>]
public class ActorComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly ActorComponentConfig _config;
    private readonly SceneNode _node;
    private readonly SceneNode _hint;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }
    public String Name { get => _config.Name; }

    public ActorComponent(SceneNode node, ActorComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        _hint = new SceneNode(gameController.ComponentFactory, new() {
            Id = "<quest-hint>",
            Children = new(),
            Position = new(0, -384),
            Components = new() {
                new ImageComponentConfig {
                    ImageEffects = 0,
                    Origin = new(0.5f, 0.5f),
                    Texture = "Icons/Quest-01",
                    Tint = "white"
                }
            }
        });
    }

    public void Remove() {

    }

    public void Update(System.Single deltaTime, System.Single totalTime) {
        var quests = _node.Components.Where(p => p is QuestComponent).ToList();
        if (quests.Count > 0 && _hint.Parent == null) {
            _node.AddChild(_hint);
        }
        else if (quests.Count == 0 && _hint.Parent != null) {
            _node.RemoveChild(_hint);
        }
    }
}
