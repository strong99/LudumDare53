using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;

namespace LDJam53.Scenes;

[ComponentConfig<QuestComponentConfig>]
public class QuestComponent : Component {
    private readonly GameController _gameController;
    private readonly QuestComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Int32 Score { get => _config.Score; }
    public String Destination { get => _config.Destination; }
    public Single Penalty { get => _config.Penalty; }
    public String Route { get => _config.Route; }

    public QuestComponent(SceneNode node, QuestComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public void Remove() {

    }
}
