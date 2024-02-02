using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UiEnterection : MonoBehaviour
{
    Archibald playerScript;
    enterectionScript PaiScriopt;
    PlayerInput playerInput;

    [Header("Canvas Ui")]
    [SerializeField] Canvas canva; //canva onde tem o enigma

    [Header("quantos problema tem que ser resolvido")]
    [SerializeField] int _Number;

    [Header("Quantidade de problema resovido")]
    public int CauntCorretsInfo = 0; //Quantidade de problema resovido

    [Header("Envents Sistem")]
    [SerializeField] InputSystemUIInputModule Input;

    [Header("OBJ")]
    [SerializeField] public GameObject Pai;
    [SerializeField] GameObject SliderFolt;
    [SerializeField] GameObject ButomFolt;



    public GameObject player;


    //start
    private void Start()
    {
        PaiScriopt = Pai.GetComponent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
        player = PaiScriopt.PlayerObj;
        playerInput = player.GetComponentInParent<PlayerInput>();

        playerInput.uiInputModule = Input;

    }

    //atolizaçõas
    private void Update()
    {
        if (_Number == CauntCorretsInfo)
        {
            Invoke("EnigmaResolucion",1.3f);
        }

        if (playerInput.actions["Quit"].triggered)
        {
            if (SliderFolt.activeSelf == false)
            {
                Exit();
            }
        }
    }

    //relução
    public void EnigmaResolucion()
    {
        Debug.Log("resolveu");
        Debug.Log(CauntCorretsInfo);
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

}
