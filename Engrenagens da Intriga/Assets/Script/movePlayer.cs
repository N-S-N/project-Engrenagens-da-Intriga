
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public float TimeDesh;

    public bool isMove = false;
    public bool isDesh = false;

    [SerializeField] private float _relodDesh;

    private Vector2 _direction;

    private bool _reloddesh = true;
    private bool _isDesh = false;
    public int PlayerInfo;
    public bool IsLife = true;

    [SerializeField] Transform TransformCam;

    Rigidbody2D rb2D;
    void Start()
    {
        inputManager = FindFirstObjectByType<PlayerInputManager>();
        input = GetComponent<PlayerInput>();
        PlayerInfo = inputManager.playerCount -1;
        personagem = PlayerPrefs.GetInt("Player" + inputManager.playerCount)-1;
        Debug.Log(inputManager.playerCount);
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLife) return;
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
            rb2D.velocity = (_direction * SppedWalk[personagem-1]) * 2;

            TimeDesh -= Time.deltaTime;
        }
    }

    void RecuperouDesh()
    {
        _reloddesh = true;
    }

    public void stop()
    {
        rb2D.velocity = Vector2.zero;
    }

    public void dead()
    {

    }
}
