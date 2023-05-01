namespace LDJam53.Configs.Components;

public class PlayerComponentConfig : ComponentConfig
{
    public required Int32 Score { get; set; }
    public required Int32 Deliveries { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public PlayerComponentConfig DeepClone() {
        return new PlayerComponentConfig {
            Score = Score,
            Deliveries = Deliveries
        };
    }
}
