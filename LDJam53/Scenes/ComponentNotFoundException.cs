using LDJam53.Configs.Components;
using System;

namespace LDJam53.Scenes;

public class ComponentNotFoundException : Exception {
    public String ComponentName { get; }
    public ComponentNotFoundException(String name) { ComponentName = name; }
    public ComponentNotFoundException(ComponentConfig component) { ComponentName = component.GetType().Name; }
}
