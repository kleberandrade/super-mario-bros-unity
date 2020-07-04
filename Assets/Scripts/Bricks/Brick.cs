using UnityEngine;

public class Brick : MonoBehaviour
{
    public float m_BounceTime = 0.3f;
    public float m_BounceDistance = 0.3f;
    public AnimationCurve m_Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

    private Vector3 m_Source;
    private bool m_CanBounce;
    private float m_StartTime;
    private Bounds m_Bounds;

    private void Start()
    {
        m_Source = transform.position;
        m_Bounds = GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        if (!m_CanBounce) return;

        float time = (Time.time - m_StartTime) / m_BounceTime;
        transform.position = m_Source + Vector3.up * m_BounceDistance * m_Curve.Evaluate(time);
        m_CanBounce = time < 1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 impact = collision.contacts[0].point;
        if (impact.y <= m_Bounds.min.y)
        {
            m_StartTime = Time.time;
            m_CanBounce = true;
        }
    }
}
