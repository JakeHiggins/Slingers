using System;
using System.Collections;
using UnityEngine;

namespace Slingers.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {

        [SerializeField] private float m_MaxSpeed = 10f;
        [SerializeField] private float m_JumpForce = 400f;
        [SerializeField] private float m_SlideMaxSpeed = 15f;
        [SerializeField] private bool m_AirControl = true;
        [SerializeField] private bool m_DoubleJumpEnabled = true;
        [SerializeField] private LayerMask m_Ground;

        private Transform m_GroundRay;
        const float k_GroundRadius = .2f;
        public bool m_OnGround;
        public bool m_DoubleJumpActive;        // Prevents double jump while holding input
        public bool m_DoubleJumping;           // Checks if the character is double jumping
        private Transform m_CeilingRay;
        const float k_CeilingRadius = .01f;
        private Animator m_Anim;
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;

        private void Awake()
        {
            m_GroundRay = transform.Find("GroundCheck");
            m_CeilingRay = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            m_OnGround = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundRay.position, k_GroundRadius, m_Ground);
            foreach(Collider2D col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    m_OnGround = true;
                    m_DoubleJumping = false;
                    m_DoubleJumpActive = false;
                }
            }
            m_Anim.SetBool("Ground", m_OnGround);

            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingRay.position, k_CeilingRadius, m_Ground))
                {
                    crouch = true;
                }
            }

            m_Anim.SetBool("Crouch", crouch);

            if(m_OnGround || m_AirControl)
            {
                move = (crouch ? move*0.5f : move);

                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                if(move > 0 && !m_FacingRight)
                {
                    Flip();
                }

                else if(move < 0 && m_FacingRight)
                {
                    Flip();
                }
            }

            if(((m_OnGround && m_Anim.GetBool("Ground")) || m_DoubleJumpActive) && jump)
            {
                if(m_DoubleJumpActive)
                {
                    Debug.Log("Double Jump");
                    m_DoubleJumpActive = false;
                    m_DoubleJumping = true;
                }
                m_OnGround = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        private void Flip()
        {
            m_FacingRight = !m_FacingRight;

            Vector3 charScale = transform.localScale;
            charScale.x *= -1;
            transform.localScale = charScale;
        }

        public bool FacingRight
        {
            get { return m_FacingRight; }
        }

        public void AllowDoubleJump()
        {
            if(!m_DoubleJumping && !m_OnGround)
                m_DoubleJumpActive = true;
        }
    }
}