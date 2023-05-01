using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;

namespace LDJam53.States.Activities;

[StateActivityConfig<EnableNodeActivityConfig>]
public class EnableNodeActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly EnableNodeActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public EnableNodeActivity(SceneNode node, GameController gameController, EnableNodeActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished)
            return Status;

        _node.Get(_config.Target).Enabled = _config.Enabled;

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
