using System.Collections.Generic;

namespace LDJam53.Configs.Components;

public class MusicComponentConfig : ComponentConfig
{
    public required String Song { get; set; }
    public required Single Volume { get; set; }
    public required Int32 SoundEffects { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public MusicComponentConfig DeepClone() {
        return new MusicComponentConfig {
            Song = Song,
            Volume = Volume,
            SoundEffects = SoundEffects
        };
    }
}
