namespace LDJam53.Configs.States.Activities;

public class LoadSceneActivityConfig : StateActivityConfig {
    public required String Scene { get; set; }
}

public class LoadSceneFromQuestActivityConfig : StateActivityConfig {
    public Boolean Destination { get; set; } = true;
}