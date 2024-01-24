using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class atteckSprit : MonoBehaviour
{
    [Header("tipo")]
    [SerializeField] bool ADistancia;
    Rigidbody2D Rigidbody2D;
    [Header("força")]
    [SerializeField] float forca;
    [Header("dano")]
    [SerializeField] float damege;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (ADistancia)
        {
            Invoke("destoyTime",5f);
            if (Rigidbody2D)
            {
                Rigidbody2D.AddRelativeForce(Vector2.right *forca);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<enemyControle>().Life -= damege;
            //enemyControl.Life -= damege; 
        }
        if (ADistancia) Destroy(gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<enemyControle>().Life -= damege;
            //enemyControl.Life -= damege; 
        }
    }

    void destoyTime()
    {
        Destroy(gameObject);
    }
}
