using InputScanner.JsonObject;
using InputScanner.Observable;
using InputScanner.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InputScanner
{
    public partial class MainWindow : Window
    {
        private JsonSerializerSettings jsonSettings;

        private List<WebSocketSession> sessions;
        private KeyboardHook keyboardHook;

        private LayoutModeObject layoutModeObject;
        private ColorSetObject colorSetObject;
        private ContainerObject containerObject;

        private Settings settings;
        private bool[] pressedKeys;

        private WebSocketServer wsServer;
        private ButtonManager buttonManager;
        private GamepadManager gamepadManager;

        public MainWindow()
        {
            InitializeComponent();

            layoutModeObject = new LayoutModeObject
            {
                Mode = "key",
            };
            colorSetObject = new ColorSetObject
            {
                Text = "white",
                Border = "white",
                Shadow = "black",
                BackgroundReleased = "#0000001F",
                BackgroundPressed = "#FF3377CC",
            };
            containerObject = new ContainerObject
            {
                Top = 0,
                Left = 0,
                Width = 0,
                Height = 0,
            };

            pressedKeys = new bool[256];
            jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            buttonManager = new ButtonManager();
            preview.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Buttons") { Source = buttonManager });

            gamepads.Items.Clear();
            gamepadManager = new GamepadManager();
            gamepadManager.PropertyChanged += OnGamepadManagerPropertyChanged;
            foreach (DeviceInstance deviceInstance in gamepadManager.GetDevices())
            {
                gamepads.Items.Add(new ComboBoxItem() { Content = deviceInstance.ProductName, Tag = deviceInstance.InstanceGuid });
            }

            sessions = new List<WebSocketSession>();

            keyboardHook = new KeyboardHook();
            keyboardHook.Install();
            keyboardHook.KeyUp += OnKeyUp;
            keyboardHook.KeyDown += OnKeyDown;

            for (int i = 0; i < 255; i++)
            {
                if (Enum.IsDefined(typeof(KeyboardHook.VKeys), i) == false)
                {
                    continue;
                }
                string label = Generator.GetLabel(i, true);
                if (label == null)
                {
                    continue;
                }

                ComboBoxItem item = new ComboBoxItem();

                item.Content = label;
                item.Tag = i;

                buttons.Items.Add(item);
            }

            SyncFromSettings();

            wsServer = new WebSocketServer(13300);
            wsServer.Connected += NewSessionConnected;
            wsServer.Closed += SessionClosed;
            wsServer.Start();
        }

        private void OnGamepadManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (layoutModeObject.Mode != "button")
            {
                layoutModeObject.Mode = "button";

                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, layoutModeObject);
                }
            }

            ButtonEventObject baseObject = null;
            if (e.PropertyName == GamepadManager.ButtonL2Property.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "L2",
                    Value = gamepadManager.ButtonL2,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonL1Property.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "L1",
                    Value = gamepadManager.ButtonL1,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonR2Property.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "R2",
                    Value = gamepadManager.ButtonR2,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonR1Property.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "R1",
                    Value = gamepadManager.ButtonR1,
                };
            }

            if (e.PropertyName == GamepadManager.AxisLXProperty.Name || e.PropertyName == GamepadManager.AxisLYProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "L3",
                    Value = gamepadManager.AxisLX != 0.0 || gamepadManager.AxisLY != 0.0,
                };
            }
            if (e.PropertyName == GamepadManager.AxisRXProperty.Name || e.PropertyName == GamepadManager.AxisRYProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "R3",
                    Value = gamepadManager.AxisRX != 0.0 || gamepadManager.AxisRY != 0.0,
                };
            }

            if (e.PropertyName == GamepadManager.ButtonUpProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "U",
                    Value = gamepadManager.ButtonUp,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonLeftProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "L",
                    Value = gamepadManager.ButtonLeft,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonDownProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "D",
                    Value = gamepadManager.ButtonDown,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonRightProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "R",
                    Value = gamepadManager.ButtonRight,
                };
            }

            if (e.PropertyName == GamepadManager.ButtonTriangleProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "T",
                    Value = gamepadManager.ButtonTriangle,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonSquareProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "S",
                    Value = gamepadManager.ButtonSquare,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonCrossProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "X",
                    Value = gamepadManager.ButtonCross,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonCircleProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "O",
                    Value = gamepadManager.ButtonCircle,
                };
            }

            if (e.PropertyName == GamepadManager.ButtonShareProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "SHARE",
                    Value = gamepadManager.ButtonShare,
                };
            }
            if (e.PropertyName == GamepadManager.ButtonOptionsProperty.Name)
            {
                baseObject = new ButtonEventObject
                {
                    ButtonName = "OPTIONS",
                    Value = gamepadManager.ButtonOptions,
                };
            }


            if (baseObject != null)
            {
                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, baseObject);
                }
                message.Text = $"Property: {e.PropertyName}, Value: {baseObject.Value}";
            }
        }

        private void OnKeyDown(KeyboardHook.VKeys key)
        {
            if (layoutModeObject.Mode != "key")
            {
                layoutModeObject.Mode = "key";

                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, layoutModeObject);
                }
            }

            if ((int)key < 256)
            {
                pressedKeys[(int)key] = true;
            }

            message.Text = key.ToString();

            if (buttonManager.Press(key))
            {
                ButtonState buttonState = buttonManager.GetButtonState(key);

                KeyEventObject keyEventObject = new KeyEventObject
                {
                    KeyCode = buttonState.KeyCode,
                    Pressed = true,
                    Count = buttonState.Count,
                    Percent = buttonState.Percent,
                };

                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, keyEventObject);
                }

                List<ButtonState> buttonStates = buttonManager.GetButtonStates(key);
                KeyCountListObject keyCountListObject = new KeyCountListObject();
                for (int i = 0; i < buttonStates.Count; i++)
                {
                    buttonState = buttonStates[i];

                    keyCountListObject.KeyCounts.Add(new KeyCountObject
                    {
                        KeyCode = buttonState.KeyCode,
                        Count = buttonState.Count,
                        Percent = buttonState.Percent,
                    });
                }
                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, keyCountListObject);
                }
            }

            if (pressedKeys[(int)KeyboardHook.VKeys.LCONTROL] && pressedKeys[(int)KeyboardHook.VKeys.BACK])
            {
                CountReset();
            }
        }

        private void OnKeyUp(KeyboardHook.VKeys key)
        {
            if (layoutModeObject.Mode != "key")
            {
                layoutModeObject.Mode = "key";

                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, layoutModeObject);
                }
            }

            if ((int)key < 256)
            {
                pressedKeys[(int)key] = false;
            }

            if (buttonManager.Release(key))
            {
                ButtonState buttonState = buttonManager.GetButtonState(key);
                KeyEventObject keyEventObject = new KeyEventObject
                {
                    KeyCode = buttonState.KeyCode,
                    Pressed = false,
                    Count = buttonState.Count,
                    Percent = buttonState.Percent,
                };

                foreach (WebSocketSession session in sessions)
                {
                    SendToSession(session, keyEventObject);
                }
            }
        }

        private void OnAlignChanged(object sender, RoutedEventArgs e)
        {
            if (IsLoaded == false)
            {
                return;
            }

            if (alignLeft.IsChecked == true)
            {
                containerObject.Left = 10;
            }
            if (alignCenter.IsChecked == true)
            {
                containerObject.Left = (1920 - containerObject.Width) / 2;
            }
            if (alignRight.IsChecked == true)
            {
                containerObject.Left = 1920 - containerObject.Width - 10;
            }

            foreach (WebSocketSession session in sessions)
            {
                SendToSession(session, containerObject);
            }
        }

        private void OnLayerSetChanged(object sender, SelectionChangedEventArgs e)
        {
            if (layerSet.SelectedIndex == -1)
            {
                buttonManager.UpdateButtons(new List<Settings.Layer>());
            }
            else
            {
                buttonManager.UpdateButtons(settings.LayerSets[layerSet.SelectedIndex].Layers);
            }
            UpdateButtonLayouts();
        }

        private void OnGamepadChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)gamepads.SelectedItem;

            gamepadManager.Select((Guid)item.Tag);
        }

        private void OnSaveSetting(object sender, RoutedEventArgs e)
        {
            foreach (Settings.LayerSet layerSet in settings.LayerSets)
            {
                foreach (Settings.Layer layer in layerSet.Layers)
                {
                    layer.KeyCode = layer.KeyCode;
                }
            }

            Settings.Save(settings);
        }


        private void OnResetSetting(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.LayerSets.Add(new Settings.LayerSet() { Name = "iruis" });
            foreach (KeyLayoutObject buttonLayoutObject in Generator.GenerateIruis())
            {
                int idx = settings.LayerSets.Count - 1;
                settings.LayerSets[idx].Layers.Add(new Settings.Layer()
                {
                    Kind = buttonLayoutObject.Kind,
                    KeyCode = buttonLayoutObject.KeyCode,
                    Top = buttonLayoutObject.Top,
                    Left = buttonLayoutObject.Left,
                    Width = buttonLayoutObject.Width,
                    Height = buttonLayoutObject.Height,
                });
            }
            settings.LayerSets.Add(new Settings.LayerSet() { Name = "BMS" });
            foreach (KeyLayoutObject buttonLayoutObject in Generator.GenerateBMS())
            {
                int idx = settings.LayerSets.Count - 1;
                settings.LayerSets[idx].Layers.Add(new Settings.Layer()
                {
                    Kind = buttonLayoutObject.Kind,
                    KeyCode = buttonLayoutObject.KeyCode,
                    Top = buttonLayoutObject.Top,
                    Left = buttonLayoutObject.Left,
                    Width = buttonLayoutObject.Width,
                    Height = buttonLayoutObject.Height,
                });
            }
            settings.LayerSets.Add(new Settings.LayerSet() { Name = "DJMAX 4,5,6" });
            foreach (KeyLayoutObject buttonLayoutObject in Generator.GenerateDjmax456())
            {
                int idx = settings.LayerSets.Count - 1;
                settings.LayerSets[idx].Layers.Add(new Settings.Layer()
                {
                    Kind = buttonLayoutObject.Kind,
                    KeyCode = buttonLayoutObject.KeyCode,
                    Top = buttonLayoutObject.Top,
                    Left = buttonLayoutObject.Left,
                    Width = buttonLayoutObject.Width,
                    Height = buttonLayoutObject.Height,
                });
            }
            settings.LayerSets.Add(new Settings.LayerSet() { Name = "DJMAX 8" });
            foreach (KeyLayoutObject buttonLayoutObject in Generator.GenerateDjmax8())
            {
                int idx = settings.LayerSets.Count - 1;
                settings.LayerSets[idx].Layers.Add(new Settings.Layer()
                {
                    Kind = buttonLayoutObject.Kind,
                    KeyCode = buttonLayoutObject.KeyCode,
                    Top = buttonLayoutObject.Top,
                    Left = buttonLayoutObject.Left,
                    Width = buttonLayoutObject.Width,
                    Height = buttonLayoutObject.Height,
                });
            }
            settings.LayerSets.Add(new Settings.LayerSet() { Name = "DJMAX ALL" });
            foreach (KeyLayoutObject buttonLayoutObject in Generator.GenerateDjmaxAll())
            {
                int idx = settings.LayerSets.Count - 1;
                settings.LayerSets[idx].Layers.Add(new Settings.Layer()
                {
                    Kind = buttonLayoutObject.Kind,
                    KeyCode = buttonLayoutObject.KeyCode,
                    Top = buttonLayoutObject.Top,
                    Left = buttonLayoutObject.Left,
                    Width = buttonLayoutObject.Width,
                    Height = buttonLayoutObject.Height,
                });
            }

            Settings.Save(settings);
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            wsServer.Stop();

            keyboardHook.Uninstall();
            gamepadManager.Select(Guid.Empty);
        }

        private void SendToSession(WebSocketSession session, BaseObject baseObject)
        {
            try
            {
                wsServer.Send(session, JsonConvert.SerializeObject(baseObject, jsonSettings));
            }
            catch { }
        }

        private void SyncFromSettings()
        {
            settings = Settings.Load();

            layerSet.SelectedIndex = -1;
            layerSet.Items.Clear();
            foreach (Settings.LayerSet set in settings.LayerSets)
            {
                layerSet.Items.Add(new ComboBoxItem() { Content = set.Name });
            }

            if (layerSet.Items.Count > 0)
            {
                layerSet.SelectedIndex = 0;
            }
        }

        private void UpdatePreview()
        {
            int maxWidth = 0;
            int maxHeight = 0;

            foreach (ButtonObservable buttonObservable in buttonManager.Buttons)
            {
                maxWidth = Math.Max(buttonObservable.Left + buttonObservable.Width, maxWidth);
                maxHeight = Math.Max(buttonObservable.Top + buttonObservable.Height, maxHeight);
            }
            preview.Width = maxWidth;
            preview.Height = maxHeight;
        }

        private void UpdateButtonLayouts()
        {
            UpdatePreview();

            containerObject.Top = 10;
            containerObject.Width = (int)preview.Width;
            containerObject.Height = (int)preview.Height;

            if (alignLeft.IsChecked == true)
            {
                containerObject.Left = 10;
            }
            if (alignCenter.IsChecked == true)
            {
                containerObject.Left = (1920 - containerObject.Width) / 2;
            }
            if (alignRight.IsChecked == true)
            {
                containerObject.Left = 1920 - containerObject.Width - 10;
            }

            foreach (WebSocketSession session in sessions)
            {
                try
                {
                    SendLayout(session);
                }
                catch { }
            }
        }

        private void SendLayout(WebSocketSession session)
        {
            SendToSession(session, new ResetLayoutObject());
            SendToSession(session, containerObject);
            SendToSession(session, colorSetObject);
            SendToSession(session, layoutModeObject);

            KeyLayoutListObject buttonLayoutListObject = new KeyLayoutListObject();
            foreach (ButtonObservable buttonObservable in buttonManager.Buttons)
            {
                buttonLayoutListObject.KeyLayouts.Add(new KeyLayoutObject
                {
                    KeyCode = buttonObservable.KeyCode,
                    Label = Generator.GetLabel(buttonObservable.KeyCode),
                    Top = buttonObservable.Top + 25, // TODO
                    Left = buttonObservable.Left,
                    Width = buttonObservable.Width,
                    Height = buttonObservable.Height,
                });
            }
            SendToSession(session, buttonLayoutListObject);

            KeyEventListObject keyEventListObject = new KeyEventListObject();
            foreach (ButtonState buttonState in buttonManager.GetButtonStates())
            {
                keyEventListObject.KeyEvents.Add(new KeyEventObject
                {
                    KeyCode = buttonState.KeyCode,
                    Pressed = buttonState.Pressed,
                    Count = buttonState.Count,
                    Percent = buttonState.Percent,
                });
            }
            SendToSession(session, keyEventListObject);

            foreach (ButtonLayoutObject buttonLayoutObject in Generator.GenerateButtons())
            {
                buttonLayoutObject.Left += 125; // TODO
                SendToSession(session, buttonLayoutObject);
            }
        }

        private void SessionClosed(WebSocketSession session)
        {
            sessions.Remove(session);
        }

        private void NewDataReceived(WebSocketSession session, byte[] value)
        {
        }

        private void NewMessageReceived(WebSocketSession session, string value)
        {
            var update = new Action(() =>
            {
                message.Text = value;
            });

            Dispatcher.BeginInvoke(update);
        }

        private void NewSessionConnected(WebSocketSession session)
        {
            sessions.Add(session);

            Dispatcher.Invoke(() =>
            {
                SendLayout(session);
            });
        }

        private void OnCountReset(object sender, RoutedEventArgs e)
        {
            CountReset();
        }

        private void CountReset()
        {
            buttonManager.ResetCount();

            KeyCountListObject keyCountListObject = new KeyCountListObject();

            foreach (ButtonState buttonState in buttonManager.GetButtonStates())
            {
                KeyCountObject keyCountObject = new KeyCountObject
                {
                    KeyCode = buttonState.KeyCode,
                    Count = buttonState.Count,
                    Percent = buttonState.Percent,
                };

                keyCountListObject.KeyCounts.Add(keyCountObject);
            }

            foreach (WebSocketSession session in sessions)
            {
                SendToSession(session, keyCountListObject);
            }
        }
    }
}
