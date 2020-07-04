using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Goomba : MonoBehaviour
{
    public float m_SpeedMove = 5.0f;
    public float m_SpeedRotation = 15.0f;
    public Vector3 m_Axis = Vector3.right;

    private Rigidbody m_Body;
    private Collider m_Collider;

    private void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        m_Body.MovePosition(m_Body.position + m_Axis * m_SpeedMove * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        if (m_Axis.sqrMagnitude > 0.001f)
        {
            var forwardRotation = Quaternion.Euler(0, -90, 0) * Quaternion.LookRotation(m_Axis);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) return;

        var impact = collision.contacts[0].point;
        var bounds = m_Collider.bounds;


        Debug.Log($"Impact: {impact} | {bounds.min} | {bounds.max}");

        /*
        if (impact.y <= bounds.max.y)
        {
            Debug.Log("Em cima");
        }
        */
        Debug.Log($"{bounds.min.x} <= {impact.x} = {bounds.min.x <= impact.x} || {impact.x} >= {bounds.max.x} = {impact.x >= bounds.max.x}");

        if (bounds.min.x <= impact.x || impact.x >= bounds.max.x)
        {
            m_Axis.x *= -1;
        } 
    }
}
