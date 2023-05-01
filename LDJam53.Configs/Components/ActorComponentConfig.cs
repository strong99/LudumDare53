namespace LDJam53.Configs.Components;

public class ActorComponentConfig : ComponentConfig {
    public required String Name { get; set; }
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }

    public ActorComponentConfig DeepClone() {
        return new ActorComponentConfig {
            Name = Name
        };
    }
}

public class QuestTakerComponentConfig : SceneNodeConfig, ComponentConfig {
    ComponentConfig ComponentConfig.DeepClone() {
        return DeepClone();
    }

    public new QuestTakerComponentConfig DeepClone() {
        return new QuestTakerComponentConfig {
            Id = Id,
            Position = Position,
            Scale = Scale,
            Rotation = Rotation,
            Children = Children.Select(p => p.DeepClone()).ToList(),
            Components = Components.Select(p => p.DeepClone()).ToList()
        };
    }
}
