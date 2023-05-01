using System;

namespace LDJam53.States.Activities;

public enum StateActivityStatus {
    Pending,
    Active,
    Finished
}

public interface StateActivity {
    StateActivityStatus Status { get; }
    StateActivityStatus Update(Single deltaTime, Single totalTime);
    void Remove();
}
