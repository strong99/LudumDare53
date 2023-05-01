using System.Drawing;
using System.Numerics;

namespace LDJam53.Configs.Components;

public class AnimatedImageComponentConfig : ComponentConfig
{
    public required String[] Textures { get; set; }
    public required Single Duration { get; set; } = 100;
    public required Int32 KeyFrameIndex { get; set; }
    public required Vector2 Origin { get; set; }
    public required Boolean Loop { get; set; } = true;
    public required Int32 ImageEffects { get; set; }
    public required String Tint { get; set; } = "white";
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public AnimatedImageComponentConfig DeepClone() {
        return new AnimatedImageComponentConfig {
            Loop = Loop,
            Textures = Textures.ToArray(),
            Duration = Duration,
            KeyFrameIndex = KeyFrameIndex,  
            Origin = Origin,
            ImageEffects = ImageEffects ,
            Tint = Tint
        };
    }
}
