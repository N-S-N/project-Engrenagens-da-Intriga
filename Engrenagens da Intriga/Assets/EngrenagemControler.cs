using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EngrenagemControler : MonoBehaviour
{
    bool acertou = false;

    [Header("Scripts")]
    [SerializeField] UiEnterection Brem;
    [SerializeField] Slider Slider;

    [Header("Resposta")]
    [SerializeField] int _gabartito;

    [Header("OBJ")]
    [SerializeField] GameObject engrenagem;
    [SerializeField] GameObject SliderFolt;
    [SerializeField] GameObject ButomFolt;


    PlayerInput playerInput;

    void OnEnable()
    {
        playerInput = Brem.player.GetComponent<PlayerInput>();
    }
    void Update()
    {
        //sair
        if (playerInput.actions["Quit"].triggered)
        {
            ButomFolt.SetActive(true);
            SliderFolt.SetActive(false);
        }

        //sistema de rota��o
        if (Slider.value == 9) Slider.value = 1;
        if (Slider.value == 0) Slider.value = 8;

        //sistema de rota��o
        engrenagem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotencionFuncion()));

        //sistema de ferifica��o
        //tava certo e saio
        if (Slider.value != _gabartito && acertou)
        {
            Brem.CauntCorretsInfo--;
            acertou = false;
        }
        //tava errado e acertou
        if (Slider.value == _gabartito && !acertou)
        {
            Brem.CauntCorretsInfo++;
            acertou = true;
        }

    }
    
    float rotencionFuncion()
    {
        float numeroRotencion = 45; //360 / 8 que e a contidade de tota��o deste objeto

        float rotecionAtual = numeroRotencion * Slider.value-1; // a rota��o que esta atuando neste momento

        return rotecionAtual;
    }

}
