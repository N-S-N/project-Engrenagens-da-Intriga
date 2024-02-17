using UnityEngine;
using UnityEngine.InputSystem;

public class movePlayer : MonoBehaviour
{
    [Header("personagem\n" +
       "0 = Archibald Cogsworth\n" +
       "1 = Victoria Ironheart\n" +
       "2 = Capitão Octavius Steele\n" +
       "3 = Lady Seraphina Ravenscroft")]
    public int personagem;
    PlayerInputManager inputManager;
    PlayerInput input;

    public int atteckmove = 1;

    public float[] SppedWalk;

    public bool isMove = false;

    [SerializeField] private float _relodDesh;

    public Vector2 _direction;

    public int PlayerInfo;
    public bool IsLife = true;

    [SerializeField] Transform TransformCam;
    public Rigidbody2D rb2D;
    void Start()
    {
        inputManager = FindFirstObjectByType<PlayerInputManager>();
        input = GetComponent<PlayerInput>();
        PlayerInfo = inputManager.playerCount -1;
        personagem = PlayerPrefs.GetInt("Player" + inputManager.playerCount)-1;
        rb2D = GetComponent<Rigidbody2D>();
        PlayerPrefs.SetInt("playerCont", inputManager.playerCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLife)
        {
            rb2D.velocity = Vector2.zero;
            return;
        }
        TransformCam.position = new Vector3(transform.position.x, transform.position.y, -10);
        rb2D.velocity = (input.actions["Move"].ReadValue<Vector2>() * SppedWalk[personagem]) / atteckmove;
        if (rb2D.velocity != new Vector2(0, 0))
        {
            isMove = true;
            _direction = input.actions["Move"].ReadValue<Vector2>() * SppedWalk[personagem];
        }
        else
        {
            isMove = false;
        }

        if (personagem != 2) return;
        //bug
        /*
        if (input.actions["desh"].triggered && _reloddesh)
        {
            isDesh = true;
            _isDesh = true;
            _reloddesh = false;
            Invoke("RecuperouDesh", _relodDesh);
        }
        else
        {
            isDesh = false;
        }

        if (_isDesh && TimeDesh >= 0)
        {
            rb2D.velocity = (_direction * SppedWalk[personagem-1]) * 1.2f;

            TimeDesh -= Time.deltaTime;
        }
        */
    }

    public void stop()
    {
        rb2D.velocity = Vector2.zero;
    }

    public void dead()
    {

    }
}
