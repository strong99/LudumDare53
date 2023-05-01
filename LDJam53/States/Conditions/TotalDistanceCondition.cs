using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<TotalDistanceConditionConfig>]
public class TotalDistanceCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly TotalDistanceConditionConfig _config;

    private SceneNode? _tracked;

    public TotalDistanceCondition(SceneNode node, GameController gameController, TotalDistanceConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;

        _tracked = _node.Get(_config.Track);
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        _tracked ??= _node.Get(_config.Track);
        return _tracked.Position.X >= _config.Distance;
    }

    public void Remove() { }
}
