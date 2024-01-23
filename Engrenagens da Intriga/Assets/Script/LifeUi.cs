using UnityEngine;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
    [SerializeField] Uimaneger Uimaneger;
    [SerializeField] int playerinfo;
    Image imagem;
    void Start()
    {
       // player = sapawn.playerObj.GetComponent<Archibald>();
        imagem = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagem.fillAmount = Uimaneger.life[playerinfo] / Uimaneger.maxlife[playerinfo];
        //Debug.Log("porcentagem " + Uimaneger.maxlife[playerinfo] / Uimaneger.life[playerinfo]);
        //Debug.Log("vida max " + Uimaneger.maxlife[playerinfo]);
        //Debug.Log("vida atual " + Uimaneger.life[playerinfo]);
    }
}
