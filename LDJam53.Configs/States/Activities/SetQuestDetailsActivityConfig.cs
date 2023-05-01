using System.Numerics;

namespace LDJam53.Configs.States.Activities;

public class SetQuestDetailsActivityConfig : StateActivityConfig {
    public String FromLabel { get; set; }
    public String ToLabel { get; set; }
    public String LevelLabel { get; set; }
    public String ScoreLabel { get; set; }
}

public class SetTextureActivityConfig : StateActivityConfig {
    public String Texture { get; set; }
    public String Target { get; set; }
}
