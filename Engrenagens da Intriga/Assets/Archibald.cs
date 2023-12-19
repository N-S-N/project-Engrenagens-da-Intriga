using UnityEngine;
using UnityEngine.InputSystem;

public class Archibald : MonoBehaviour
{
    #region variaves
    //personagem
    [Header("personagem\n" +
        "0 = Archibald Cogsworth\n" +
        "1 = Victoria Ironheart\n" +
        "2 = Capitão Octavius Steele\n" +
        "3 = Lady Seraphina Ravenscroft")]
    [SerializeField] int personagem;
    //variaves
    [Header("sistema de movimentação")]
    [SerializeField] float speedWalk;
    [Header("tempo")]
    [SerializeField] float timecreat;
    [SerializeField] float timeatteck;
    [SerializeField] float timedamege;
    [SerializeField] float timedash;
    [Header("campo de visão")]
    [SerializeField] float Rangecret;
    [Header("Layer")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask playerMask;
    [Header("tag")]
    [SerializeField] string taginteracao;
    [Header("objeto")]
    [SerializeField] GameObject attek;
    [SerializeField] Transform posisionatteck;
    [Header("audio")]
    [SerializeField] AudioClip[] audios;
    [Header("relod")]
    [SerializeField] float reloddesh;

    //public
    public bool resoveu;

    //privada
    private AudioSource audiosource;
    private Rigidbody2D rb2D;
    private Collider2D coll2D;
    private SpriteRenderer imagerender;
    private Animator InimeAnimator;
    private PlayerInput playerInput;
    private bool demegecolider;
    private int atteckmove = 1;
    private float deshrelod = 0;
    private Vector2 derictionmove;
    private Camera mainCamera;
    private Quaternion rotacao;
    private bool joy;
    //just for debugging
    [Header("colisãos")]
    [SerializeField] Collider2D colliderSeeing = null;

    #endregion

    #region state
    enum State
    {
        Iddle,
        move,
        interaction,
        atteck,
        damege,
        desh
    }

    [Header("estaddo")]
    [SerializeField] State enemyState;
    float stateTIme;

    #endregion

    #region updete e start
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        audiosource = GetComponent<AudioSource>();
        enemyState = State.Iddle;
        rb2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        imagerender = GetComponent<SpriteRenderer>();
        InimeAnimator = GetComponent<Animator>();
        resoveu = false;
    }

    void Update()
    {
        float delta = Time.deltaTime;
        homdleenemyFSM(delta);
        //InimeAnimator.SetInteger("State", (int)enemyState);
        deshrelod -= delta;
        rotecion();
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
                if (OnMove())
                {
                    return State.move;
                }
                if (interactionbotom() && interacaoobj())
                {
                    if (personagem == 0 || personagem == 1 || personagem == 3) { 
                        return State.interaction;
                    }
                }
                if (atteckbotom())
                {
                    return State.atteck;
                }
                if (demegecolider)
                {
                    return State.damege;
                }
                if (personagem == 2 && deshrelod <= 0 && deshbotom())
                {
                    return State.desh;
                }
                break;

            case State.move: 
                if (!OnMove())
                {
                    return State.Iddle;
                }
                if (interactionbotom() && interacaoobj())
                {
                    if (personagem == 0 || personagem == 1 || personagem == 3)
                    {
                        return State.interaction;
                    }
                }
                if (atteckbotom())
                {
                    return State.atteck;
                }
                if (demegecolider)
                {
                    return State.damege;
                }
                if (personagem == 2 && deshrelod <= 0 && deshbotom())
                {
                    return State.desh;
                }
                break;

            case State.interaction:
                if (time >= timecreat && personagem == 1 || personagem == 3)
                {
                    return State.Iddle;
                }
                if (personagem == 0 && resoveu)
                {
                    return State.Iddle;
                }
                if (demegecolider)
                {
                    return State.damege;
                }
                break;

            case State.atteck:
                if (time >= timeatteck)
                {
                    return State.Iddle;
                }
                if (demegecolider)
                {
                    return State.damege;
                }
                break;

            case State.damege:
                if (time >= timedamege)
                {
                    return State.Iddle;
                }
                break;

            case State.desh:
                if (time >= timedash)
                {
                    return State.Iddle;
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
            case State.atteck:
                atteckmove = 1;
                break;
            case State.desh:
                deshrelod = reloddesh;
                break;
            case State.interaction:
                resoveu = false;
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
            case State.atteck:
                atteckfuncion();
                atteckmove = 2;
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
            case State.move:
                mover();
                break;
            case State.atteck:
                mover();
                break;
            case State.desh:
                desh();
                break;
            case State.interaction:
                stop();
                break;
            case State.Iddle:
                stop();
                break;
            default:
                break;
        }
    }
    #endregion

    #region sistemas
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //}


    //butao de mover
    bool OnMove()
    {
        if (playerInput.actions["Move"].inProgress)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //butao de interagir
    bool interactionbotom()
    {
        if (playerInput.actions["interaction"].triggered)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //butom de atacar
    bool atteckbotom()
    {
        if (playerInput.actions["atteck"].inProgress)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //butom desh
    bool deshbotom()
    {
        if (playerInput.actions["desh"].triggered)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //colicão entrada
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            demegecolider = true;
        }
    }
    //colicão saida
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            demegecolider = false;
        }
    }

    private bool interacaoobj()
    {
        RaycastHit2D m_HitDetect = Physics2D.CircleCast(transform.position, Rangecret, Vector2.zero);
        if (m_HitDetect.collider.gameObject.CompareTag(taginteracao))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void atteckfuncion()
    {
        Instantiate(attek, posisionatteck.position, transform.rotation);
    }

    //rotação
    void rotecion()
    {
        if (playerInput.actions["key"].triggered)
        {
            joy = true;
        }
        else if (playerInput.actions["joy"].triggered)
        {
            joy = false;
        }
        if (joy)
        {
            //mause
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 b = mainCamera.ScreenToWorldPoint(playerInput.actions["rotecion"].ReadValue<Vector2>());
            Vector2 a = b - position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg));
        }
        else if (playerInput.actions["rotecion"].ReadValue<Vector2>().x != 0 && playerInput.actions["rotecion"].ReadValue<Vector2>().y != 0)
        {
            //controle
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(playerInput.actions["rotecion"].ReadValue<Vector2>().y, playerInput.actions["rotecion"].ReadValue<Vector2>().x) * Mathf.Rad2Deg));
            rotacao = transform.rotation;
        }
        else
        {
            transform.rotation = rotacao;
        }
    }

    #endregion

    #region movimentacao

    private void mover()
    {
        rb2D.velocity = (playerInput.actions["Move"].ReadValue<Vector2>() * speedWalk) / atteckmove;
        if (rb2D.velocity != new Vector2(0,0)) 
        {
            derictionmove = playerInput.actions["Move"].ReadValue<Vector2>();
        }
    }

    private void desh()
    {
        rb2D.velocity = (derictionmove * speedWalk) * 2;
    }

    private void stop()
    {
        rb2D.velocity = Vector2.zero;
    }
    //rotação

    #endregion

}
