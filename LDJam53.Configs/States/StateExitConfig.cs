using LDJam53.Configs.States.Conditions;

namespace LDJam53.Configs.States;

public class StateExitConfig {
    public required String Destination { get; set; }
    public required List<StateConditionConfig> Conditions { get; set; } = new();
}
