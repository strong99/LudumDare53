using System;

namespace LDJam53.Scenes;

public interface Component {
    void Remove();
}

public interface UpdatableComponent : Component {
    void Update(Single deltaTime, Single totalTime);
}

public interface DrawableComponent : Component {
    void Draw(Single deltaTime, Single totalTime);
}
