using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float m_SpeedMove = 5.0f;
    public float m_SpeedRun = 15.0f;
    public float m_SpeedRotation = 15.0f;
    private bool m_IsRunning;

    [Header("Jump")]
    public float m_JumpForce = 6.5f;
    public float m_JumpTime = 0.2f;
    private float m_JumpElapsedTime;
    private bool m_IsJumping;

    [Header("Ground")]
    public float m_GroundDistance = 0.1f;
    public LayerMask m_GroundLayer;
    public Transform m_Feet;
    private bool m_IsGrounded;

    private Rigidbody m_Body;
    private Vector3 m_Movement = Vector3.zero;

    [Header("Sounds")]
    private AudioSource m_Audio;
    public AudioClip JumpSmall;
    public AudioClip JumpSuper;
    private void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_IsGrounded = Physics.CheckSphere(m_Feet.position, m_GroundDistance, m_GroundLayer, QueryTriggerInteraction.Ignore);
        if (m_IsGrounded && !m_IsJumping)
            m_Body.velocity = new Vector3(m_Body.velocity.x, 0.0f, 0.0f);

        m_IsRunning = Input.GetButton("Fire1");

        m_Movement.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_IsJumping = true;
            m_JumpElapsedTime = 0;
        }
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
        Rotate();
    }

    private void Jump()
    {
        if(m_IsJumping && m_JumpElapsedTime > (m_JumpTime/ 3)){
            if(Input.GetButton("Jump"))
            {
                if(!m_Audio.isPlaying){
                    m_Audio.clip = JumpSuper;
                    m_Audio.Play();
                }
                
            }
            else
            {
                m_IsJumping = false;            
                if(!m_Audio.isPlaying){
                    m_Audio.clip = JumpSmall;
                    m_Audio.Play();
                }
            }
        }

        if (m_IsJumping && m_JumpElapsedTime < m_JumpTime)
        {
            m_JumpElapsedTime += Time.fixedDeltaTime;
            float proportionCompleted = Mathf.Clamp01(m_JumpElapsedTime / m_JumpTime);
            float currentForce = Mathf.Lerp(m_JumpForce, 0.0f, proportionCompleted);
            
            m_Body.AddForce(Vector3.up * currentForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            m_IsJumping = false;
        }
    }

    private void Move()
    {
        if (m_Movement.x != 0.0f)
        {
            float speed = m_IsRunning ? m_SpeedRun : m_SpeedMove;
            m_Body.MovePosition(m_Body.position + m_Movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            var velocity = m_Body.velocity;
            velocity.x = 0.0f;

            m_Body.velocity = velocity;
        }
    }

    private void Rotate()
    {
        if (m_Movement.sqrMagnitude > 0.001f)
        { 
            var forwardRotation = Quaternion.Euler(0, -90, 0) * Quaternion.LookRotation(m_Movement);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }
}
