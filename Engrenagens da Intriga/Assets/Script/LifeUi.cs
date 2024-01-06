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
        imagem.fillAmount = Uimaneger.maxlife[playerinfo] / Uimaneger.life[playerinfo];
    }
}
