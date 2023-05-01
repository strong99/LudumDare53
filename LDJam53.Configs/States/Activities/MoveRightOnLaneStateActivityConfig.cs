namespace LDJam53.Configs.States.Activities;

public class MoveRightOnLaneStateActivityConfig : StateActivityConfig {
    public required Single Speed { get; set; } = 1;
    public required Single Order { get; set; } = 0;
    public required String LineManager { get; set; }
    public required Single SpeedIncreaseInterval {get;set;}
    public required Single SpeedIncreaseMultiplier { get; set; }
}