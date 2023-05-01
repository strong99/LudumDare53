using LDJam53.Configs.States.Activities;
using LDJam53.Graphics;
using LDJam53.Scenes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Numerics;

namespace LDJam53.States.Activities;

[StateActivityConfig<FreeRoamRectangleActivityConfig>]
public class FreeRoamRectangleStateActivity : StateActivity {

    private readonly SceneNode _node;
    private readonly GameController _gameController;
    private readonly FreeRoamRectangleActivityConfig _config;

    public StateActivityStatus Status { get; private set; }

    public FreeRoamRectangleStateActivity(SceneNode node, GameController gameController, FreeRoamRectangleActivityConfig config) {
        _node = node;
        _gameController = gameController;
        _config = config;
    }

    public StateActivityStatus Update(System.Single deltaTime, System.Single totalTime) {
        Vector2 position = _node.Position;
        Vector2 direction = new ();
        if (_gameController.InputManager.PressActive("moveDown") && position.Y + deltaTime < _config.BottomRight.Y) {
            direction.Y += deltaTime / 100 * _config.Speed;
        }
        if (_gameController.InputManager.PressActive("moveUp") && position.Y - deltaTime > _config.TopLeft.Y) {
            direction.Y -= deltaTime / 100 * _config.Speed;
        }
        if (_gameController.InputManager.PressActive("moveLeft") && position.X - deltaTime > _config.TopLeft.X) {
            direction.X -= deltaTime / 100 * _config.Speed;
        }
        if (_gameController.InputManager.PressActive("moveRight") && position.X + deltaTime < _config.BottomRight.X) {
            direction.X += deltaTime / 100 * _config.Speed;
        }

        _node.Position = position +  direction;

        GraphicEffects a = direction.X < 0 ? GraphicEffects.FlipX : GraphicEffects.None;
        if (_node.TryGetComponent<AnimatedImageComponent>(out var component1)) {
            component1.Effect = a;
        }
        if (_node.TryGetComponent<ImageComponent>(out var component2)) {
            component2.Effect = a;
        }

        return Status = StateActivityStatus.Active;
    }

    public void Remove() { }
}
