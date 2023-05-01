using YamlDotNet.Core.Tokens;

namespace LDJam53.Configs.Components;

public class SimpleSideScrollingTileConfig {
    public required Single Width { get; set; }
    public required List<SceneNodeConfig> Children { get; set; } = new();
    public required List<ComponentConfig> Components { get; set; } = new();
    public Single Weight { get; set; } = 1;

    public SimpleSideScrollingTileConfig DeepClone() {
        return new SimpleSideScrollingTileConfig {
            Width = Width,
            Children = Children.Select(p=>p.DeepClone()).ToList(),
            Components = Components.Select(p=>p.DeepClone()).ToList()
        };
    }
}

public class SimpleSideScrollingTileComponentConfig : ComponentConfig {
    public required List<SimpleSideScrollingTileConfig> Tiles { get; set; } = new();

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public SimpleSideScrollingTileComponentConfig DeepClone() {
        return new SimpleSideScrollingTileComponentConfig {
            Tiles = Tiles.Select(p=>p.DeepClone()).ToList()
        };
    }
}
