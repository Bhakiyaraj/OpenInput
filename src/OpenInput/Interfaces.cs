﻿namespace OpenInput
{
    using OpenInput.Touch;
    using System;
    
    /// <summary>
    /// Interface for a basic device.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Interface for a device with a state.
    /// </summary>
    public interface IDevice<TState> : IDevice 
        where TState : struct
    {
        /// <summary>
        /// Gets the current state of the device.
        /// </summary>
        TState GetCurrentState();
    }
    
    /// <summary>
    /// Interface for multiple devices.
    /// </summary>
    public interface IDevices<TDevice, TState> 
        where TDevice : IDevice<TState>
        where TState : struct
    {
        /// <summary> Occurs when a new device is connecting. </summary>
        event EventHandler<DeviceChangedEventArgs> Connecting;

        /// <summary> Occurs when a device is disconnecting. </summary>
        event EventHandler<DeviceChangedEventArgs> Disconnecting;

        /// <summary>
        /// Get connected device count.
        /// </summary>
        int GetDevicesCount();

        /// <summary>
        /// Gets the device at index.
        /// </summary>
        TDevice GetDevice(int index);

        /// <summary>
        /// Get the current state of the device at index.
        /// </summary>
        TState GetCurrentState(int index);
    }

    /// <summary>
    /// Interface that represents a mouse.
    /// </summary>
    public interface IMouse : IDevice<MouseState>
    {
        /// <summary> Occurs when the mouse pointer moves. </summary>
        event EventHandler<MouseEventArgs> Move;

        /// <summary> Occurs when a mouse wheel moved. </summary>
        event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <summary> Occurs when a mouse button is pressed. </summary>
        event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <summary> Occurs when a mouse button is released. </summary>
        event EventHandler<MouseButtonEventArgs> MouseUp;

        /// <summary>
        /// Sets the mouse cursor position.
        /// </summary>
        void SetPosition(int x, int y);

        /// <summary>
        /// Gets the mouse cursor position.
        /// </summary>
        void GetPosition(out int x, out int y);

        // TODO: Mouse Cursor Image
    }

    /// <summary>
    /// Interface that represents a keyboard.
    /// </summary>
    public interface IKeyboard : IDevice<KeyboardState>
    {
        /// <summary> Occurs when a key is pressed. </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary> Occurs when a key is released. </summary>
        event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Gets the <see cref="TextInput"/>.
        /// </summary>
        TextInput TextInput { get; }
    }

    /// <summary>
    /// Interface that represents a touch device.
    /// </summary>
    public interface ITouchDevice : IDevice<TouchCollection>
    {
        /// <summary>
        /// Used to determine if a touch gesture is available.
        /// </summary>
        bool IsGestureAvailable { get; }

        /// <summary>
        /// 
        /// </summary>
        GestureSample ReadGesture();
    }

    /// <summary>
    /// Interface that represent gamepads.
    /// </summary>
    public interface IGamePads<TDevice> : IDevices<TDevice, GamePadState>
        where TDevice : IDevice<GamePadState>
    {
        /// <summary> Occurs when a button is pressed. </summary>
        event EventHandler<GamePadEventArgs> buttonDown;

        /// <summary> Occurs when a button is released. </summary>
        event EventHandler<GamePadEventArgs> buttonUp;
    }
}
