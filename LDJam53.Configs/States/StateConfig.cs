using LDJam53.Configs.States.Activities;

namespace LDJam53.Configs.States;
public class StateConfig {
    public required String Id { get; set; }
    public required List<StateActivityConfig> Activities { get; set; }
    public required List<StateActivityConfig> End { get; set; }
    public required List<StateExitConfig> Exits { get; set; }
}
