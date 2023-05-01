using LDJam53.Configs.Loaders;

namespace LDJam53.Configs;

public class SceneConfig {
    public required String Id { get; set; }
    public required SceneNodeConfig Root { get; set; }
    public String? States { get; set; } = null;

    public static implicit operator SceneConfig(String input) => new YamlSceneLoader().LoadFromFile($"Configs/{input}Scene.yaml");
}
