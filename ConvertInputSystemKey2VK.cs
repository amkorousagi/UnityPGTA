using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WindowsInput.Native;

public class ConvertInputSystemKey2VK : MonoBehaviour
{
    public static WindowsInput.Native.VirtualKeyCode ConvertK2VK(Key k)
    {
        switch (k)
        {
            case Key.A: return VirtualKeyCode.VK_A;
            case Key.B: return VirtualKeyCode.VK_B;
            case Key.C: return VirtualKeyCode.VK_C;
            case Key.D: return VirtualKeyCode.VK_D;
            case Key.E: return VirtualKeyCode.VK_E;
            case Key.F: return VirtualKeyCode.VK_F;
            case Key.G: return VirtualKeyCode.VK_G;
            case Key.H: return VirtualKeyCode.VK_H;
            case Key.I: return VirtualKeyCode.VK_I;
            case Key.J: return VirtualKeyCode.VK_J;
            case Key.K: return VirtualKeyCode.VK_K;
            case Key.L: return VirtualKeyCode.VK_L;
            case Key.M: return VirtualKeyCode.VK_M;
            case Key.N: return VirtualKeyCode.VK_N;
            case Key.O: return VirtualKeyCode.VK_O;
            case Key.P: return VirtualKeyCode.VK_P;
            case Key.Q: return VirtualKeyCode.VK_Q;
            case Key.R: return VirtualKeyCode.VK_R;
            case Key.S: return VirtualKeyCode.VK_S;
            case Key.T: return VirtualKeyCode.VK_T;
            case Key.U: return VirtualKeyCode.VK_U;
            case Key.V: return VirtualKeyCode.VK_V;
            case Key.W: return VirtualKeyCode.VK_W;
            case Key.X: return VirtualKeyCode.VK_X;
            case Key.Y: return VirtualKeyCode.VK_Y;
            case Key.Z: return VirtualKeyCode.VK_Z;
            case Key.Digit0: return VirtualKeyCode.VK_0;
            case Key.Digit1: return VirtualKeyCode.VK_1;
            case Key.Digit2: return VirtualKeyCode.VK_2;
            case Key.Digit3: return VirtualKeyCode.VK_3;
            case Key.Digit4: return VirtualKeyCode.VK_4;
            case Key.Digit5: return VirtualKeyCode.VK_5;
            case Key.Digit6: return VirtualKeyCode.VK_6;
            case Key.Digit7: return VirtualKeyCode.VK_7;
            case Key.Digit8: return VirtualKeyCode.VK_8;
            case Key.Digit9: return VirtualKeyCode.VK_9;
            case Key.Numpad0: return VirtualKeyCode.NUMPAD0;
            case Key.Numpad1: return VirtualKeyCode.NUMPAD1;
            case Key.Numpad2: return VirtualKeyCode.NUMPAD2;
            case Key.Numpad3: return VirtualKeyCode.NUMPAD3;
            case Key.Numpad4: return VirtualKeyCode.NUMPAD4;
            case Key.Numpad5: return VirtualKeyCode.NUMPAD5;
            case Key.Numpad6: return VirtualKeyCode.NUMPAD6;
            case Key.Numpad7: return VirtualKeyCode.NUMPAD7;
            case Key.Numpad8: return VirtualKeyCode.NUMPAD8;
            case Key.Numpad9: return VirtualKeyCode.NUMPAD9;
            case Key.F1: return VirtualKeyCode.F1;
            case Key.F2: return VirtualKeyCode.F2;
            case Key.F3: return VirtualKeyCode.F3;
            case Key.F4: return VirtualKeyCode.F4;
            case Key.F5: return VirtualKeyCode.F5;
            case Key.F6: return VirtualKeyCode.F6;
            case Key.F7: return VirtualKeyCode.F7;
            case Key.F8: return VirtualKeyCode.F8;
            case Key.F9: return VirtualKeyCode.F9;
            case Key.F10: return VirtualKeyCode.F10;
            case Key.F11: return VirtualKeyCode.F11;
            case Key.F12: return VirtualKeyCode.F12;
            case Key.Space: return VirtualKeyCode.SPACE;
            case Key.Escape: return VirtualKeyCode.ESCAPE;
            case Key.Tab: return VirtualKeyCode.TAB;
            case Key.CapsLock: return VirtualKeyCode.CAPITAL;
            case Key.LeftShift: return VirtualKeyCode.LSHIFT;
            case Key.RightShift: return VirtualKeyCode.RSHIFT;
            case Key.LeftCtrl: return VirtualKeyCode.LCONTROL;
            case Key.RightCtrl: return VirtualKeyCode.RCONTROL;
            case Key.LeftAlt: return VirtualKeyCode.LMENU;
            case Key.RightAlt: return VirtualKeyCode.RMENU;
            case Key.LeftArrow: return VirtualKeyCode.LEFT;
            case Key.RightArrow: return VirtualKeyCode.RIGHT;
            case Key.UpArrow: return VirtualKeyCode.UP;
            case Key.DownArrow: return VirtualKeyCode.DOWN;
            case Key.Backspace: return VirtualKeyCode.BACK;
            case Key.Delete: return VirtualKeyCode.DELETE;
            case Key.Insert: return VirtualKeyCode.INSERT;
            case Key.Home: return VirtualKeyCode.HOME;
            case Key.End: return VirtualKeyCode.END;
            case Key.PageUp: return VirtualKeyCode.PRIOR;
            case Key.PageDown: return VirtualKeyCode.NEXT;
            case Key.NumpadDivide: return VirtualKeyCode.DIVIDE;
            case Key.NumpadMultiply: return VirtualKeyCode.MULTIPLY;
            case Key.NumpadMinus: return VirtualKeyCode.SUBTRACT;
            case Key.NumpadPlus: return VirtualKeyCode.ADD;
            case Key.NumpadEnter: return VirtualKeyCode.RETURN;
            case Key.NumpadPeriod: return VirtualKeyCode.DECIMAL;
            case Key.NumLock: return VirtualKeyCode.NUMLOCK;
            case Key.ScrollLock: return VirtualKeyCode.SCROLL;
            case Key.PrintScreen: return VirtualKeyCode.SNAPSHOT;
            case Key.Pause: return VirtualKeyCode.PAUSE;
            case Key.LeftBracket: return VirtualKeyCode.OEM_4;
            case Key.RightBracket: return VirtualKeyCode.OEM_6;
            case Key.Backquote: return VirtualKeyCode.OEM_3;
            case Key.Semicolon: return VirtualKeyCode.OEM_1;
            case Key.Quote: return VirtualKeyCode.OEM_7;
            case Key.Comma: return VirtualKeyCode.OEM_COMMA;
            case Key.Period: return VirtualKeyCode.OEM_PERIOD;
            case Key.Slash: return VirtualKeyCode.OEM_2;
            case Key.Backslash: return VirtualKeyCode.OEM_5;
            // Add more cases for other key codes
            default: return VirtualKeyCode.SPACE;
        }
    }
}
