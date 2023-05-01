using LDJam53.Configs.States;
using LDJam53.Configs.States.Activities;
using LDJam53.Configs.States.Conditions;
using LDJam53.Scenes;
using LDJam53.States.Activities;
using LDJam53.States.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LDJam53.States;

public class StateRepository {
    private List<StateFactory> _factory;
    private GameController _gameController;
    public StateRepository(GameController gameController, StateSetsConfig sets) {
        _gameController = gameController;
        _factory = sets.Sets.Select(s => new StateFactory(gameController, s)).ToList();
    }
    public StateFactory Get(String id) {
        return _factory.First(f => f.Id == id);
    }

    public StateActivity? CreateActivity(SceneNode node, GameController gameController, StateActivityConfig config) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies) {
            var types = assembly.GetTypes();
            foreach (var type in types) {
                if (type.IsAssignableTo(typeof(StateActivity))
                 && type.IsClass && !type.IsAbstract
                 && type.GetCustomAttribute<StateActivityConfigAttribute>()?.Type == config.GetType()) {
                    return (StateActivity)Activator.CreateInstance(type, node, gameController, config);
                }
            }
        }
        throw new StateActivityNotFoundException(config);
    }

    public StateCondition CreateCondition(SceneNode node, GameController gameController, StateConditionConfig config) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies) {
            var types = assembly.GetTypes();
            foreach (var type in types) {
                if (type.IsAssignableTo(typeof(StateCondition))
                 && type.IsClass && !type.IsAbstract
                 && type.GetCustomAttribute<StateConditionConfigAttribute>()?.Type == config.GetType()) {
                    return (StateCondition)Activator.CreateInstance(type, node, gameController, config);
                }
            }
        }
        throw new StateConditionNotFoundException(config);
    }
}

public class StateActivityNotFoundException : Exception {
    public StateActivityNotFoundException(StateActivityConfig config) {

    }
}

public class StateConditionNotFoundException : Exception {
    public StateConditionNotFoundException(StateConditionConfig config) {

    }
}
