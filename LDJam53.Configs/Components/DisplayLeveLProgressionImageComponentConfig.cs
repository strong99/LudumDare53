using System.Numerics;

namespace LDJam53.Configs.Components;

public class DisplayLevelProgressionImageComponentConfig : ComponentConfig {
    public required Single Distance { get; set; }
    public required String Track { get; set; }
    public required String BackTexture { get; set; }
    public required String FrontTexture { get; set; }
    public required Vector2 Origin { get; set; }
    public required Int32 ImageEffects { get; set; }
    public required String Tint { get; set; } = "white";

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public DisplayLevelProgressionImageComponentConfig DeepClone() {
        return new DisplayLevelProgressionImageComponentConfig {
            Distance = Distance,
            Track = Track,
            BackTexture = BackTexture,
            FrontTexture = FrontTexture, 
            Origin = Origin,
            ImageEffects = ImageEffects,
            Tint = Tint
        };
    }
}
