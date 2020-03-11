using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MotoresJogosFase1
{
    public enum Input
    {
        None,
        Fire,
        Pause,
        Enter
    }

    public enum MouseInput
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
    }

    public class InputManager
    {
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState currentMouseState;
        public MouseState previousMouseState;
        public Dictionary<Input, Keys> KeyboardMap;
        public Dictionary<Input, MouseInput> MouseMap;

        public void Initialize()
        {
            KeyboardMap = new Dictionary<Input, Keys>();
            MouseMap = new Dictionary<Input, MouseInput>();

            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
            SetDictionary();
        }

        public void Update()
        {
            // Save the one and only (if available) keyboardstate 
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            // Save the one and only (if available) mousestate 
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        void SetDictionary()
        {
            KeyboardMap.Add(Input.None, Keys.None);
            KeyboardMap.Add(Input.Fire, Keys.Space);
            KeyboardMap.Add(Input.Pause, Keys.Escape);
            KeyboardMap.Add(Input.Enter, Keys.Enter);

            MouseMap.Add(Input.Fire, MouseInput.LeftButton);
        }

        public bool IsPressed(Input input)
        {
            Keys key = KeyboardMap.TryGetValue(input, out key) ? key : default(Keys);
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool Clicked(Input input)
        {
            Keys key = KeyboardMap.TryGetValue(input, out key) ? key : default(Keys);

            return !currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        public bool IsPressed(MouseInput input)
        {
            return IsPressed(currentMouseState, input);
        }

        public bool Clicked(MouseInput input)
        {
            return !IsPressed(currentMouseState, input) && IsPressed(previousMouseState, input);
        }

        public bool IsPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        private bool IsPressed(MouseState state, MouseInput input)
        {
            switch (input)
            {
                case MouseInput.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseInput.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseInput.RightButton:
                    return state.RightButton == ButtonState.Pressed;
            }
            return false;
        }

        public float GetRaw(Input input)
        {
            var raw = GetRaw(input);
            return raw;
        }

        public Point GetMousePosition()
        {
            return currentMouseState.Position;
        }

        public bool IsMouseMoved()
        {
            return currentMouseState.X != previousMouseState.X || currentMouseState.Y != previousMouseState.Y;
        }

        public int GetMouseScroll()
        {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }
    }
}
