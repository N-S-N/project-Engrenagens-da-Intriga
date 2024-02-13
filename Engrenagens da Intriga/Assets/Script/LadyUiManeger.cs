using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LadyUiManeger : MonoBehaviour
{
    [Header("UI Enemy Dada Base")]
    [SerializeField] bool _isDataBase;

    Archibald playerScript;
    enterectionScript PaiScriopt;
    PlayerInput playerInput;

    [Header("Canvas Ui")]
    [SerializeField] Canvas canva; //canva onde tem o enigma
    
    [Header("Gabarito")]
    [SerializeField] int _Number;
    [SerializeField] public int Decenas;

    [Header("numero atual")]
    public int CauntCorretsInfo = 0; //Quantidade de problema resovido
    
    [Header("Envents Sistem")]
    [SerializeField] InputSystemUIInputModule Input;

    [Header("OBJ")]
    [SerializeField] public GameObject Pai;

    [Header("Text")]
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text error;

    [Header("player")]
    public GameObject player;

    [Header("Input Sicrolizado")]
    [SerializeField] InputActionReference[] inputas;
    void Start()
    {
        PaiScriopt = Pai.GetComponent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
        player = PaiScriopt.PlayerObj;
        playerInput = player.GetComponentInParent<PlayerInput>();

        playerInput.uiInputModule = Input;
        siconizacao();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_isDataBase)
        {
            Decenas = CauntCorretsInfo / 10;

            text.text = CauntCorretsInfo.ToString();

            if (playerInput.actions["Quit"].triggered)
            {
                Exit();
            }
        }
        else
        {
            if (playerInput.actions["Quit"].triggered)
            {
                EnigmaResolucion();
            }
        }
    }
    
    //verificacao
    public void confirma()
    {
        if (_Number == CauntCorretsInfo)
        {
            Invoke("EnigmaResolucion", 1.5f);
        }
        else
        {
            error.text = "senha errada";
            Invoke("erro", 2f);
        }
    }
    void erro()
    {
        error.text = "";
    }

    public void apagar()
    {
        if (Decenas < 0) return;
        if (Decenas == 0)CauntCorretsInfo = 0; 
        CauntCorretsInfo /= 10; 

    }
    
    //relução
    public void EnigmaResolucion()
    {
        playerScript.resoveu = true;
        Destroy(Pai);
        Destroy(gameObject);
    }

    //saida
    public void Exit()
    {
        PaiScriopt.PlayerObj = null;
        playerScript.resoveu = true;
        Destroy(gameObject);
    }

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
