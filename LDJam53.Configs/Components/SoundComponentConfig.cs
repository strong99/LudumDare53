using System.Collections.Generic;

namespace LDJam53.Configs.Components;

public class SoundComponentConfig : ComponentConfig
{
    public required String Sound { get; set; }
    public required Single Volume { get; set; }
    public required Int32 SoundEffects { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public SoundComponentConfig DeepClone() {
        return new SoundComponentConfig {
            Sound = Sound,
            Volume = Volume,
            SoundEffects = SoundEffects
        };
    }
}
