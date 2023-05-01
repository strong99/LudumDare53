using LDJam53.Configs.Components;
using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;

namespace LDJam53.States.Activities;

[StateActivityConfig<LoadSceneFromQuestActivityConfig>]
public class LoadSceneFromQuestActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly LoadSceneFromQuestActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public LoadSceneFromQuestActivity(SceneNode node, GameController gameController, LoadSceneFromQuestActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status != StateActivityStatus.Finished) {
            QuestComponent? quest = null;
            if (_node.TryGetComponent<QuestTakerComponent>(out var questTakerComponent)) {
                var focussedActor = questTakerComponent?.FocusedActor;
                quest = focussedActor?.GetComponent<QuestComponent>();
            }
            else if (_node.Root.TryGetComponent<QuestComponent>(out var questComponent)) {
                quest = questComponent;
            }
            if (quest == null) throw new ComponentNotFoundException(nameof(QuestComponent));

            var nextScene = _config.Destination ? quest.Destination : quest.Route;

            var newScene = _gameController.SetScene(nextScene);
            newScene.Root.AddComponent(new QuestComponent(newScene.Root, new QuestComponentConfig() {
                Score = quest.Score,
                Destination = quest.Destination,
                Route = quest.Route
            }, _gameController));

            // Sync player data
            var initialPlayerData = _node.Get("//layerPlay/player")?.GetComponent<PlayerComponent>();
            var newPlayerData = newScene.Root.Get("//layerPlay/player")?.GetComponent<PlayerComponent>();
            if (initialPlayerData is not null && newPlayerData is not null) {
                newPlayerData.Score = initialPlayerData.Score;
                newPlayerData.Deliveries = initialPlayerData.Deliveries;
            }
        }
        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
