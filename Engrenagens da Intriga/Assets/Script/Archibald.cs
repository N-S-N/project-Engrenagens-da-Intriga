using UnityEngine;
using UnityEngine.InputSystem;

public class Archibald : MonoBehaviour
{

    #region variaves
    //personagem
    [Header("personagem\n" +
        "0 = Archibald Cogsworth\n" +
        "1 = Victoria Ironheart\n" +
        "2 = Capit�o Octavius Steele\n" +
        "3 = Lady Seraphina Ravenscroft")]
    [SerializeField] int personagem;
    //variaves
    //[Header("sistema de movimenta��o")]
    //[SerializeField] float speedWalk;
    [Header("tempo")]
    [SerializeField] float[] timecreat;
    [SerializeField] float[] timeatteck;
    [SerializeField] float timedamege;
    [Header("campo de vis�o")]
    [SerializeField] float[] Rangecret;
    [Header("Layer")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enterection;
    [Header("tag")]
    [SerializeField] string[] taginteracao;
    [Header("objeto")]
    [SerializeField] GameObject[] attek;
    [SerializeField] Transform posisionatteck;
    [Header("audio")]
    [SerializeField] AudioClip[] audios;
    [Header("STATE")]
    public float[] Maxlife;
    public float[] Life;
    [Header("Camera")]
    public Camera mainCamera;
    //public
    public bool resoveu;
    [Header("Image")]
    [SerializeField] Sprite[] sprites;
    public Sprite[] foto;
    public Sprite[] blackfoto;
    [Header("dudios")]
    [SerializeField] AudioSource[] audiosource;


    //privada
    Uimaneger Uimaneger;
    private movePlayer moveplayer;
    private Rigidbody2D rb2D;
    private Collider2D coll2D;
    private SpriteRenderer imagerender;
    private Animator InimeAnimator;
    private PlayerInput playerInput;
    private bool demegecolider;
    private Vector2 derictionmove; 
    private Quaternion rotacao;
    private bool joy;
    private GameObject atteck;
    private bool _isdead = true;
    Transform playerPosision;

    #endregion

    #region state
    public enum State
    {
        Iddle,
        move,
        interaction,
        atteck,
        damege,
        desh
    }

    [Header("estaddo")]
    [SerializeField]public State enemyState;
    float stateTIme;

    #endregion

    #region updete e start
    private void Awake()
    {
        //mainCamera = GetComponent<Camera>();
        //mainCamera = GetComponentInChildren<Camera>();
    }
    void Start()
    {
        moveplayer = GetComponentInParent<movePlayer>(); 
        playerInput = GetComponentInParent<PlayerInput>();
        //audiosource = GetComponent<AudioSource>();
        enemyState = State.Iddle;
        rb2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        imagerender = GetComponent<SpriteRenderer>();
        InimeAnimator = GetComponent<Animator>();
        resoveu = false;
        Uimaneger = FindAnyObjectByType<Uimaneger>();
        personagem = moveplayer.personagem;
        playerPosision = GetComponentInParent<Transform>();

        //parte de imagems

        imagerender.sprite = sprites[personagem];
        Uimaneger.foto[moveplayer.PlayerInfo].sprite = foto[personagem];
        Uimaneger.blackfoto[moveplayer.PlayerInfo].sprite = blackfoto[personagem];
    }

    void Update()
    {
        if (Life[personagem] <= 0) 
        {
            rb2D.velocity = Vector2.zero;
            gameObject.layer = groundMask;
            gameObject.tag = "wall";
            moveplayer.gameObject.tag = "wall";
            moveplayer.gameObject.layer = groundMask;
            moveplayer.IsLife = false;
            audiosource[1].Stop();
            if (_isdead)
            {
                PlayerPrefs.SetInt("deadPlayer", PlayerPrefs.GetInt("deadPlayer") + 1);
                _isdead = false;
            }
            //Destroy(gameObject); 
            return;
        }
        
        float delta = Time.deltaTime;
        homdleenemyFSM(delta);
        //InimeAnimator.SetInteger("State", (int)enemyState);
        rotecion();

        Uimaneger.maxlife[moveplayer.PlayerInfo] = Maxlife[personagem];
        Uimaneger.life[moveplayer.PlayerInfo] = Life[personagem];
        transform.position = playerPosision.position;
    }

    #endregion

    #region FSM
    //controle de state
    
    void homdleenemyFSM(float deltaTIme)
    {
        //tempo de estado
        stateTIme += deltaTIme;

        //verificar as condi��es de troca de estado
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
                if (moveplayer.isMove)
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
                break;

            case State.move: 
                if (!moveplayer.isMove)
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
                break;

            case State.interaction:
                /*if (time >= timecreat[personagem] && personagem == 1 || personagem == 3)
                {
                    return State.Iddle;
                }*/
                if (/*personagem == 0 &&*/ resoveu)
                {
                    return State.Iddle;
                }
                if (demegecolider)
                {
                    return State.damege;
                }
                break;

            case State.atteck:
                if (time >= timeatteck[personagem])
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
                moveplayer.atteckmove = 1;
                audiosource[1].Stop();
                if (personagem == 1 || personagem == 2)
                {
                    //Destroy(atteck);
                    attek[personagem].SetActive(false);
                }
                break;
            case State.interaction:
                resoveu = false;
                break;
            case State.move:
                audiosource[1].Stop();
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
                if(personagem == 0 || personagem == 3)
                {
                    audiosource[0].volume = 0.5F;
                    audiosource[0].PlayOneShot(audios[2]);
                }
                else
                {
                    audiosource[0].volume = 0.7F;
                    audiosource[0].PlayOneShot(audios[1]);
                }
                audiosource[1].Play();
                atteckfuncion();
                moveplayer.atteckmove = 2;
                break;

            case State.move:
                audiosource[1].Play();
                break;

            case State.interaction:
                audiosource[0].volume = 1F;
                audiosource[0].PlayOneShot(audios[4]);
                Invoke("stop",0.3f);
                break;

            case State.damege:
                audiosource[0].volume = 1F;
                audiosource[0].PlayOneShot(audios[5]);
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
            case State.interaction:
                moveplayer.stop();
                break;
            case State.Iddle:
                moveplayer.stop();
                break;
            default:
                break;
        }
    }
    #endregion


    #region sistemas
    //audio
    private void stop()
    {
        audiosource[0].Stop();
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

        if (playerInput.actions["atteck"].inProgress /*|| Input.GetMouseButton(0)*/)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //colic�o entrada
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("enemyProjects"))
        {
            demegecolider = true;
            Life[personagem] -= collision.gameObject.GetComponent<fireConfigEnemy>().demege;

        }
    }
    //colic�o saida
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("enemyProjects"))
        {
            demegecolider = false;
        }
    }
    //triger entra
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("enemyProjects"))
        {
            demegecolider = true;
            Life[personagem] -= collision.gameObject.GetComponent<fireConfigEnemy>().demege;
        }
    }

    //triger sair
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("enemyProjects"))
        {
            demegecolider = false;
        }
    }

    private bool interacaoobj()
    {
        RaycastHit2D m_HitDetect = Physics2D.CircleCast(transform.position, Rangecret[personagem], Vector2.zero,0, enterection);
        if (m_HitDetect.collider.gameObject.CompareTag(taginteracao[personagem]))
        {
            m_HitDetect.collider.gameObject.GetComponent<enterectionScript>().PlayerObj = gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void atteckfuncion()
    {
        if (personagem == 3 || personagem == 0) {
            Instantiate(attek[personagem], posisionatteck.position, transform.rotation);
        }
        else
        {
            //atteck = Instantiate(attek, posisionatteck.position, transform.rotation,transform.parent);
            attek[personagem].SetActive(true);
        }
    }

    //rota��o
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
            Vector2 b = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 a = (b - position).normalized;
           
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

}
