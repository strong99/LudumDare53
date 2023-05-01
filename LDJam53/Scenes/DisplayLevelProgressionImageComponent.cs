using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Drawing;
using System.Numerics;

namespace LDJam53.Scenes;

[ComponentConfig<DisplayLevelProgressionImageComponentConfig>]
public class DisplayLevelProgressionImageComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly DisplayLevelProgressionImageComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Texture? FrontTexture { get; set; }
    public Texture? BackTexture { get; set; }
    public Color Tint { get; set; } = Color.White;
    public SceneNode? Track { get; private set; }

    public DisplayLevelProgressionImageComponent(SceneNode node, DisplayLevelProgressionImageComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        FrontTexture = VideoDriver.GetTexture(config.FrontTexture);
        BackTexture = VideoDriver.GetTexture(config.BackTexture);
        Tint = ColorTranslator.FromHtml(config.Tint);
    }

    public void Draw(Single deltaTime, Single totalTime) {
        Track ??= _node.Get(_config.Track);
        if (FrontTexture == null || BackTexture == null || Track == null) {
            return;
        }

        _gameController.VideoDriver.Draw(
            BackTexture,
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new (_config.Origin.X * FrontTexture.Size.X, _config.Origin.Y * FrontTexture.Size.Y),
            _node.WorldScale,
            (GraphicEffects)_config.ImageEffects,
            1 - (_node.WorldOrder / 1000f)
        );

        var progress = Track.Position.X / _config.Distance;
        _gameController.VideoDriver.Draw(
            FrontTexture,
            new Rectangle(0, 0, (Int32)(FrontTexture.Size.X * progress), (Int32)FrontTexture.Size.Y),
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new Vector2(_config.Origin.X * BackTexture.Size.X, _config.Origin.Y * BackTexture.Size.Y),
            _node.WorldScale,
            (GraphicEffects)_config.ImageEffects,
            1 - (_node.WorldOrder / 1000f)
        );
    }

    public void Remove() {

    }
}