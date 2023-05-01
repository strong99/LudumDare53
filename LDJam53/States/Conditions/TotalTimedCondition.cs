using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<TotalTimedConditionConfig>]
public class TotalTimedCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly TotalTimedConditionConfig _config;
    private Single? _startTime;

    public TotalTimedCondition(SceneNode node, GameController gameController, TotalTimedConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        if (!_startTime.HasValue) _startTime = totalTime;
        var activeTime = totalTime - _startTime;
        return activeTime > _config.TotalTimed;
    }

    public void Remove() { }
}
