namespace LDJam53.Configs.Components;

public class SetTextureComponentConfig : ComponentConfig
{
    public String? Template { get; set; }
    public String? Texture { get; set; }
    public String? SourceComponent { get; set; }
    public String? SourceNode { get; set; }
    public String? SourceProperty { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public SetTextureComponentConfig DeepClone() {
        return new SetTextureComponentConfig {
            Template = Template,
            Texture = Texture,
            SourceComponent = SourceComponent,
            SourceNode = SourceNode,
            SourceProperty = SourceProperty
        };
    }
}
