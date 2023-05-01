using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using LDJam53.Configs.Components;

namespace LDJam53.Configs.Loaders;

public class YamlSceneLoader {
    public SceneConfig LoadFromFile(String file) {
        var fileData = File.ReadAllText(file);
        return Load(fileData);
    }

    public SceneConfig Load(String data) {
        var deserializerBuilder = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .WithTypeDiscriminatingNodeDeserializer((o) => {
                var valueMappings = new Dictionary<string, Type>();
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
                    var types = a.GetTypes();
                    foreach (var t in types) {
                        if (t.IsAssignableTo(typeof(ComponentConfig)) && t.IsClass && !t.IsAbstract) {
                            valueMappings.Add(t.Name, t);
                            valueMappings.Add(t.Name[..^"Config".Length], t);
                        }
                    }
                }
                o.AddKeyValueTypeDiscriminator<ComponentConfig>("$type", valueMappings);
            })
            .Build();

        return deserializerBuilder.Deserialize<SceneConfig>(data);
    }
}
