using LDJam53.Configs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace LDJam53;

public class InputManager {
    private Dictionary<String, Keys[]> _mappings = new();
    private Dictionary<Keys, InputEvent> _states = new();

    public InputManager(AppConfig _appConfig) {
        foreach (var key in _appConfig.Keys) {
            _mappings[key.Key] = key.Value.Select(p => Enum.TryParse<Keys>(p, true, out var result) ? result:  Keys.None).ToArray();
        }
    }

    public void Update(Single deltaTime, Single totalTime) {
        var keys = _mappings.Values.SelectMany(p=>p).Distinct().ToList();
        foreach (var keyCode in keys) {
            var state = Keyboard.GetState();
            var isDown = state.IsKeyDown(keyCode);

            if (!_states.TryGetValue(keyCode, out var prevKeyState)) prevKeyState = InputEvent.Released;

            var newState = InputEvent.None;
            if (isDown) newState |= InputEvent.PressActive;
            else newState |= InputEvent.Released;

            if (prevKeyState == InputEvent.Released && isDown) newState |= InputEvent.PressStart;
            else if (prevKeyState.HasFlag(InputEvent.PressActive) && !isDown) newState |= InputEvent.PressEnd;

            _states[keyCode] = newState;
        }
    }

    public Boolean PressedStart(String key) {
        var keyCode = _mappings[key];
        return keyCode.Any(p => _states[p].HasFlag(InputEvent.PressStart));
    }

    public Boolean PressActive(String key) {
        var keyCode = _mappings[key];
        return keyCode.Any(p => _states[p].HasFlag(InputEvent.PressActive));
    }

    public Boolean PressEnded(String key) {
        var keyCode = _mappings[key];
        return keyCode.Any(p => _states[p].HasFlag(InputEvent.PressEnd)) && !PressActive(key);
    }

    public Boolean Released(String key) {
        return !PressActive(key);
    }
}
