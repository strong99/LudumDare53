using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDJam53.Configs;

[Flags]
public enum InputEvent {
    None = 0x0,
    Released = 0x1,
    PressStart = 0x2,
    PressActive = 0x4,
    PressEnd = 0x8,
}