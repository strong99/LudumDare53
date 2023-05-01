namespace LDJam53.Configs.States.Conditions;

public class LineCollisionConditionConfig : StateConditionConfig {
    public String? Target { get; set; }
    public required String LineManager { get; set; }
}