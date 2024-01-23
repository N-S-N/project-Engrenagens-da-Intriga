using UnityEngine;
using UnityEngine.InputSystem;

public class enemyControle : MonoBehaviour
{
    #region variaves
    //personagem
    [Header("personagem\n" +
        "0 = corpo a corpo\n" +
        "1 = a distancia")]
    [SerializeField] int personagem;

    //variaves
    [Header("sistema de movimentação")]
    [SerializeField] float speedWalk;
    [Header("tempo")]
    [SerializeField] float _timeAtteck;
    [Header("campo de visão")]
    [SerializeField] float _rangervision, _distanceAtteck;
    [Header("Layer")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask playerMask;
    [Header("objeto")]
    [SerializeField] GameObject attek;
    [Header("audio")]
    [SerializeField] AudioClip[] audios;
    [Header("STATE")]
    public float Maxlife;
    public float Life;


    //privada
    private bool atteckva;
    private GameObject playerdetect;
    private AudioSource audiosource;
    private Rigidbody2D rb2D;
    private Collider2D coll2D;
    private SpriteRenderer imagerender;
    private Animator InimeAnimator;
    private Vector2 direction;


    #endregion

    #region state
    enum State
    {
        Iddle,
        persigindo,
        attack

    }

    [Header("estaddo")]
    [SerializeField] State enemyState;
    float stateTIme;

    #endregion

    #region updete e start
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        enemyState = State.Iddle;
        rb2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        imagerender = GetComponent<SpriteRenderer>();
        InimeAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        float delta = Time.deltaTime;
        homdleenemyFSM(delta);
        //InimeAnimator.SetInteger("State", (int)enemyState);
        directionVio();
        if (Life <= 0) Destroy(gameObject); 
    }

    #endregion

    #region FSM
    //controle de state

    void homdleenemyFSM(float deltaTIme)
    {
        //tempo de estado
        stateTIme += deltaTIme;

        //verificar as condições de troca de estado
        var newState = TryChangeCurrentState(enemyState, stateTIme);

        //saber se trocando de estado
        if (newState != enemyState)
        {
            //troque de estado
            OnStateExit(enemyState);

            //Trocar pora um novo estado
            enemyState = newState;
            stateTIme = 0;

            //Entra no novo estado
            OnStateEnter(enemyState);
        }
        //dar uipdaite para um estado atual
        OnStateUpdete(enemyState, deltaTIme);

    }
    //tentando sai do stato atual   
    State TryChangeCurrentState(State State, float time)
    {
        switch (State)
        {

            case State.Iddle:
                if (CampoDeVisao())
                {
                    return State.persigindo;
                }
                break;
            case State.attack:
                if (time >= _timeAtteck)
                {
                    return State.persigindo;
                }
                break;
            case State.persigindo:
                if (playerdetect == null)
                {
                    return State.Iddle;
                }
                if(atteckva)
                {
                    return State.attack; 
                }

                break;
            default:
                break;

        }
        return State;
    }

    //sair do estado atual
    void OnStateExit(State State)
    {
        switch (State)
        {
            case State.attack:
                atteckva = false;
                EndeFire();
                break;
            default:
                break;
        }
    }
    //entra no estado atual
    void OnStateEnter(State State)
    {
        switch (State)
        {
            case State.attack:
                Atteck();
                break;
            default:
                break;
        }
    }
    //Updete do estado atual
    void OnStateUpdete(State State, float deltaTIme)
    {
        switch (State)
        {
            case State.persigindo:
                visionDirection();
                break;
            case State.Iddle:
                Stop();
                break;
            default:
                break;
        }
    }
    #endregion

    #region sistemas

    //visao paralela
    bool CampoDeVisao()
    {
        RaycastHit2D m_HitDetect = Physics2D.CircleCast(transform.position, _rangervision, Vector2.zero, 0, playerMask);
        if (m_HitDetect) 
        {
            playerdetect = m_HitDetect.collider.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    //visao focada
    void visionDirection()
    {
        //Debug.Log(playerdetect);
        if (!playerdetect) return;
        direction = (playerdetect.transform.position - transform.position ).normalized;
        RaycastHit2D m_HitDetect = Physics2D.Raycast(transform.position, direction, _rangervision, playerMask+ groundMask);
        if (m_HitDetect && !m_HitDetect.collider.gameObject.CompareTag("wall"))
        {
            if (Physics2D.Raycast(transform.position, direction, _distanceAtteck, playerMask)) 
            {
                atteckva = true;
                rb2D.velocity = Vector2.zero;
                return;
            }
            Move();
        }
        else
        {
            playerdetect = null;
            rb2D.velocity = Vector2.zero;
        }
    }

    void directionVio()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
    }

    #endregion

    #region move

    void Move()
    {
        rb2D.velocity = direction * speedWalk;
    }

    void Stop()
    {
        rb2D.velocity = Vector2.zero;

    }


    #endregion

    #region atteck

    void Atteck()
    {

        if (personagem == 0)
        {
            attek.SetActive(true);
        }
        else
        {
            GameObject tiro = Instantiate(attek,transform.position,transform.rotation);
            fireConfigEnemy confug;
            confug = tiro.GetComponent<fireConfigEnemy>();
            confug.dir = direction;
        }

    }

    void EndeFire()
    {
        if (personagem == 0)
        {
            attek.SetActive(false);
        }
    }


    #endregion

}
