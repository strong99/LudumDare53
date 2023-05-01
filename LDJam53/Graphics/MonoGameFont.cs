using Microsoft.Xna.Framework.Graphics;
using System;

namespace LDJam53.Graphics;

using CoreNumerics = System.Numerics;

public class MonoGameFont : Font {
    public SpriteFont Font { get; }
    public String Name { get => Font.Texture.Name; }
    public MonoGameFont(SpriteFont font) {
        Font = font;
    }
    public CoreNumerics.Vector2 GetSize(String text) {
        var v = Font.MeasureString(text);
        return new(v.X, v.Y);
    }
}
