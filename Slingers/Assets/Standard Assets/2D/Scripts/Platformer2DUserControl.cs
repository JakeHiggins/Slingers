using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [Range(0, 4)] public int controller_id = 0;
        public bool keyboard_enabled = true;
        public bool gamepad_enabled = true;

        protected bool gamepad_crouch;
        protected bool keyboard_crouch;
        protected bool m_Jump;
        protected PlatformerCharacter2D m_Character;

        protected string gamepad_keycode(int button)
        {
            string joystick = "joystick";
            if (controller_id > 0) joystick = "joystick " + controller_id;
            return joystick + " button " + button;
        }

        public void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            if (controller_id > 0)
            {
                string[] attached_joysticks = Input.GetJoystickNames();
                bool found_desired_joystick = false;
                int first_working_joystick = -1;
                for (int i = 0; i < attached_joysticks.Length; ++i)
                {
                    int joystick_id = i + 1;
                    //empty strings seem to indicate unrecognized or disconnected joysticks (ghosts)
                    if (string.IsNullOrEmpty(attached_joysticks[i]))
                    {
                        Debug.LogWarningFormat("Joystick #{0} detected but null", joystick_id);
                        continue;
                    }
                    if (joystick_id == controller_id)
                    {
                        found_desired_joystick = true;
                    }
                    if(first_working_joystick == -1)
                    {
                        first_working_joystick = joystick_id;
                    }
                }
                if (found_desired_joystick == false)
                {
                    if (first_working_joystick == -1)
                    {
                        Debug.LogWarningFormat("No joysticks found. Disabling gamepad for {0}", gameObject.name);
                        gamepad_enabled = false;
                    }
                    else
                    {
                        Debug.LogWarningFormat("Switching \"{0}\" to joystick #{1}", gameObject.name, first_working_joystick);
                        controller_id = first_working_joystick;
                    }
                }
            }
        }
        public void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                bool gamepad_jump = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(0));
                bool keyboard_jump = keyboard_enabled && Input.GetKeyDown(KeyCode.Space);
                m_Jump = gamepad_jump || keyboard_jump;
            }
        }
        public void FixedUpdate()
        {
            // Pass all parameters to the character control script.
            string gamepad_y_axis = "JoyY";
            if (controller_id > 0)
                gamepad_y_axis = "Joy" + controller_id + "Y";
            float gamepad_vertical = CrossPlatformInputManager.GetAxis(gamepad_y_axis);
            gamepad_crouch = gamepad_enabled && (Input.GetKey(gamepad_keycode(1)) || gamepad_vertical > 0.3f);
            keyboard_crouch = keyboard_enabled &&
                (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
            bool crouch = gamepad_crouch || keyboard_crouch;

            string gamepad_x_axis = "JoyX";
            if (controller_id > 0)
                gamepad_x_axis = "Joy" + controller_id + "X";
            float gamepad_h = gamepad_enabled ? CrossPlatformInputManager.GetAxis(gamepad_x_axis) : 0;
            float keyboard_h = 0;
            if (keyboard_enabled && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                keyboard_h++;
            if (keyboard_enabled && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
                keyboard_h--;
            float h = gamepad_h + keyboard_h;

            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
