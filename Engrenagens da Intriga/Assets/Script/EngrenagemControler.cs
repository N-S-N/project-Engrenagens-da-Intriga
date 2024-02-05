using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class EngrenagemControler : MonoBehaviour
{
    bool acertou = false;

    [Header("Scripts")]
    [SerializeField] UiEnterection Brem;
    [SerializeField] Slider Slider;

    [Header("Resposta")]
    [SerializeField] int _gabartito1;
    [SerializeField] int _fimDaSessao;

    [Header("OBJ")]
    [SerializeField] GameObject engrenagem;
    [SerializeField] GameObject SliderFolt;
    [SerializeField] GameObject ButomFolt;
    [SerializeField] GameObject floatIndivialSlider;

    [Header("Envents Sistem")]
    [SerializeField] InputSystemUIInputModule[] Input;

    [Header("Input Sicrolizado")]
    [SerializeField] InputActionReference[] inputas;


    PlayerInput playerInput;

    void OnEnable()
    {
        playerInput = Brem.player.GetComponentInParent<PlayerInput>();

        playerInput.uiInputModule = Input[0];
        siconizacao(0);
    }
    void Update()
    {
        //sair
        if (playerInput.actions["Quit"].triggered)
        {
            SliderFolt.SetActive(false);
            floatIndivialSlider.SetActive(false);
            ButomFolt.SetActive(true);
            playerInput.uiInputModule = Input[1];
            siconizacao(1);
        }

        //sistema de rotação
        if (Slider.value == _fimDaSessao) Slider.value = 1;
        if (Slider.value == 0) Slider.value = _fimDaSessao-1;

        //sistema de rotação
        engrenagem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotencionFuncion()));

        //sistema de ferificação
        //tava certo e saio


        if (acertou && Slider.value != _gabartito1)
        {
            Brem.CauntCorretsInfo--;
            acertou = false;
        }
        //tava errado e acertou
        if (Slider.value == _gabartito1 && !acertou)
        {
            Brem.CauntCorretsInfo++;
            acertou = true;
        }
    }
    
    float rotencionFuncion()
    {
        float numeroRotencion = 45; //360 / 8 que e a contidade de totação deste objeto

        float rotecionAtual = numeroRotencion * Slider.value-1; // a rotação que esta atuando neste momento

        return rotecionAtual;
    }


    //atolização de navegação
    void siconizacao(int a)
    {
        Input[a].point = inputas[0];
        Input[a].leftClick = inputas[1];
        Input[a].middleClick = inputas[2];
        Input[a].rightClick = inputas[3];
        Input[a].scrollWheel = inputas[4];
        Input[a].move = inputas[5];
        Input[a].submit = inputas[6];
        Input[a].cancel = inputas[7];
        Input[a].trackedDevicePosition = inputas[8];
        Input[a].trackedDeviceOrientation = inputas[9];

    }
}
