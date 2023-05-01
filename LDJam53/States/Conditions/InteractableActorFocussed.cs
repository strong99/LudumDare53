using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<InteractableActorFocussedConditionConfig>]
public class InteractableActorFocussedCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly InteractableActorFocussedConditionConfig _config;

    public InteractableActorFocussedCondition(SceneNode node, GameController gameController, InteractableActorFocussedConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        return _node.GetComponent<QuestTakerComponent>()?.FocusedActor is not null;
    }

    public void Remove() { }
}
