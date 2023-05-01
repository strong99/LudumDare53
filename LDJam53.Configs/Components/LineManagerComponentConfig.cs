using System.Numerics;

namespace LDJam53.Configs.Components;

public class LineManagerComponentConfig : ComponentConfig
{
    public Single SpawnIntervalMs { get; set; } = 1500;
    public Single DifficultyIntervalMs { get; set; } = 5000;
    public Single Difficulty { get; set; } = 0;
    public required Int32 Count { get; set; }
    public required List<ObstacleTemplateComponentConfig> Obstacles { get; set; }
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public LineManagerComponentConfig DeepClone() {
        return new LineManagerComponentConfig {
            Count = Count,
            Obstacles = Obstacles.Select(p=>p.DeepClone()).ToList()
        };
    }
}
