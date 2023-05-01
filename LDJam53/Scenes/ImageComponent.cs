using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Drawing;

namespace LDJam53.Scenes;

[ComponentConfig<ImageComponentConfig>]
public class ImageComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly ImageComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Texture? Texture { get; set; }
    public Color Tint { get; set; } = Color.White;
    public GraphicEffects Effect { get => (GraphicEffects)_config.ImageEffects; set => _config.ImageEffects = (Int32)value; }

    public ImageComponent(SceneNode node, ImageComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        Texture = VideoDriver.GetTexture(config.Texture);
        Tint = ColorTranslator.FromHtml(config.Tint);
    }

    public void Draw(Single deltaTime, Single totalTime) {
        if (Texture == null) {
            return;
        }

        _gameController.VideoDriver.Draw(
            Texture,
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new (_config.Origin.X * Texture.Size.X, _config.Origin.Y * Texture.Size.Y),
            _node.WorldScale,
            (GraphicEffects)_config.ImageEffects,
            1 - (_node.WorldOrder / 1000f)
        );
    }

    public void Remove() {

    }
}