using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    public GameObject m_Explosion;
    private Bounds m_Bounds;

    private void Start()
    {
        m_Bounds = GetComponent<Collider>().bounds;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("BigPlayer")) return;

        var impact = collision.contacts[0].point;
        var isBelowBrick = impact.y <= m_Bounds.min.y;
        if (!isBelowBrick) return;

        Instantiate(m_Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

