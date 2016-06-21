using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Slingers.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterUserControl : MonoBehaviour
    {
        [Range(1,4)][SerializeField] int playerNumber = 1;
        private CharacterController m_Character;
        public bool m_Jump;
        public bool m_HoldingJump;

        private void Awake()
        {
            m_Character = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // Ensures jump isn't being held and activates double jump if it's released while jumping
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if(m_HoldingJump)
            {
                if(CrossPlatformInputManager.GetButtonUp("Jump"))
                {
                    m_Jump = false;
                    m_HoldingJump = false;
                    m_Character.AllowDoubleJump();
                }
            }
        }

        private void FixedUpdate()
        {
            bool crouch = CrossPlatformInputManager.GetButton("Crouch");
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            bool jump = m_HoldingJump ? false : m_Jump;

            m_Character.Move(h, crouch, jump);

            if (m_Jump)
                m_HoldingJump = true;
        }
    }
}