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
            gamepad_crouch = gamepad_enabled && Input.GetKey(gamepad_keycode(1));
            keyboard_crouch = keyboard_enabled &&
                (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
            bool crouch = gamepad_crouch || keyboard_crouch;

            string gamepad_x_axis = "JoyX";
            if (controller_id > 0)
                gamepad_x_axis = "Joy" + controller_id + "X";
            float gamepad_h = gamepad_enabled ? CrossPlatformInputManager.GetAxis(gamepad_x_axis) : 0;
            float keyboard_h = 0;
            if (keyboard_enabled && Input.GetKey(KeyCode.RightArrow))
                keyboard_h++;
            if (keyboard_enabled && Input.GetKey(KeyCode.LeftArrow))
                keyboard_h--;
            float h = gamepad_h + keyboard_h;

            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
