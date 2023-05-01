using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;
using System.Linq;

namespace LDJam53.States.Activities;

[StateActivityConfig<SetAnimationActivityConfig>]
public class SetAnimationActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly SetAnimationActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public SetAnimationActivity(SceneNode node, GameController gameController, SetAnimationActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished)
            return Status;

        var component = (!String.IsNullOrWhiteSpace(_config.Target) ? _node.Get(_config.Target) : _node).GetComponent<AnimatedImageComponent>();
        if (component is not null) {
            component.Textures = _config.Textures.Select(t=> _gameController.VideoDriver.GetTexture(t)).ToArray();
            component.Duration =  _config.Duration;
            component.Loop =  _config.Loop;
        }

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
