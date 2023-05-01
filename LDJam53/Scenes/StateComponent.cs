using LDJam53.Configs.Components;
using LDJam53.Graphics;
using LDJam53.States;
using System;
using System.Drawing;

namespace LDJam53.Scenes;

[ComponentConfig<StateComponentConfig>]
public class StateComponent : UpdatableComponent {
    private readonly GameController _gameController;
    private readonly StateComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public State? State 
        {
        get => _state;
        set {
            var oldValue = _state;
            var newValue = _state = value;

            oldValue?.Remove();
        }
    }
    private State? _state;

    public StateComponent(SceneNode node, StateComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;
    }

    public void Update(Single deltaTime, Single totalTime) {
        if (State is null) SetState(_config.State);
        State?.Update(deltaTime, totalTime);
    }

    public void Remove() {
        State?.Remove();
    }

    public void SetState(String state) {
        State = _gameController.StateRepository.Get(_config.Set).Create(_node, state);
    }
}