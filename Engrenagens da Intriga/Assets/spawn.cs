using UnityEngine;
using UnityEngine.UIElements;

public class spawn : MonoBehaviour
{
    [Header("player")]
    [SerializeField] int player;

    [Header("personagems")]
    [SerializeField] GameObject[] prefeb;
    [SerializeField] Sprite[] imagemsUi;
    [SerializeField] Sprite[] artUi;

    [Header("ui")]
    [SerializeField] GameObject[] UiLife;
    [SerializeField] GameObject[] UiImage;


    [Header("public")]
    public GameObject playerObj;

    private Image lifeSprite;
    private Image fotoSprite;

    private int perso;

    private void Awake()
    {
        //spanando o buneco
        perso = PlayerPrefs.GetInt("Player" + player)-1;
        playerObj = Instantiate(prefeb[perso],transform.position,transform.rotation);
        //pegando componete
        lifeSprite = UiLife[perso].GetComponent<Image>();
        fotoSprite = UiImage[perso].GetComponent<Image>();
        //colocando imagem
        lifeSprite.sprite = imagemsUi[perso];
        fotoSprite.sprite = artUi[perso];     
    }
}
