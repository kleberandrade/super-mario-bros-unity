using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    public GameObject m_Explosion;  // <----
    private Bounds m_Bounds;

    private void Start()
    {
        m_Bounds = GetComponent<Collider>().bounds;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("BigPlayer")) return;  // <----

        Vector3 impact = collision.contacts[0].point;
        if (impact.y <= m_Bounds.min.y)
        {
            Instantiate(m_Explosion, transform.position, Quaternion.identity);  // <----
            Destroy(gameObject);  // <----
        }
    }
}
