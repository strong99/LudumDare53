using System;
using Microsoft.Xna.Framework.Audio;

namespace LDJam53.Graphics;

public class MonoGameSound : Sound {
    public String Name { get => Sound.Name; }
    public SoundEffect Sound { get; }
    public MonoGameSound(SoundEffect sound) {
        Sound = sound;
    }
}
