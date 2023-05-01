using System.Drawing;
using System.Numerics;

namespace LDJam53.Configs.Components;

public class DisplayLevelProgressionTextComponentConfig : ComponentConfig
{
    public required Single Distance { get; set; }
    public required String Track { get; set; }
    public required String Template { get; set; } = "{0}/{1}";
    public required Vector2 Origin { get; set; }
    public required Int32 ImageEffects { get; set; }
    public required String Tint { get; set; } = "white";
    public String Font { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public DisplayLevelProgressionTextComponentConfig DeepClone() {
        return new DisplayLevelProgressionTextComponentConfig {
            Distance = Distance,
            Font = Font,
            Track = Track,
            Template = Template,
            Origin = Origin,
            ImageEffects = ImageEffects,
            Tint = Tint
        };
    }
}
