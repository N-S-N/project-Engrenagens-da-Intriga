
using UnityEngine;

public class fireConfigEnemy : MonoBehaviour
{
    Rigidbody2D RD2D;
    [SerializeField] float force;
    public float demege;
    public Vector2 dir;
    private void Start()
    {
        RD2D = GetComponent<Rigidbody2D>();
        if (force != 0) Destroy(gameObject, 10);
        //RD2D.AddForce(new Vector2(100, 0) * force);
        if (force == 0) return;
        RD2D.AddForce(dir * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (force != 0) Destroy(gameObject,0.2f);

    }
}
