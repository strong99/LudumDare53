using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using LDJam53.Configs.States;
using LDJam53.Configs.States.Activities;
using LDJam53.Configs.States.Conditions;

namespace LDJam53.Configs.Loaders;

public class YamlStatesLoader {
    public StateSetsConfig LoadFromFile(String file) {
        var fileData = File.ReadAllText(file);
        return Load(fileData);
    }

    public StateSetsConfig Load(String data) {
        var deserializerBuilder = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .WithTypeDiscriminatingNodeDeserializer((o) => {
                var conditionValueMappings = new Dictionary<string, Type>();
                var activityValueMappings = new Dictionary<string, Type>();
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
                    var types = a.GetTypes();
                    foreach (var t in types) {
                        if (t.IsAssignableTo(typeof(StateConditionConfig)) && t.IsClass && !t.IsAbstract) {
                            conditionValueMappings.Add(t.Name, t);
                            if (t.Name.EndsWith("Config")) conditionValueMappings.Add(t.Name[..^"Config".Length], t);
                            if (t.Name.EndsWith("ConditionConfig")) conditionValueMappings.Add(t.Name[..^"ConditionConfig".Length], t);
                            if (t.Name.EndsWith("StateConditionConfig")) conditionValueMappings.Add(t.Name[..^"StateConditionConfig".Length], t);
                        }
                        else if (t.IsAssignableTo(typeof(StateActivityConfig)) && t.IsClass && !t.IsAbstract) {
                            activityValueMappings.Add(t.Name, t);
                            if (t.Name.EndsWith("Config")) activityValueMappings.Add(t.Name[..^"Config".Length], t);
                            if (t.Name.EndsWith("ActivityConfig")) activityValueMappings.Add(t.Name[..^"ActivityConfig".Length], t);
                            if (t.Name.EndsWith("StateActivityConfig")) activityValueMappings.Add(t.Name[..^"StateActivityConfig".Length], t);
                        }
                    }
                }
                o.AddKeyValueTypeDiscriminator<StateConditionConfig>("$type", conditionValueMappings);
                o.AddKeyValueTypeDiscriminator<StateActivityConfig>("$type", activityValueMappings);
            })
            .Build();

        return deserializerBuilder.Deserialize<StateSetsConfig>(data);
    }
}
