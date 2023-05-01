using LDJam53.Configs.States.Activities;
using LDJam53.Scenes;

namespace LDJam53.States.Activities;

[StateActivityConfig<SetQuestDetailsActivityConfig>]
public class SetQuestDetailsActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly SetQuestDetailsActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public SetQuestDetailsActivity(SceneNode node, GameController gameController, SetQuestDetailsActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        if (Status == StateActivityStatus.Finished)
            return Status;

        var focussedActor = _node.GetComponent<QuestTakerComponent>()?.FocusedActor;
        var actor = focussedActor.GetComponent<ActorComponent>();
        var quest = focussedActor.GetComponent<QuestComponent>();

        _node.Get(_config.FromLabel).GetComponent<TextComponent>().Text = actor.Name;
        _node.Get(_config.ToLabel).GetComponent<TextComponent>().Text = $"{actor.Name}\n{quest.Destination}";
        _node.Get(_config.LevelLabel).GetComponent<TextComponent>().Text = quest.Route;
        _node.Get(_config.ScoreLabel).GetComponent<TextComponent>().Text = $"+{quest.Score}";

        return Status = StateActivityStatus.Finished;
    }

    public void Remove() { }
}
