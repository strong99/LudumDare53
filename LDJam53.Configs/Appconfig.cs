using System.Numerics;

namespace LDJam53.Configs;

public class AppConfig {
    public required String StartScene { get; set; }
    public required String PlayerController { get; set; }
    public Boolean FullScreen { get; set; } = false;
    public Vector2 Resolution { get; set; } = new(640, 480);
    public Dictionary<String, String[]> Keys { get; set; }
}
