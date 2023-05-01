namespace LDJam53.Configs.States.Activities;

public class EnableNodeActivityConfig : StateActivityConfig {
    public required String Target { get; set; }
    public Boolean Enabled { get; set; } = true;
}