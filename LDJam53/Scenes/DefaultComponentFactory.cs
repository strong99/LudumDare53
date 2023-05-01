using LDJam53.Configs.Components;
using System;
using System.Reflection;

namespace LDJam53.Scenes;

public class DefaultComponentFactory : ComponentFactory {
    public GameController GameController { get; set; }

    public Component Create(SceneNode node, ComponentConfig config) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies) {
            var types = assembly.GetTypes();
            foreach (var type in types) {
                if (type.IsAssignableTo(typeof(Component))
                 && type.IsClass && !type.IsAbstract
                 && type.GetCustomAttribute<ComponentConfigAttribute>()?.Type == config.GetType()) {
                    return (Component)Activator.CreateInstance(type, node, config, GameController);
                }
            }
        }
        throw new ComponentNotFoundException(config);
    }
}
