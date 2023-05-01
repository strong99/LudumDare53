namespace LDJam53.Configs.Components;

public class SetTextComponentConfig : ComponentConfig
{
    public required String Template { get; set; }
    public required String SourceComponent { get; set; }
    public required String SourceNode { get; set; }
    public required String SourceProperty { get; set; }

    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }
    public SetTextComponentConfig DeepClone() {
        return new SetTextComponentConfig {
            Template = Template,
            SourceComponent = SourceComponent,
            SourceNode = SourceNode,
            SourceProperty = SourceProperty
        };
    }
}
