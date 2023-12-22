using UnityEngine;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
    [SerializeField] spawn sapawn;
    private Archibald player;
    Image imagem;
    void Start()
    {
        player = sapawn.playerObj.GetComponent<Archibald>();
        imagem = player.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagem.fillAmount = (float)player.Maxlife / player.Life;
    }
}
