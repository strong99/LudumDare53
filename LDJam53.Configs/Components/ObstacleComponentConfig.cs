using System.Numerics;

namespace LDJam53.Configs.Components;

public class ObstacleComponentConfig : ComponentConfig {
    public required Single Width { get; set; }
    public required Int32 LanesHeight { get; set; }
    public required Vector2 Origin { get; set; }
    public required Int32 Lane { get; set; }
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public ObstacleComponentConfig DeepClone() {
        return new ObstacleComponentConfig {
            Width = Width,
            LanesHeight = LanesHeight,
            Origin = Origin,
            Lane = Lane
        };
    }
}