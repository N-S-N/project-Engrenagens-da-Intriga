using UnityEngine;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
    [SerializeField] spawn2 sapawn;
    private Archibald player;
    [SerializeField] Image imagem;
    void Start()
    {
        player = sapawn.playerObj.GetComponent<Archibald>();
        //imagem = player.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagem.fillAmount = player.Maxlife / player.Life;
    }
}
