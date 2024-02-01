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
    [SerializeField] MultiplayerEventSystem[] EventSystems;

    [Header("OBJ")]
    [SerializeField] GameObject SliderFolt;
    [SerializeField] GameObject ButomFolt;

    public GameObject player;
    //start

    private void OnEnable()
    {
        PaiScriopt = GetComponentInParent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
        player = PaiScriopt.PlayerObj;
        playerInput = player.GetComponent<PlayerInput>();
        for (int i = 0; i < EventSystems.Length; i++)
        {
            EventSystems[i].playerRoot = PaiScriopt.PlayerObj;
        }
    }

    //atolizaçõas
    private void FixedUpdate()
    {
        if (_Number == CauntCorretsInfo)
        {
            EnigmaResolucion();
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
        playerScript.resoveu = true;
        Destroy(PaiScriopt.gameObject);
    }

    //saida

    public void Exit()
    {
        playerScript.resoveu = true;
        gameObject.SetActive(false);

    }

}
