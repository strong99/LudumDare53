using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Drawing;

namespace LDJam53.Scenes;

[ComponentConfig<TextComponentConfig>]
public class TextComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly TextComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public String Text { get => _config.Text; set => _config.Text = value; }
    public Font Font { get; set; }
    public Color Tint { get; set; } = Color.White;

    public TextComponent(SceneNode node, TextComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        Font = VideoDriver.GetFont(config.Font);
        Tint = ColorTranslator.FromHtml(config.Tint);
    }

    public void Draw(Single deltaTime, Single totalTime) {
        if (Font == null) {
            return;
        }

        var m = Font.GetSize(_config.Text);

        VideoDriver.Draw(
            Font,
            _config.Text,
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new(_config.Origin.X * m.X, _config.Origin.Y * m.Y),
            _node.WorldScale,
            GraphicEffects.None,
            1 - (_node.WorldOrder / 1000f)
        );
    }

    public void Remove() { }
}