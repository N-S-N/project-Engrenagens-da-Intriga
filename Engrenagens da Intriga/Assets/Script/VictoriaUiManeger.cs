using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VictoriaUiManeger : MonoBehaviour
{
    Archibald playerScript;
    enterectionScript PaiScriopt;
    PlayerInput playerInput;

    [Header("Canvas Ui")]
    [SerializeField] Canvas canva; //canva onde tem o enigma

    [Header("Envents Sistem")]
    [SerializeField] InputSystemUIInputModule Input;

    [Header("OBJ")]
    [SerializeField] public GameObject Pai;

    [Header("player")]
    public GameObject player;

    [Header("Input Sicrolizado")]
    [SerializeField] InputActionReference[] inputas;

    [Header("imagem")]
    [SerializeField] Image cano;

    [SerializeField] AudioClip door;
    AudioSource sorce;
    
    void Start()
    {
        PaiScriopt = Pai.GetComponent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
        player = PaiScriopt.PlayerObj;
        playerInput = player.GetComponentInParent<PlayerInput>();
        sorce = GetComponent<AudioSource>();
        playerInput.uiInputModule = Input;
        siconizacao();

        cano.color = Color.gray;
    }

    void Update()
    {
        if (playerInput.actions["Quit"].triggered)
        {
            Exit();
        }
        
    }

    //resolucao
    public void EnigmaResolucion()
    {
        sorce.PlayOneShot(door);
        Invoke("awitengresolucion",1F);
    }

    void awitengresolucion()
    {
        playerScript.resoveu = true;
        Destroy(Pai);
        Destroy(gameObject);
    }

    //saida
    public void Exit()
    {
        Invoke("awitengExit", 1F);
    }
    void awitengExit()
    {
        PaiScriopt.PlayerObj = null;
        playerScript.resoveu = true;
        Destroy(gameObject);
    }

    //input
    void siconizacao()
    {
        Input.point = inputas[0];
        Input.leftClick = inputas[1];
        Input.middleClick = inputas[2];
        Input.rightClick = inputas[3];
        Input.scrollWheel = inputas[4];
        Input.move = inputas[5];
        Input.submit = inputas[6];
        Input.cancel = inputas[7];
        Input.trackedDevicePosition = inputas[8];
        Input.trackedDeviceOrientation = inputas[9];

    }

}
