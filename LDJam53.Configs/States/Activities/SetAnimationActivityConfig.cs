using System.Collections.Generic;

namespace LDJam53.Configs.States.Activities;

public class SetAnimationActivityConfig : StateActivityConfig {
    public required String Target { get; set; }
    public required List<String> Textures { get; set; }
    public required Boolean Loop { get; set; } = true;
    public required Single Duration { get; set; } = 100;
}