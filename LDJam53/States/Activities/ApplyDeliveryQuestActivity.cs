using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace LDJam53.States.Activities;

[StateActivityConfig<ApplyDeliveryQuestActivityConfig>]
public class ApplyDeliveryQuestActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly ApplyDeliveryQuestActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    private List<StateActivity> _activities = new();

    public ApplyDeliveryQuestActivity(SceneNode node, GameController gameController, ApplyDeliveryQuestActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished) {
            return Status;
        }

        var questComponent = _node.Get(_config.Quest)?.GetComponent<QuestComponent>();
        var playerComponent = _node.Get(_config.Target)?.GetComponent<PlayerComponent>();
        if (playerComponent != null && questComponent != null) {
            playerComponent.Deliveries++;
            playerComponent.Score += questComponent.Score;
        }

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
