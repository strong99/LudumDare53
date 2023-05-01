using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<OnStateFinishedConditionConfig>]
public class OnStateFinishedCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly OnStateFinishedConditionConfig _config;
    private Boolean _finished = false;

    public OnStateFinishedCondition(SceneNode node, GameController gameController, OnStateFinishedConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;

        _node.OnEvent += ListenToParentOnEvent;
    }

    private void ListenToParentOnEvent(Object source, Scenes.EventArgs args) {
        if (args is NamedEventArgs a && a.Id == "StateFinished") {
            _finished = true;
        }
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        return _finished;
    }

    public void Remove() {
        _node.OnEvent -= ListenToParentOnEvent;
    }
}
