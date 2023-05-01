namespace LDJam53.Configs.Components;

public class QuestComponentConfig : ComponentConfig {

    public required String Destination { get; set; }
    public required String Route { get; set; }
    public required Int32 Score { get; set; }
    public Single Penalty { get; set; } = 0;

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }

    public QuestComponentConfig DeepClone() {
        return new QuestComponentConfig {
            Destination = Destination, 
            Route = Route,
            Score = Score
        };
    }
}
