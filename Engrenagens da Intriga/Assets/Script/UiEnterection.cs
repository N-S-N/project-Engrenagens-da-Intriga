using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UiEnterection : MonoBehaviour
{
    Archibald playerScript;
    enterectionScript PaiScriopt;

    [Header("Canvas Ui")]
    [SerializeField] Canvas canva; //canva onde tem o enigma

    [Header("quantos problema tem que ser resolvido")]
    [SerializeField] int _Number;

    [Header("Quantidade de problema resovido")]
    public int CauntCorretsInfo = 0; //Quantidade de problema resovido

    [Header("Envents Sistem")]
    [SerializeField] MultiplayerEventSystem[] EventSystems;

    public GameObject player;
    //start

    private void OnEnable()
    {
        PaiScriopt = GetComponentInParent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
        player = PaiScriopt.PlayerObj;
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
    }

    //relução
    public void EnigmaResolucion()
    {
        playerScript.resoveu = true;
        Destroy(PaiScriopt.gameObject);
    }

}
