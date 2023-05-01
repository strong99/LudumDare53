using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<LineCollisionConditionConfig>]
public class LineCollisionCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly LineCollisionConditionConfig _config;
    private readonly SceneNode _target;
    private readonly SceneNode _manager;

    public LineCollisionCondition(SceneNode node, GameController gameController, LineCollisionConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;

        _target = _config.Target != null ? node.Get(_config.Target) ?? throw new ArgumentNullException(nameof(_config.Target)) : _node;
        _manager = _node.Get(_config.LineManager) ?? throw new ArgumentNullException(nameof(_config.LineManager));
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        var self = _target.GetComponent<ObstacleComponent>();
        return _manager.GetComponent<LineManagerComponent>().HasCollision(self.MinX, self.MaxX, self.Lane);
    }

    public void Remove() { }
}
