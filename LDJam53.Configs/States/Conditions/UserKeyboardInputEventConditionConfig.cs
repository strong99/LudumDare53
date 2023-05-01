namespace LDJam53.Configs.States.Conditions;

public class UserKeyboardInputEventConditionConfig : StateConditionConfig {
    public String? Key { get; set; } = null;
    public String[]? Keys { get; set; } = null;
    public required Int32 State { get; set; } = 1;
}