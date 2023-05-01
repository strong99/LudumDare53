using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;

namespace LDJam53.States.Activities;

[StateActivityConfig<SetTextureActivityConfig>]
public class SetTextureActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly SetTextureActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public SetTextureActivity(SceneNode node, GameController gameController, SetTextureActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished)
            return Status;

        (!String.IsNullOrWhiteSpace(_config.Target) ? _node.Get(_config.Target) : _node).GetComponent<ImageComponent>().Texture = _gameController.VideoDriver.GetTexture(_config.Texture);

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
