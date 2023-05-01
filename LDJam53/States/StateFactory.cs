using LDJam53.Configs.States;
using LDJam53.Scenes;
using System;
using System.Linq;

namespace LDJam53.States;

public class StateFactory {
    private StateSetConfig _stateSetConfig;
    private GameController _gameController;
    public String Id { get => _stateSetConfig.Id; }
    public StateFactory(GameController gameController, StateSetConfig stateSetConfig) {
        _stateSetConfig = stateSetConfig;
        _gameController = gameController;
    }
    public State Create(SceneNode node, String id) {
        return new State(node, _gameController, _stateSetConfig.States.First(p => p.Id == id));
    }
}
