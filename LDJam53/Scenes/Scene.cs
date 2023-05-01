using LDJam53.Configs;
using System.Collections.Generic;
using System;

namespace LDJam53.Scenes;

public class Scene {
    private SceneConfig _config;
    public SceneNode Root { get; }

    public Scene(ComponentFactory factory) {
        _config = new() {
            Id = "new",
            Root = new() {
                Id = "new",
                Children = new(),
                Components = new()
            }
        };
        Root = new SceneNode(factory, _config.Root);
    }

    public Scene(ComponentFactory factory, SceneConfig config) {
        _config = config;
        Root = new SceneNode(factory, config.Root);
    }

    public void Update(Single deltaTime, Single totalTime) {
        Root.Update(deltaTime, totalTime);
    }

    public void Draw(Dictionary<DrawableComponent, Single> queue) {
        Root.Draw(queue);
    }
}
