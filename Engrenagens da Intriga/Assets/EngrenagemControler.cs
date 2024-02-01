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
    [SerializeField] int _gabartito1;
    [SerializeField] int _gabartito2;

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

        //sistema de rotação
        if (Slider.value == 9) Slider.value = 1;
        if (Slider.value == 0) Slider.value = 8;

        //sistema de rotação
        engrenagem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotencionFuncion()));

        //sistema de ferificação
        //tava certo e saio

        if (_gabartito2 == 0)
        {
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
        else
        {
            if (acertou && Slider.value != _gabartito1 && Slider.value != _gabartito2)
            {
                Brem.CauntCorretsInfo--;
                acertou = false;
            }
            //tava errado e acertou
            if (!acertou && Slider.value == _gabartito1 || Slider.value == _gabartito2)
            {
                Brem.CauntCorretsInfo++;
                acertou = true;
            }
        }
    }
    
    float rotencionFuncion()
    {
        float numeroRotencion = 45; //360 / 8 que e a contidade de totação deste objeto

        float rotecionAtual = numeroRotencion * Slider.value-1; // a rotação que esta atuando neste momento

        return rotecionAtual;
    }

}
