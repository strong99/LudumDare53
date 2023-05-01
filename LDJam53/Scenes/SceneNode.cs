using LDJam53.Configs;
using LDJam53.Configs.Loaders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;

namespace LDJam53.Scenes;

public class SceneNode {
    private readonly SceneNodeConfig _config;

    private Boolean _dirty = true;

    public SceneNode Root { get => Parent != null ? Parent.Root : this; }

    public Vector2 Position { get => _config.Position; set { _config.Position = value; SetDirty(); } }
    public Vector2 Scale { get => _config.Scale; set { _config.Scale = value; SetDirty(); } }
    public Single Rotation { get => _config.Rotation; set { _config.Rotation = value; SetDirty(); } }

    public Boolean Enabled { get => _config.Enabled; set { _config.Enabled = value; } }

    public Single Order { get => _config.Order; set => _config.Order = value; }
    public Single WorldOrder { get => Parent is null ? _config.Order : _config.Order + Parent.WorldOrder; }


    public Matrix3x2 Transformation { get; private set; }
    public Matrix3x2 WorldTransformation { get; private set; }
    public Vector2 WorldPosition { get; private set; }
    public Vector2 WorldScale { get; private set; }
    public Single WorldRotation { get; private set; }

    public SceneNode? Parent {
        get => _parent;
        set {
            var oldValue = _parent;
            var newValue = _parent = value;

            SetDirty();

            oldValue?.RemoveChild(this);
            newValue?.AddChild(this);
        }
    }
    private SceneNode? _parent;

    public IEnumerable<SceneNode> Children { get => _children; }
    private List<SceneNode> _children = new();

    public List<Component> Components { get => _components; }
    public String Id { get => _config.Id; }

    private readonly List<Component> _components = new();
    private readonly List<UpdatableComponent> _updatableComponents = new();
    private readonly List<DrawableComponent> _drawableComponents = new();

    private void SetDirty() {
        _dirty = true;
    }

    public SceneNode(ComponentFactory componentFactory, SceneNodeConfig config) {
        _config = config;

        if (_config.Children != null) {
            foreach (var c in _config.Children) {
                AddChild(
                    new SceneNode(componentFactory, c)
                );
            }
        }

        if (_config.Components != null) {
            foreach (var c in _config.Components) {
                AddComponent(
                    componentFactory.Create(this, c)
                );
            }
        }
    }

    public void AddChild(SceneNode node) {
        if (_children.Contains(node)) return;

        _children.Add(node);
        node.Parent = this;
    }

    public void RemoveChild(SceneNode node) {
        _children.Remove(node);
        if (node.Parent == this) node.Parent = null;
    }

    public void AddComponent(Component component) {
        _components.Add(component);
        if (component is UpdatableComponent u) _updatableComponents.Add(u);
        if (component is DrawableComponent d) _drawableComponents.Add(d);
    }

    public void RemoveComponent(Component component) {
        _components.Remove(component);
        if (component is UpdatableComponent u) _updatableComponents.Remove(u);
        if (component is DrawableComponent d) _drawableComponents.Remove(d);
    }

    public void UpdateTransformIfNeeded() {
        if (_dirty) {
            UpdateTransform();
        }
    }

    public void UpdateTransform() {
        _dirty = false;

        Transformation =
                Matrix3x2.CreateScale(Scale) *
                Matrix3x2.CreateRotation(Rotation) *
                Matrix3x2.CreateTranslation(Position);

        WorldTransformation = Parent is null ? Transformation : Transformation * Parent.WorldTransformation;

        WorldPosition = WorldTransformation.Translation;
        WorldRotation = Parent is null ? Rotation : Rotation * Parent.WorldRotation;
        WorldScale = Parent is null ? Scale : Scale * Parent.WorldScale;

        foreach (var c in _children) c.UpdateTransform();
    }

    public void Update(Single deltaTime, Single totalTime) {
        if (!Enabled) return;

        UpdateTransformIfNeeded();

        var components = new List<UpdatableComponent>(_updatableComponents);
        foreach (var c in components) c.Update(deltaTime, totalTime);

        var children = new List<SceneNode>(_children);
        foreach (var c in children) c.Update(deltaTime, totalTime);
    }

    public void Draw(Dictionary<DrawableComponent, Single> queue) {
        if (!Enabled) return;

        var components = new List<DrawableComponent>(_drawableComponents);
        foreach (var c in components) queue.Add(c, WorldOrder);

        var children = new List<SceneNode>(_children);
        foreach (var c in children) c.Draw(queue);
    }

    public void Remove() {
        var children = _children.ToList();
        _children.Clear();
        foreach (var c in children) c.Remove();

        var components = _components.ToList();
        _components.Clear();
        _drawableComponents.Clear();
        _updatableComponents.Clear();
        foreach (var c in components) c.Remove();

        Parent = null;
    }

    public Boolean HasComponent<T>() where T : Component {
        return _components.Any(i => i is T);
    }

    public T GetComponent<T>() where T : Component {
        return (T)_components.First(i => i is T);
    }

    public Boolean TryGetComponent<T>([NotNullWhen(true)] out T? component) where T : Component {
        component = (T)_components.FirstOrDefault(i => i is T);
        return component is not null;
    }

    public SceneNode? Get(String path) {
        SceneNode[] current = new[] { this };
        if (path.StartsWith("//")) {
            path = path[2..];
            current[0] = this.Root;
        }

        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        foreach (var s in segments) {
            if (s == "..") {
                current = current.Select(p => p.Parent).ToArray();
            }
            else {
                current = current.SelectMany(c => c.Children.Where(p => p.Id == s)).ToArray();
            }
        }
        return current.FirstOrDefault();
    }

    public void Get(Object follow) {
        throw new NotImplementedException();
    }

    public IEnumerable<SceneNode> FindWithComponent<T>() where T : Component {
        var list = new List<SceneNode>();
        FindWithComponent<T>(list);
        return list;
    }

    private void FindWithComponent<T>(List<SceneNode> refList) where T : Component {
        foreach (var c in Children) {
            if (c.TryGetComponent<T>(out var component)) {
                refList.Add(c);
                continue;
            }
            c.FindWithComponent<T>(refList);
        }
    }

    public event OnEvent OnEvent;
    internal void Inform(EventArgs args) {
        OnEvent?.Invoke(this, args);
    }
}

public delegate void OnEvent(Object source, EventArgs args);

public interface EventArgs {
    
}

public class NamedEventArgs : EventArgs {
    public required String Id { get; init; }
}
