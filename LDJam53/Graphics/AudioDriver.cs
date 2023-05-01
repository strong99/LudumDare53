using System;
using System.Drawing;
using System.Numerics;

namespace LDJam53.Graphics;

public interface Music {
    String Name { get; }
}

public interface Sound {
    String Name { get; }
}

public interface AudioDriver {
    Music GetMusic(String name);
    Sound GetSound(String font);
    void Play(Music music, Single volume, Boolean loop);
    void Play(Sound sound, Single volume, Vector2? position = null, Object? instance = null);
}
