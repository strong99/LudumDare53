using System;
using System.Numerics;

namespace LDJam53.Graphics;

public interface Font {
    String Name { get; }
    Vector2 GetSize(String text);
}
