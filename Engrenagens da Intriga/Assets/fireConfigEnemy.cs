using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireConfigEnemy : MonoBehaviour
{
    Rigidbody2D RD2D;
    [SerializeField] float force;
    public float demege;
    private void Start()
    {
        RD2D = GetComponent<Rigidbody2D>();
        if (force != 0) Destroy(gameObject,10);
    }

    public void FireDisrary(Vector2 direction)
    {
        RD2D.AddForce(direction * force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (force != 0) Destroy(gameObject,0.2f);

    }
}
