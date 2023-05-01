using LDJam53.Configs.States;
using LDJam53.Scenes;
using LDJam53.States.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53.States;

public class StateExit {
    private readonly StateExitConfig _config;
    private readonly GameController _gameController;
    private readonly SceneNode _node;

    private readonly List<StateCondition> _conditions;

    public StateExit(SceneNode node, GameController gameController, StateExitConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;

        _conditions = config.Conditions.Select(c => _gameController.StateRepository.CreateCondition(node, _gameController, c)).ToList();
    }

    public String State { get => _config.Destination; }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        return _conditions.All(p => p.IsMet(deltaTime, totalTime));
    }

    public void Remove() {
        foreach (var c in _conditions) c.Remove();
    }
}
