using LDJam53.Configs;
using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using System;

namespace LDJam53.States.Conditions;

[StateConditionConfig<UserKeyboardInputEventConditionConfig>]
public class UserKeyboardInputEventCondition : StateCondition {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly UserKeyboardInputEventConditionConfig _config;

    public UserKeyboardInputEventCondition(SceneNode node, GameController gameController, UserKeyboardInputEventConditionConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public Boolean IsMet(Single deltaTime, Single totalTime) {
        var expectedKeyState = (InputEvent)_config.State;
        var keys = _config.Keys is null ? new[] { _config.Key } : _config.Keys;
        var anyPressed = false;
        foreach (var key in keys) {
            if (key is null) continue;

            var result = false;
            if (expectedKeyState == InputEvent.Released) {
                result = _gameController.InputManager.Released(key);
                anyPressed = anyPressed || !result;
            }
            else if (expectedKeyState == InputEvent.PressStart) {
                result = _gameController.InputManager.PressedStart(key);
                anyPressed = anyPressed || result;
            }
            else if (expectedKeyState == InputEvent.PressEnd) {
                result = _gameController.InputManager.PressEnded(key);
                anyPressed = anyPressed || !result;
            }
            else if (expectedKeyState == InputEvent.PressActive) {
                result = _gameController.InputManager.PressActive(key);
                anyPressed = anyPressed || result;
            }
            else throw new NotSupportedException($"Key state {expectedKeyState} not supported");

            if (result 
             && !expectedKeyState.HasFlag(InputEvent.Released)) {
                return true;
            }
        }
        return expectedKeyState.HasFlag(InputEvent.Released) && !anyPressed;
    }

    public void Remove() { }
}
