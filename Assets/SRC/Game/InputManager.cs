using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game
{

    public class InputManager:MonoBehaviour 
    {

        public enum ACTIONS {UNKNOWN, MOVE_X, MOVE_Z, JUMP, ATTACK_LEFT, ATTACK_RIGHT, MOUSE_X, MOUSE_Y}
        public static System.Action<ACTIONS, int> changed = delegate {};

        private static Dictionary<ACTIONS, int> _pressedActions = new Dictionary<ACTIONS, int>();


        private void Start() {
            foreach (ACTIONS a in Enum.GetValues(typeof(ACTIONS)))
            {
                _pressedActions.Add(a, 0);
            }
        }

        public static int GetValue(ACTIONS action)
        {
            return _pressedActions[action];
        }

        public void OnUpdate()
        {
            CheckAction(ACTIONS.MOVE_X, (int)Input.GetAxisRaw("Horizontal"));
            CheckAction(ACTIONS.MOVE_Z, (int)Input.GetAxisRaw("Vertical"));
            CheckAction(ACTIONS.MOUSE_X, (int)Input.GetAxisRaw("Mouse X"));
            CheckAction(ACTIONS.MOUSE_Y, (int)Input.GetAxisRaw("Mouse Y"));
        }

        private static void CheckAction(ACTIONS action, int value)
        {
            if (_pressedActions[action] != value)
            {
                _pressedActions[action] = value;
                changed(action, value);
            }
        }

    }
}