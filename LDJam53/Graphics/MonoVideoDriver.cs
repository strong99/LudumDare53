using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDJam53.Graphics;

using CoreNumerics = System.Numerics;
using CoreDrawing = System.Drawing;

public class MonoGameVideoDriver : VideoDriver {
    private readonly SpriteBatch _spriteBatch;
    private readonly ContentManager _contentManager;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private HashSet<WeakReference<MonoGameTexture>> _textures = new();
    private HashSet<WeakReference<MonoGameFont>> _fonts = new();

    public MonoGameVideoDriver(SpriteBatch spriteBatch, ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager) {
        _spriteBatch = spriteBatch;
        _contentManager = contentManager;
        _graphicsDeviceManager = graphicsDeviceManager;
    }

    public CoreNumerics.Vector2 ViewportSize { get => new(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight); }
    public void Draw(Texture texture, CoreNumerics.Vector2 position, CoreDrawing.Color tint, Single rotation, CoreNumerics.Vector2 origin, CoreNumerics.Vector2 scale, GraphicEffects effect, Single z) {
        if (texture is not MonoGameTexture monoTexture) throw new Exception("Unsupported texture format");

        SpriteEffects monoEffect = SpriteEffects.None;

        if (effect.HasFlag(GraphicEffects.FlipY)) monoEffect |= SpriteEffects.FlipVertically;
        if (effect.HasFlag(GraphicEffects.FlipX)) monoEffect |= SpriteEffects.FlipHorizontally;

        _spriteBatch.Draw(monoTexture.Texture, position, monoTexture.Rectangle, new Color(tint.R, tint.G, tint.B, tint.A), rotation, origin, scale, monoEffect, z);
    }

    public void Draw(Texture texture, CoreDrawing.Rectangle source, CoreNumerics.Vector2 position, CoreDrawing.Color tint, Single rotation, CoreNumerics.Vector2 origin, CoreNumerics.Vector2 scale, GraphicEffects effect, Single z) {
        if (texture is not MonoGameTexture monoTexture) throw new Exception("Unsupported texture format");

        SpriteEffects monoEffect = SpriteEffects.None;

        if (effect.HasFlag(GraphicEffects.FlipY)) monoEffect |= SpriteEffects.FlipVertically;
        if (effect.HasFlag(GraphicEffects.FlipX)) monoEffect |= SpriteEffects.FlipHorizontally;

        _spriteBatch.Draw(monoTexture.Texture, position, new(source.X, source.Y, source.Width, source.Height), new Color(tint.R, tint.G, tint.B, tint.A), rotation, origin, scale, monoEffect, z);
    }

    public void Draw(Font font, String text, CoreNumerics.Vector2 position, CoreDrawing.Color tint, Single rotation, CoreNumerics.Vector2 origin, CoreNumerics.Vector2 scale, GraphicEffects effect, Single z) {
        if (font is not MonoGameFont monoFont) throw new Exception("Unsupported font format");

        SpriteEffects monoEffect = SpriteEffects.None;

        if (effect.HasFlag(GraphicEffects.FlipY)) monoEffect &= SpriteEffects.FlipVertically;
        if (effect.HasFlag(GraphicEffects.FlipX)) monoEffect &= SpriteEffects.FlipHorizontally;

        _spriteBatch.DrawString(monoFont.Font, text, position, new Color(tint.R, tint.G, tint.B, tint.A), rotation, origin, scale, monoEffect, z);
    }

    public Font GetFont(String name) {
        var fonts = _fonts.ToArray();
        foreach (var fontRef in fonts) {
            if (fontRef.TryGetTarget(out var t)) {
                if (t.Name == name) {
                    return t;
                }
            }
            else {
                _fonts.Remove(fontRef);
            }
        }

        var font = new MonoGameFont(_contentManager.Load<SpriteFont>(name));
        _fonts.Add(new(font));

        return font;
    }

    public Texture GetTexture(String name) {
        var textures = _textures.ToArray();
        foreach (var textureRef in textures) {
            if (textureRef.TryGetTarget(out var t)) {
                if (t.Name == name) {
                    return t;
                }
            }
            else {
                _textures.Remove(textureRef);
            }
        }

        var texture = new MonoGameTexture(_contentManager.Load<Texture2D>(name));
        _textures.Add(new(texture));

        return texture;
    }
}
