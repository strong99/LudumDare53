using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;

namespace LDJam53.States.Activities;

[StateActivityConfig<MoveRightStateActivityConfig>]
public class MoveRightStateActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly MoveRightStateActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public MoveRightStateActivity(SceneNode node, GameController gameController, MoveRightStateActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        _node.Position = new(_node.Position.X + deltaTime / 1000.0f * _config.Speed, _node.Position.Y);
        return Status = StateActivityStatus.Active;
    }

    public void Remove() { }
}
