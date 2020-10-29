using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace InputScanner
{
    public class GamepadManager : DependencyObject, INotifyPropertyChanged
    {
        #region DependencyProperties & PropertyChanged event handler

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double AxisLX
        {
            get { return (double)GetValue(AxisLXProperty); }
            set
            {
                SetValue(AxisLXProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AxisLXProperty =
            DependencyProperty.Register("AxisLX", typeof(double), typeof(GamepadManager), new PropertyMetadata(0.0));

        public double AxisLY
        {
            get { return (double)GetValue(AxisLYProperty); }
            set
            {
                SetValue(AxisLYProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AxisLYProperty =
            DependencyProperty.Register("AxisLY", typeof(double), typeof(GamepadManager), new PropertyMetadata(0.0));

        public double AxisRX
        {
            get { return (double)GetValue(AxisRXProperty); }
            set
            {
                SetValue(AxisRXProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AxisRXProperty =
            DependencyProperty.Register("AxisRX", typeof(double), typeof(GamepadManager), new PropertyMetadata(0.0));

        public double AxisRY
        {
            get { return (double)GetValue(AxisRYProperty); }
            set
            {
                SetValue(AxisRYProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AxisRYProperty =
            DependencyProperty.Register("AxisRY", typeof(double), typeof(GamepadManager), new PropertyMetadata(0.0));

        public bool ButtonL1
        {
            get { return (bool)GetValue(ButtonL1Property); }
            set
            {
                SetValue(ButtonL1Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonL1Property =
            DependencyProperty.Register("ButtonL1", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));


        public bool ButtonL2
        {
            get { return (bool)GetValue(ButtonL2Property); }
            set
            {
                SetValue(ButtonL2Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonL2Property =
            DependencyProperty.Register("ButtonL2", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonL3
        {
            get { return (bool)GetValue(ButtonL3Property); }
            set
            {
                SetValue(ButtonL3Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonL3Property =
            DependencyProperty.Register("ButtonL3", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonR1
        {
            get { return (bool)GetValue(ButtonR1Property); }
            set
            {
                SetValue(ButtonR1Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonR1Property =
            DependencyProperty.Register("ButtonR1", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonR2
        {
            get { return (bool)GetValue(ButtonR2Property); }
            set
            {
                SetValue(ButtonR2Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonR2Property =
            DependencyProperty.Register("ButtonR2", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonR3
        {
            get { return (bool)GetValue(ButtonR3Property); }
            set
            {
                SetValue(ButtonR3Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonR3Property =
            DependencyProperty.Register("ButtonR3", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public double AnalogL2
        {
            get { return (double)GetValue(AnalogL2Property); }
            set
            {
                SetValue(AnalogL2Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AnalogL2Property =
            DependencyProperty.Register("AnalogL2", typeof(double), typeof(GamepadManager), new PropertyMetadata(-1.0));

        public double AnalogR2
        {
            get { return (double)GetValue(AnalogR2Property); }
            set
            {
                SetValue(AnalogR2Property, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty AnalogR2Property =
            DependencyProperty.Register("AnalogR2", typeof(double), typeof(GamepadManager), new PropertyMetadata(-1.0));

        public bool ButtonUp
        {
            get { return (bool)GetValue(ButtonUpProperty); }
            set
            {
                SetValue(ButtonUpProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonUpProperty =
            DependencyProperty.Register("ButtonUp", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonDown
        {
            get { return (bool)GetValue(ButtonDownProperty); }
            set
            {
                SetValue(ButtonDownProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonDownProperty =
            DependencyProperty.Register("ButtonDown", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonLeft
        {
            get { return (bool)GetValue(ButtonLeftProperty); }
            set
            {
                SetValue(ButtonLeftProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonLeftProperty =
            DependencyProperty.Register("ButtonLeft", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonRight
        {
            get { return (bool)GetValue(ButtonRightProperty); }
            set
            {
                SetValue(ButtonRightProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonRightProperty =
            DependencyProperty.Register("ButtonRight", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonTriangle
        {
            get { return (bool)GetValue(ButtonTriangleProperty); }
            set
            {
                SetValue(ButtonTriangleProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonTriangleProperty =
            DependencyProperty.Register("ButtonTriangle", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonCircle
        {
            get { return (bool)GetValue(ButtonCircleProperty); }
            set
            {
                SetValue(ButtonCircleProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonCircleProperty =
            DependencyProperty.Register("ButtonCircle", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonCross
        {
            get { return (bool)GetValue(ButtonCrossProperty); }
            set
            {
                SetValue(ButtonCrossProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonCrossProperty =
            DependencyProperty.Register("ButtonCross", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonSquare
        {
            get { return (bool)GetValue(ButtonSquareProperty); }
            set
            {
                SetValue(ButtonSquareProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonSquareProperty =
            DependencyProperty.Register("ButtonSquare", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonShare
        {
            get { return (bool)GetValue(ButtonShareProperty); }
            set
            {
                SetValue(ButtonShareProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonShareProperty =
            DependencyProperty.Register("ButtonShare", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        public bool ButtonOptions
        {
            get { return (bool)GetValue(ButtonOptionsProperty); }
            set
            {
                SetValue(ButtonOptionsProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ButtonOptionsProperty =
            DependencyProperty.Register("ButtonOptions", typeof(bool), typeof(GamepadManager), new PropertyMetadata(false));

        #endregion

        private DirectInput directInput;
        private Guid instanceGuid;
        private Thread thread;

        public GamepadManager()
        {
            directInput = new DirectInput();
            instanceGuid = Guid.Empty;
            thread = null;
        }

        public List<DeviceInstance> GetDevices()
        {
            return directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly).ToList();
        }

        public void Select(Guid instanceGuid)
        {
            this.instanceGuid = instanceGuid;

            if (thread == null && instanceGuid != Guid.Empty)
            {
                thread = new Thread(new ThreadStart(Reader));
                thread.Start();
            }
        }

        private void Reader()
        {
            const int MAX_ANALOG = 150;

            int axisLX = 0;
            int axisLY = 0;
            int axisRX = 0;
            int axisRY = 0;

            int buttonL1 = 0;
            int buttonL2 = 0;
            int buttonL3 = 0;
            int buttonR1 = 0;
            int buttonR2 = 0;
            int buttonR3 = 0;
            int analogL2 = -130;
            int analogR2 = -130;
            int digitalPad = -1;
            int buttonTriangle = 0;
            int buttonCircle = 0;
            int buttonCross = 0;
            int buttonSquare = 0;
            int buttonShare = 0;
            int buttonOptions = 0;

            Guid deviceGuid = instanceGuid;

            Joystick joystick;

            joystick = new Joystick(directInput, deviceGuid);
            joystick.Properties.AxisMode = DeviceAxisMode.Absolute;
            joystick.Properties.DeadZone = 2560;
            joystick.Properties.Range = new InputRange(-MAX_ANALOG, MAX_ANALOG);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();

            while (deviceGuid == instanceGuid)
            {
                joystick.Poll();

                JoystickUpdate[] joystickUpdates = joystick.GetBufferedData();
                foreach (JoystickUpdate joystickUpdate in joystickUpdates)
                {
                    switch (joystickUpdate.Offset)
                    {
                        case JoystickOffset.X:
                            if (axisLX != joystickUpdate.Value / 5)
                            {
                                axisLX = joystickUpdate.Value / 5;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AxisLX = v; }), joystickUpdate.Value / 5 / (double)(MAX_ANALOG / 5));
                            }
                            break;
                        case JoystickOffset.Y:
                            if (axisLY != joystickUpdate.Value / 5)
                            {
                                axisLY = joystickUpdate.Value / 5;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AxisLY = v; }), joystickUpdate.Value / 5 / (double)(MAX_ANALOG / 5));
                            }
                            break;
                        case JoystickOffset.Z:
                            if (axisRX != joystickUpdate.Value / 5)
                            {
                                axisRX = joystickUpdate.Value / 5;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AxisRX = v; }), joystickUpdate.Value / 5 / (double)(MAX_ANALOG / 5));
                            }
                            break;
                        case JoystickOffset.RotationZ:
                            if (axisRY != joystickUpdate.Value / 5)
                            {
                                axisRY = joystickUpdate.Value / 5;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AxisRY = v; }), joystickUpdate.Value / 5 / (double)(MAX_ANALOG / 5));
                            }
                            break;
                        case JoystickOffset.Buttons4:
                            if (buttonL1 != joystickUpdate.Value)
                            {
                                buttonL1 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonL1 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons5:
                            if (buttonR1 != joystickUpdate.Value)
                            {
                                buttonR1 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonR1 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons6:
                            if (buttonL2 != joystickUpdate.Value)
                            {
                                buttonL2 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonL2 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons7:
                            if (buttonR2 != joystickUpdate.Value)
                            {
                                buttonR2 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonR2 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons10:
                            if (buttonL3 != joystickUpdate.Value)
                            {
                                buttonL3 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonL3 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons11:
                            if (buttonR3 != joystickUpdate.Value)
                            {
                                buttonR3 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonR3 = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.RotationX:
                            if (analogL2 != joystickUpdate.Value)
                            {
                                analogL2 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AnalogL2 = v; }), joystickUpdate.Value / (double)MAX_ANALOG);
                            }
                            break;
                        case JoystickOffset.RotationY:
                            if (analogR2 != joystickUpdate.Value)
                            {
                                analogR2 = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<double>((double v) => { AnalogR2 = v; }), joystickUpdate.Value / (double)MAX_ANALOG);
                            }
                            break;
                        case JoystickOffset.PointOfViewControllers0:
                            if (digitalPad != joystickUpdate.Value)
                            {
                                digitalPad = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<int>((int v) =>
                                {
                                    bool up = v == 0 || v == 4500 || v == 31500;
                                    bool left = v == 22500 || v == 27000 || v == 31500;
                                    bool down = v == 13500 || v == 18000 || v == 22500;
                                    bool right = v == 4500 || v == 9000 || v == 13500;

                                    if (ButtonUp != up)
                                    {
                                        ButtonUp = up;
                                    }
                                    if (ButtonLeft != left)
                                    {
                                        ButtonLeft = left;
                                    }
                                    if (ButtonDown != down)
                                    {
                                        ButtonDown = down;
                                    }
                                    if (ButtonRight != right)
                                    {
                                        ButtonRight = right;
                                    }
                                }), joystickUpdate.Value);
                            }
                            break;
                        case JoystickOffset.Buttons3:
                            if (buttonTriangle != joystickUpdate.Value)
                            {
                                buttonTriangle = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonTriangle = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons2:
                            if (buttonCircle != joystickUpdate.Value)
                            {
                                buttonCircle = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonCircle = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons1:
                            if (buttonCross != joystickUpdate.Value)
                            {
                                buttonCross = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonCross = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons0:
                            if (buttonSquare != joystickUpdate.Value)
                            {
                                buttonSquare = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonSquare = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons8:
                            if (buttonShare != joystickUpdate.Value)
                            {
                                buttonShare = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonShare = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        case JoystickOffset.Buttons9:
                            if (buttonOptions != joystickUpdate.Value)
                            {
                                buttonOptions = joystickUpdate.Value;

                                Dispatcher.BeginInvoke(new Action<bool>((bool v) => { ButtonOptions = v; }), joystickUpdate.Value != 0);
                            }
                            break;
                        default:
                            Debug.WriteLine(joystickUpdate);
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            joystick.Unacquire();

            thread = null;
        }
    }
}
