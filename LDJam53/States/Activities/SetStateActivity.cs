using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;
using System.Collections.Generic;

namespace LDJam53.States.Activities;

[StateActivityConfig<SetStateActivityConfig>]
public class SetStateActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly SetStateActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    private List<StateActivity> _activities = new();

    public SetStateActivity(SceneNode node, GameController gameController, SetStateActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(Single deltaTime, Single totalTime) {
        if (Status == StateActivityStatus.Finished) {
            return Status;
        }

        var target = _node.Get(_config.Target);
        var stateManager = target?.GetComponent<StateComponent>();
        stateManager?.SetState(_config.State);

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
