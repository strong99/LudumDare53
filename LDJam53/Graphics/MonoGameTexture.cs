using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LDJam53.Graphics;

using CoreNumerics = System.Numerics;

public class MonoGameTexture : Texture {
    public Texture2D Texture { get; }
    public String Name { get => Texture.Name; }

    public CoreNumerics.Vector2 Size { get => new(Texture.Width, Texture.Height); }
    public MonoGameTexture(Texture2D texture) {
        Texture = texture;
    }
    public Rectangle Rectangle { get => new(0, 0, Texture.Width, Texture.Height); }
}
