using LDJam53.Configs.Components;
using LDJam53.Graphics;
using System;
using System.Linq;
using System.Reflection;

namespace LDJam53.Scenes;

[ComponentConfig<SetTextComponentConfig>]
public class SetTextComponent : DrawableComponent {
    private readonly GameController _gameController;
    private readonly SetTextComponentConfig _config;
    private readonly SceneNode _node;

    private VideoDriver VideoDriver { get => _gameController.VideoDriver; }

    private WeakReference<Component>? _refTracker;
    private PropertyInfo? _cachedProperty;

    public SetTextComponent(SceneNode node, SetTextComponentConfig config, GameController gameController) {
        _config = config;
        _gameController = gameController;
        _node = node;

        LinkProperty();
    }

    public void Draw(Single deltaTime, Single totalTime) {
        if (_refTracker != null && _refTracker.TryGetTarget(out var c) && _cachedProperty != null) {
            var value = _cachedProperty.GetValue(c)?.ToString() ?? "";
            var formattedText = !String.IsNullOrWhiteSpace(_config.Template) ? String.Format(_config.Template, value) : value.ToString();
            if (_node.TryGetComponent<TextComponent>(out var targetComponent)) {
                targetComponent.Text = formattedText;
            }
        } else LinkProperty();
    }

    private void LinkProperty() {

        _refTracker = null;
        _cachedProperty = null;

        var node = _node.Get(_config.SourceNode);
        if (node == null) {
            return;
        }

        var component = node.Components.Where(p => p.GetType().Name == _config.SourceComponent).FirstOrDefault();
        if (component == null) {
            return;
        }

        var property = component.GetType().GetProperty(_config.SourceProperty);
        if (property == null) {
            return;
        }

        _refTracker = new(component);
        _cachedProperty = property;
    }

    public void Remove() { }
}