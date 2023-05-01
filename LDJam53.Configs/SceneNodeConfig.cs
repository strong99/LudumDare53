using System.Numerics;
using LDJam53.Configs.Components;
using LDJam53.Configs.Loaders;

namespace LDJam53.Configs;

public class SceneNodeConfig {
    public required String Id { get; set; }
    public Vector2 Position { get; set; } = new();
    public Vector2 Scale { get; set; } = new(1, 1);
    public Single Rotation { get; set; } = 0;
    public Single Order { get; set; } = 0;
    public required List<SceneNodeConfig> Children { get; set; } = new();
    public required List<ComponentConfig> Components { get; set; } = new();
    public Boolean Enabled { get; set; } = true;

    public SceneNodeConfig DeepClone() {
        return new() {
            Id = Id,
            Position = Position,
            Scale = Scale,
            Rotation = Rotation,
            Enabled = Enabled,
            Children = Children.Select(p => p.DeepClone()).ToList(),
            Components = Components.Select(p => p.DeepClone()).ToList()
        };
    }

    public static implicit operator SceneNodeConfig(String input) => new YamlSceneLoader().LoadFromFile($"Configs/{input}Scene.yaml").Root;
}

