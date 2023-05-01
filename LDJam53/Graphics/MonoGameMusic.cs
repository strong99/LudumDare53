using System;
using Microsoft.Xna.Framework.Media;

namespace LDJam53.Graphics;

public class MonoGameMusic : Music {
    public String Name { get => Song.Name; }
    public Song Song { get; }
    public MonoGameMusic(Song song) {
        Song = song;
    }
}
