using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;

namespace LDJam53.States.Activities;

[StateActivityConfig<QuitActivityConfig>]
public class QuitActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly QuitActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public QuitActivity(SceneNode node, GameController gameController, QuitActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished)
            return Status;

        _gameController.Exit();

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
