using System;
using LDJam53.Configs.States.Conditions;

namespace LDJam53.States.Activities;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class StateConditionConfigAttribute : Attribute {
    public Type Type { get; }
    public StateConditionConfigAttribute(Type type) {
        Type = type;
    }
}

public sealed class StateConditionConfigAttribute<T> : StateConditionConfigAttribute where T : StateConditionConfig {
    public StateConditionConfigAttribute() : base(typeof(T)) {}
}
