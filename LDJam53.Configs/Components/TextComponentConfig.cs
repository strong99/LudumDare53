using System.Drawing;
using System.Numerics;

namespace LDJam53.Configs.Components;

public class TextComponentConfig : ComponentConfig
{
    public required String Text { get; set; }
    public required String Font { get; set; } = "default";
    public required Vector2 Origin { get; set; }
    public required Int32 ImageEffects { get; set; }
    public required String Tint { get; set; } = "white";

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public TextComponentConfig DeepClone() {
        return new TextComponentConfig {
            Text = Text,
            Font = Font,
            Origin = Origin,
            ImageEffects = ImageEffects,
            Tint = Tint
        };
    }
}
