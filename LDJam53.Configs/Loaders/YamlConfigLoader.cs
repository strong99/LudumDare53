using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace LDJam53.Configs.Loaders;

public class YamlConfigLoader {
    public AppConfig LoadFromFile(String file) {
        var fileData = File.ReadAllText(file);
        return Load(fileData);
    }

    public AppConfig Load(String data) {
        var deserializerBuilder = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            //.IgnoreUnmatchedProperties()
            .Build();

        return deserializerBuilder.Deserialize<AppConfig>(data);
    }
}
