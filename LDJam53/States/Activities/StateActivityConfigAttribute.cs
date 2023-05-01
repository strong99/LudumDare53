using System;
using LDJam53.Configs.States.Activities;

namespace LDJam53.States.Activities;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class StateActivityConfigAttribute : Attribute {
    public Type Type { get; }
    public StateActivityConfigAttribute(Type type) {
        Type = type;
    }
}

public sealed class StateActivityConfigAttribute<T> : StateActivityConfigAttribute where T : StateActivityConfig {
    public StateActivityConfigAttribute() : base(typeof(T)) {}
}
