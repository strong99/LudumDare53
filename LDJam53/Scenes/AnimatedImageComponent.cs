using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Drawing;
using System.Linq;

namespace LDJam53.Scenes;

[ComponentConfig<AnimatedImageComponentConfig>]
public class AnimatedImageComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly AnimatedImageComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    public Texture[] Textures { get; set; }
    public Color Tint { get; set; } = Color.White;
    public Single Duration { get => _config.Duration; set => _config.Duration = value; }
    public Boolean Loop { get => _config.Loop; set => _config.Loop = value; }
    public GraphicEffects Effect { get => (GraphicEffects)_config.ImageEffects; set => _config.ImageEffects = (Int32)value; }

    private Single _nextFrame;

    public AnimatedImageComponent(SceneNode node, AnimatedImageComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        Textures = config.Textures.Select(t=> VideoDriver.GetTexture(t)).ToArray();
        Tint = ColorTranslator.FromHtml(config.Tint);

        _nextFrame = _config.Duration;
    }

    public void Draw(Single deltaTime, Single totalTime) {
        _nextFrame += deltaTime;

        while(_nextFrame > _config.Duration) {
            _nextFrame -= _config.Duration;
            _config.KeyFrameIndex++;
        }

        var index = _config.KeyFrameIndex % Textures.Length;
        var texture = Textures[index];

        _gameController.VideoDriver.Draw(
            texture,
            _node.WorldPosition,
            Tint,
            _node.WorldRotation,
            new (_config.Origin.X * texture.Size.X, _config.Origin.Y * texture.Size.Y),
            _node.WorldScale,
            (GraphicEffects)_config.ImageEffects,
            1 - (_node.WorldOrder / 1000f)
        );
    }

    public void Remove() {
        
    }
}