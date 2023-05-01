namespace LDJam53.Configs.States.Activities;

public class ConcurrentActivityConfig : StateActivityConfig {
    public required Boolean FinishOnFirst { get; set; }
    public List<StateActivityConfig> Activities { get; set; }
}