using InputScanner.JsonObject;
using System.Collections.Generic;

namespace InputScanner
{
    public static class Generator
    {
        private static bool ContainsRange(long keyCode, KeyboardHook.VKeys begin, KeyboardHook.VKeys end)
        {
            return keyCode >= (long)begin && keyCode <= (long)end;
        }

        public static string GetLabel(long keyCode, bool allowNull = false)
        {
            if (ContainsRange(keyCode, KeyboardHook.VKeys.KEY_A, KeyboardHook.VKeys.KEY_Z))
            {
                return ((char)('A' + keyCode - (long)KeyboardHook.VKeys.KEY_A)).ToString();
            }
            if (ContainsRange(keyCode, KeyboardHook.VKeys.NUMPAD0, KeyboardHook.VKeys.NUMPAD9))
            {
                long num = (keyCode - (long)KeyboardHook.VKeys.NUMPAD0);

                return "num" + num;
            }
            if (ContainsRange(keyCode, KeyboardHook.VKeys.KEY_0, KeyboardHook.VKeys.KEY_9))
            {
                return (keyCode - (long)KeyboardHook.VKeys.KEY_0).ToString();
            }
            if (ContainsRange(keyCode, KeyboardHook.VKeys.F1, KeyboardHook.VKeys.F24))
            {
                long num = (keyCode - (long)KeyboardHook.VKeys.F1) + 1;

                return "F" + num;
            }

            switch ((KeyboardHook.VKeys)keyCode)
            {
                case KeyboardHook.VKeys.ESCAPE:
                    return "ESC";
                case KeyboardHook.VKeys.ADD:
                    return "num+";
                case KeyboardHook.VKeys.OEM_1:
                    return ";";
                case KeyboardHook.VKeys.CAPITAL:
                    return "CapsLock";
                case KeyboardHook.VKeys.SPACE:
                    return "SPACE";
                case KeyboardHook.VKeys.LSHIFT:
                    return "L-SHIFT";
                case KeyboardHook.VKeys.RSHIFT:
                    return "R-SHIFT";
            }

            if (allowNull)
            {
                return null;
            }
            return "UNKNOWN";
        }

        public static List<KeyLayoutObject> GenerateIruis()
        {
            List<KeyLayoutObject> buttons = new List<KeyLayoutObject>();

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.CAPITAL, Label = "CapsLock", Top = 0, Left = 0, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_Q, Label = "Q", Top = 0, Left = 90, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_W, Label = "W", Top = 0, Left = 160, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_E, Label = "E", Top = 0, Left = 230, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD7, Label = "num7", Top = 0, Left = 300, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD8, Label = "num8", Top = 0, Left = 370, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD9, Label = "num9", Top = 0, Left = 440, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.ADD, Label = "num+", Top = 0, Left = 510, Width = 80, Height = 52 });

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.SPACE, Label = "SPACE", Top = 62, Left = 150, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD0, Label = "num0", Top = 62, Left = 360, Width = 80, Height = 52 });

            return buttons;
        }

        public static List<KeyLayoutObject> GenerateBMS()
        {
            List<KeyLayoutObject> buttons = new List<KeyLayoutObject>();

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.LSHIFT, Label = "L-SHIFT", Top = 10, Left = 10, Width = 90, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_S, Label = "S", Top = 10, Left = 110, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_D, Label = "D", Top = 10, Left = 180, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_F, Label = "F", Top = 10, Left = 250, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.SPACE, Label = "SPACE", Top = 10, Left = 320, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_J, Label = "J", Top = 10, Left = 390, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_K, Label = "K", Top = 10, Left = 460, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_L, Label = "L", Top = 10, Left = 530, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.OEM_1, Label = ";", Top = 10, Left = 600, Width = 60, Height = 52 });

            return buttons;
        }

        public static List<KeyLayoutObject> GenerateDjmax456()
        {
            List<KeyLayoutObject> buttons = new List<KeyLayoutObject>();

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.LSHIFT, Label = "L-SHIFT", Top = 0, Left = 0, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_S, Label = "S", Top = 0, Left = 90, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_D, Label = "D", Top = 0, Left = 160, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_F, Label = "F", Top = 0, Left = 230, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_J, Label = "J", Top = 0, Left = 300, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_K, Label = "K", Top = 0, Left = 370, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_L, Label = "L", Top = 0, Left = 440, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.OEM_1, Label = ";", Top = 0, Left = 510, Width = 80, Height = 52 });

            return buttons;
        }

        public static List<KeyLayoutObject> GenerateDjmax8()
        {
            List<KeyLayoutObject> buttons = new List<KeyLayoutObject>();

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.CAPITAL, Label = "CapsLock", Top = 0, Left = 0, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_W, Label = "W", Top = 0, Left = 90, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_E, Label = "E", Top = 0, Left = 160, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_R, Label = "R", Top = 0, Left = 230, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD7, Label = "num7", Top = 0, Left = 300, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD8, Label = "num8", Top = 0, Left = 370, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD9, Label = "num9", Top = 0, Left = 440, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.ADD, Label = "num+", Top = 0, Left = 510, Width = 80, Height = 52 });

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.SPACE, Label = "SPACE", Top = 62, Left = 150, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD0, Label = "num0", Top = 62, Left = 360, Width = 80, Height = 52 });

            return buttons;
        }

        public static List<KeyLayoutObject> GenerateDjmaxAll()
        {
            List<KeyLayoutObject> buttons = new List<KeyLayoutObject>();

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.LSHIFT, Label = "L-SHIFT", Top = 0, Left = 0, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_S, Label = "S", Top = 0, Left = 90, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_D, Label = "D", Top = 0, Left = 160, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_F, Label = "F", Top = 0, Left = 230, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_J, Label = "J", Top = 0, Left = 300, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_K, Label = "K", Top = 0, Left = 370, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_L, Label = "L", Top = 0, Left = 440, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.OEM_1, Label = ";", Top = 0, Left = 510, Width = 80, Height = 52 });

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.CAPITAL, Label = "CapsLock", Top = 0, Left = 610, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_W, Label = "W", Top = 0, Left = 700, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_E, Label = "E", Top = 0, Left = 770, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.KEY_R, Label = "R", Top = 0, Left = 840, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD7, Label = "num7", Top = 0, Left = 910, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD8, Label = "num8", Top = 0, Left = 980, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD9, Label = "num9", Top = 0, Left = 1050, Width = 60, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.ADD, Label = "num+", Top = 0, Left = 1120, Width = 80, Height = 52 });

            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.SPACE, Label = "SPACE", Top = 62, Left = 760, Width = 80, Height = 52 });
            buttons.Add(new KeyLayoutObject() { KeyCode = (long)KeyboardHook.VKeys.NUMPAD0, Label = "num0", Top = 62, Left = 970, Width = 80, Height = 52 });

            return buttons;
        }

        public static List<ButtonLayoutObject> GenerateButtons()
        {
            List<ButtonLayoutObject> buttons = new List<ButtonLayoutObject>();

            buttons.Add(new ButtonLayoutObject { Top = 0, Left = 70, Width = 60, Height = 15, Shape = "rectangle", Label = "L2", ButtonName = "L2" });
            buttons.Add(new ButtonLayoutObject { Top = 20, Left = 70, Width = 60, Height = 15, Shape = "rectangle", Label = "L1", ButtonName = "L1" });
            buttons.Add(new ButtonLayoutObject { Top = 0, Left = 210, Width = 60, Height = 15, Shape = "rectangle", Label = "R2", ButtonName = "R2" });
            buttons.Add(new ButtonLayoutObject { Top = 20, Left = 210, Width = 60, Height = 15, Shape = "rectangle", Label = "R1", ButtonName = "R1" });

            buttons.Add(new ButtonLayoutObject { Top = 40, Left = 100, Width = 15, Height = 25, Shape = "rectangle", Label = "S", ButtonName = "SHARE" });
            buttons.Add(new ButtonLayoutObject { Top = 40, Left = 220, Width = 15, Height = 25, Shape = "rectangle", Label = "O", ButtonName = "OPTIONS" });

            buttons.Add(new ButtonLayoutObject { Top = 30, Left = 30, Width = 30, Height = 30, Shape = "circle", Label = "↑", ButtonName = "U" });
            buttons.Add(new ButtonLayoutObject { Top = 60, Left = 0, Width = 30, Height = 30, Shape = "circle", Label = "←", ButtonName = "L" });
            buttons.Add(new ButtonLayoutObject { Top = 90, Left = 30, Width = 30, Height = 30, Shape = "circle", Label = "↓", ButtonName = "D" });
            buttons.Add(new ButtonLayoutObject { Top = 60, Left = 60, Width = 30, Height = 30, Shape = "circle", Label = "→", ButtonName = "R" });

            buttons.Add(new ButtonLayoutObject { Top = 30, Left = 280, Width = 30, Height = 30, Shape = "circle", Label = "△", ButtonName = "T" });
            buttons.Add(new ButtonLayoutObject { Top = 60, Left = 250, Width = 30, Height = 30, Shape = "circle", Label = "□", ButtonName = "S" });
            buttons.Add(new ButtonLayoutObject { Top = 90, Left = 280, Width = 30, Height = 30, Shape = "circle", Label = "Ⅹ", ButtonName = "X" });
            buttons.Add(new ButtonLayoutObject { Top = 60, Left = 310, Width = 30, Height = 30, Shape = "circle", Label = "○", ButtonName = "O" });

            buttons.Add(new ButtonLayoutObject { Top = 100, Left = 100, Width = 40, Height = 40, Shape = "circle", Label = "L3", ButtonName = "L3" });
            buttons.Add(new ButtonLayoutObject { Top = 100, Left = 200, Width = 40, Height = 40, Shape = "circle", Label = "R3", ButtonName = "R3" });

            return buttons;
        }
    }
}
