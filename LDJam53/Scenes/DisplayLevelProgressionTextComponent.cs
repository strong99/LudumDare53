using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Drawing;

namespace LDJam53.Scenes;

[ComponentConfig<DisplayLevelProgressionTextComponentConfig>]
public class DisplayLevelProgressionTextComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly DisplayLevelProgressionTextComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Font? Font { get; set; }
    public Color Tint { get; set; } = Color.White;
    public SceneNode? Track { get; private set; }

    public DisplayLevelProgressionTextComponent(SceneNode node, DisplayLevelProgressionTextComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        Font = VideoDriver.GetFont(config.Font);
        Tint = ColorTranslator.FromHtml(config.Tint);

        Track = _node.Get(_config.Track);
    }

    public void Draw(Single deltaTime, Single totalTime) {
        Track ??= _node.Get(_config.Track);
        if (Font == null || Track == null) {
            return;
        }

        var str = String.Format(_config.Template, (Int32)(Track.Position.X / 1000.0f), (Int32)(_config.Distance / 1000.0f));
        var size = Font.GetSize(str);
        _gameController.VideoDriver.Draw(
            Font,
            str,
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new (_config.Origin.X * size.X, _config.Origin.Y * size.Y),
            _node.WorldScale,
            (GraphicEffects)_config.ImageEffects,
            1 - (_node.WorldOrder / 1000f)
        );
    }

    public void Remove() {

    }
}