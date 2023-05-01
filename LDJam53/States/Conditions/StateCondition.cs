using System;

namespace LDJam53.States.Conditions;

public interface StateCondition {
    Boolean IsMet(Single deltaTime, Single totalTime);
    void Remove();
}
