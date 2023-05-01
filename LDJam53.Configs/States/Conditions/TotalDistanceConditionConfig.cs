namespace LDJam53.Configs.States.Conditions;

public class TotalDistanceConditionConfig : StateConditionConfig {
    public required String Track { get; set; }
    public required Int32 Distance { get; set; }
}
