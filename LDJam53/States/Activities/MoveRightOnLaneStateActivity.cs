using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;

namespace LDJam53.States.Activities;

[StateActivityConfig<MoveRightOnLaneStateActivityConfig>]
public class MoveRightOnLaneStateActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly MoveRightOnLaneStateActivityConfig _config;
    private Single _nextSpeedIncrease;
    private Single _speedAdjustment = 5;

    public StateActivityStatus Status { get; private set; }

    public MoveRightOnLaneStateActivity(SceneNode node, GameController gameController, MoveRightOnLaneStateActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
        _nextSpeedIncrease = config.SpeedIncreaseInterval;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        var self = _node.GetComponent<ObstacleComponent>();
        var lineManager = _node.Get(_config.LineManager).GetComponent<LineManagerComponent>();
        if (_gameController.InputManager.PressedStart("laneDown") && self.Lane > 0) {
            self.Lane--;
        }
        else if (_gameController.InputManager.PressedStart("laneUp") && self.Lane < lineManager.Count - 1) {
            self.Lane++;
        }
        if (_gameController.InputManager.PressActive("speedUp") && _speedAdjustment < 10) {
            _speedAdjustment += deltaTime / 100f;
            if (_speedAdjustment > 0) _speedAdjustment = 10;
        }
        else if (_gameController.InputManager.PressActive("speedDown") && _speedAdjustment > 0) {
            _speedAdjustment -= deltaTime / 100f;
            if (_speedAdjustment < 0) _speedAdjustment = 0;
        }

        _nextSpeedIncrease -= deltaTime;
        if (_nextSpeedIncrease < 0) {
            _nextSpeedIncrease += _config.SpeedIncreaseInterval;
            _config.Speed *= _config.SpeedIncreaseMultiplier;
        }

        _node.Order = _config.Order - self.Lane /10f;
        _node.Position = new(_node.Position.X + deltaTime / 1000.0f * _config.Speed * (0.25f + _speedAdjustment / 6.65f), -self.Lane * 32);

        return Status = StateActivityStatus.Active;
    }

    public void Remove() { }
}
