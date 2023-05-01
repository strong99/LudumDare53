using System.Numerics;

namespace LDJam53.Configs.Components;

public class ObstacleTemplateComponentConfig : SceneNodeConfig {
    public required Single Weight { get; set; } = 32;
    public required Single Difficulty { get; set; } = 1;
    public required Single Width { get; set; } = 64;
    public required Int32 LanesHeight { get; set; } = 1;
    public required Vector2 Origin { get; set; }
    public new ObstacleTemplateComponentConfig DeepClone() {
        return new ObstacleTemplateComponentConfig {
            Weight = Weight,
            Difficulty = Difficulty,
            Width = Width,
            LanesHeight = LanesHeight,
            Id = Id,
            Position = Position,
            Scale = Scale,
            Rotation = Rotation,
            Origin = Origin,
            Children = Children.Select(p => p.DeepClone()).ToList(),
            Components = Components.Select(p => p.DeepClone()).ToList()
        };
    }
}
