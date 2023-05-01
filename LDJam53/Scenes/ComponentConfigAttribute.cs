using LDJam53.Configs.Components;
using System;

namespace LDJam53.Scenes;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ComponentConfigAttribute : Attribute {
    public Type Type { get; }
    public ComponentConfigAttribute(Type type) {
        Type = type;
    }
}

public sealed class ComponentConfigAttribute<T> : ComponentConfigAttribute where T : ComponentConfig {
    public ComponentConfigAttribute() : base(typeof(T)) {}
}
