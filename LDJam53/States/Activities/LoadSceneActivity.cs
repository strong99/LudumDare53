using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;

namespace LDJam53.States.Activities;

[StateActivityConfig<LoadSceneActivityConfig>]
public class LoadSceneStateActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly LoadSceneActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public LoadSceneStateActivity(SceneNode node, GameController gameController, LoadSceneActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status != StateActivityStatus.Finished) _gameController.SetScene(_config.Scene);
        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
