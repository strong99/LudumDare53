using System.Numerics;

namespace LDJam53.Configs.Components;

public class SimpleSideScrollingFollowComponentConfig : ComponentConfig {
    public String Follow { get; set; }

    public required Single Scale { get; set; } = 0;
    public required Vector2 Offset { get; set; } = new(0,0);
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public SimpleSideScrollingFollowComponentConfig DeepClone() {
        return new SimpleSideScrollingFollowComponentConfig {
            Follow = Follow,
            Scale = Scale,
            Offset = Offset
        };
    }
}
