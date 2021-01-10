using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game
{

    public class InputManager:MonoBehaviour 
    {

        public enum ACTIONS 
        {
            UNKNOWN, MOVE_X, MOVE_Z, JUMP, ATTACK_LEFT, ATTACK_RIGHT, MOUSE_X, MOUSE_Y,
            SPECIAL,    
        }
        public static System.Action<ACTIONS, int> changed = delegate {};

        private static Dictionary<ACTIONS, float> _pressedActions = new Dictionary<ACTIONS, float>();


        private void Start() {
            foreach (ACTIONS a in Enum.GetValues(typeof(ACTIONS)))
            {
                _pressedActions.Add(a, 0);
            }
        }

        public static float GetValue(ACTIONS action)
        {
            return _pressedActions[action];
        }

        public void OnUpdate()
        {
            CheckAction(ACTIONS.MOVE_X, (int)Input.GetAxisRaw("Horizontal"));
            CheckAction(ACTIONS.MOVE_Z, (int)Input.GetAxisRaw("Vertical"));
            CheckAction(ACTIONS.MOUSE_X, Input.GetAxis("Mouse X"));
            CheckAction(ACTIONS.MOUSE_Y, Input.GetAxis("Mouse Y"));
            CheckAction(ACTIONS.SPECIAL, (int)Input.GetAxisRaw("Special"));
        }

        private static void CheckAction(ACTIONS action, float value, bool emit=false)
        {
            if (_pressedActions[action] != value)
            {
                _pressedActions[action] = value;
                if (emit)
                    changed(action, (int)value);
            }
        }

    }
}