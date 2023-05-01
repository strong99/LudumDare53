using System.Drawing;
using System.Numerics;

namespace LDJam53.Configs.Components;

public class ImageComponentConfig : ComponentConfig
{
    public required String Texture { get; set; }
    public required Vector2 Origin { get; set; }
    public required Int32 ImageEffects { get; set; }
    public required String Tint { get; set; } = "white";
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public ImageComponentConfig DeepClone() {
        return new ImageComponentConfig {
            Texture = Texture, 
            Origin = Origin,
            ImageEffects = ImageEffects,
            Tint = Tint
        };
    }
}
