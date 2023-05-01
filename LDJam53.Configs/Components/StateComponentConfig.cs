using static System.Net.Mime.MediaTypeNames;

namespace LDJam53.Configs.Components;

public class StateComponentConfig : ComponentConfig {
    public required String Set { get; set; }
    public required String State { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public StateComponentConfig DeepClone() {
        return new StateComponentConfig {
            Set = Set,
            State = State
        };
    }
}