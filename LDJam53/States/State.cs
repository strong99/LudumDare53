using LDJam53.Configs.States;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53.States;

public class State {
    private readonly StateConfig _config;
    private readonly GameController _gameController;
    private readonly SceneNode _node;

    private Int32 _activityIndex = -1;
    private StateActivity? _activity;

    private List<StateExit> _exits;

    public State(SceneNode node, GameController gameController, StateConfig config) {
        _node = node;
        _config = config;
        _gameController = gameController;

        _exits = _config.Exits is null ? new() : new(_config.Exits.Select(e => new StateExit(node, gameController, e)));
    }

    public void Update(Single deltaTime, Single totalTime) {
        var exit = _exits.FirstOrDefault(e => e.IsMet(deltaTime, totalTime));
        if (exit != null) {
            _node.GetComponent<StateComponent>().SetState(exit.State);
            return;
        }

        if (_config.Activities?.Count > 0 && ( _activity == null || _activity.Status == StateActivityStatus.Finished)) {
            _activityIndex++;
            var before = _activityIndex;
            _activityIndex %= _config.Activities.Count;
            if (before != _activityIndex) _node.Inform(new NamedEventArgs { Id = "StateFinished" });
            _activity?.Remove();
            _activity = _gameController.StateRepository.CreateActivity(_node, _gameController, _config.Activities[_activityIndex]);
        }
        _activity?.Update(deltaTime, totalTime);
    }

    public void Remove() {
        foreach (var c in _exits) c.Remove();
        _activity?.Remove();
    }
}
