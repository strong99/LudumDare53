using LDJam53.Configs.Components;

namespace LDJam53.Scenes;

public interface ComponentFactory {
    Component Create(SceneNode node, ComponentConfig config);
}
