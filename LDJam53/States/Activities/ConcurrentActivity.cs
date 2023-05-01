using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53.States.Activities;

[StateActivityConfig<ConcurrentActivityConfig>]
public class ConcurrentActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly ConcurrentActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    private List<StateActivity> _activities = new();

    public ConcurrentActivity(SceneNode node, GameController gameController, ConcurrentActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;

        _activities.AddRange(_config.Activities.Select(p => gameController.StateRepository.CreateActivity(_node, _gameController, p)));
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        var allFinished = true;
        var anyFinished = false;
        foreach(var a in _activities) {
            var r = a.Update(deltaTime, totalTime);
            if (r == StateActivityStatus.Finished) {
                anyFinished = true;
            }
            else {
                allFinished = false;
            }
        }

        if ((anyFinished && _config.FinishOnFirst) || allFinished) {
            Status = StateActivityStatus.Finished;
        }
        return Status;
    }

    public void Remove() { }
}
