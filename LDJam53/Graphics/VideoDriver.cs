using System;
using System.Drawing;
using System.Numerics;

namespace LDJam53.Graphics;

public interface VideoDriver {
    Texture GetTexture(String name);
    Font GetFont(String font);
    Vector2 ViewportSize { get; }
    void Draw(Texture texture, Vector2 position, Color tint, Single absoluteRotation, Vector2 origin, Vector2 scale, GraphicEffects effect, Single z);
    void Draw(Texture texture, Rectangle source, Vector2 position, Color tint, Single absoluteRotation, Vector2 origin, Vector2 scale, GraphicEffects effect, Single z);
    void Draw(Font font, String text, Vector2 position, Color tint, Single absoluteRotation, Vector2 origin, Vector2 scale, GraphicEffects effect, Single z);
}
