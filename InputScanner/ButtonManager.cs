using InputScanner.Observable;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InputScanner
{
    public class ButtonManager
    {
        private bool[] pressed;
        private ButtonState[] states;

        public long TotalCount { get; private set; }
        public ObservableCollection<ButtonObservable> Buttons { get; private set; }

        public ButtonManager()
        {
            pressed = new bool[256];
            states = new ButtonState[256];

            TotalCount = 0L;
            Buttons = new ObservableCollection<ButtonObservable>();
        }

        public void ResetCount()
        {
            TotalCount = 0;

            foreach (ButtonState buttonState in states)
            {
                if (buttonState == null)
                {
                    continue;
                }
                buttonState.Count = 0;
                buttonState.Percent = null;
            }
            foreach (ButtonObservable buttonObservable in Buttons)
            {
                buttonObservable.Count = 0;
                buttonObservable.Percent = null;
            }
        }

        public void UpdateButtons(List<Settings.Layer> layers)
        {
            states = new ButtonState[256];

            Buttons.Clear();
            foreach (var layer in layers)
            {
                if (layer.KeyCode >= 256)
                {
                    continue;
                }

                ButtonObservable buttonObservable;

                buttonObservable = new ButtonObservable();
                buttonObservable.KeyCode = layer.KeyCode;
                buttonObservable.Label = Generator.GetLabel(layer.KeyCode);

                buttonObservable.Top = layer.Top;
                buttonObservable.Left = layer.Left;
                buttonObservable.Width = layer.Width;
                buttonObservable.Height = layer.Height;

                buttonObservable.Count = 0;
                buttonObservable.Percent = null;

                Buttons.Add(buttonObservable);

                if (states[layer.KeyCode] == null)
                {
                    states[layer.KeyCode] = new ButtonState { KeyCode = layer.KeyCode };
                }
            }
            TotalCount = 0L;
        }

        public bool Contains(KeyboardHook.VKeys vKeys)
        {
            int keyCode = (int)vKeys;
            if (keyCode > 255)
            {
                return false;
            }
            return states[keyCode] != null;
        }

        public bool Pressed(KeyboardHook.VKeys vKeys)
        {
            int keyCode = (int)vKeys;
            if (keyCode > 255)
            {
                return false;
            }
            return pressed[keyCode];
        }

        public bool Press(KeyboardHook.VKeys vKeys)
        {
            int keyCode = (int)vKeys;
            if (keyCode < 256 && pressed[keyCode] == false)
            {
                if (states[keyCode] == null)
                {
                    return false;
                }

                TotalCount++;

                pressed[keyCode] = true;
                states[keyCode].Count++;

                foreach (ButtonState buttonState in states)
                {
                    if (buttonState == null)
                    {
                        continue;
                    }
                    buttonState.Percent = 100.0 * buttonState.Count / TotalCount;
                }

                foreach (ButtonObservable buttonObservable in Buttons)
                {
                    buttonObservable.Count = states[buttonObservable.KeyCode].Count;
                    buttonObservable.Percent = states[buttonObservable.KeyCode].Percent;
                }
                return true;
            }
            return false;
        }

        public bool Release(KeyboardHook.VKeys vKeys)
        {
            int keyCode = (int)vKeys;
            if (keyCode < 256 && pressed[keyCode])
            {
                pressed[keyCode] = false;

                return true;
            }
            return false;
        }

        public ButtonState GetButtonState(KeyboardHook.VKeys vKeys)
        {
            int keyCode = (int)vKeys;
            if (keyCode >= 256)
            {
                return null;
            }
            return states[keyCode];
        }

        public List<ButtonState> GetButtonStates(params KeyboardHook.VKeys[] exclude)
        {
            List<ButtonState> buttonStates = new List<ButtonState>();
            foreach (ButtonState buttonState in states)
            {
                if (buttonState == null || (exclude != null && exclude.Contains((KeyboardHook.VKeys)buttonState.KeyCode)))
                {
                    continue;
                }
                buttonStates.Add(buttonState);
            }
            return buttonStates;
        }
    }
}
