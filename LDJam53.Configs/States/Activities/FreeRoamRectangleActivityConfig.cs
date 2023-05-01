using System.Numerics;

namespace LDJam53.Configs.States.Activities;

public class FreeRoamRectangleActivityConfig : StateActivityConfig {
    public required Single Speed { get; set; } = 1;
    public required Vector2 TopLeft { get; set; }
    public required Vector2 BottomRight { get; set; }
}