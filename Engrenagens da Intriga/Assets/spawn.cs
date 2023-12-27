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

    //[Header("ui")]
    //[SerializeField] GameObject[] UiLife;
    //[SerializeField] GameObject[] UiImage;

    [Header("public")]
    public GameObject playerObj;

    [Header("imagems")]
    [SerializeField] Image[] lifeSprite;
    [SerializeField] Image[] fotoSprite;

    private int perso;

    private void Awake()
    {
        //spanando o buneco
        perso = PlayerPrefs.GetInt("Player" + player)-1;
        playerObj = Instantiate(prefeb[perso],transform.position,transform.rotation);
        
        //playerObj = Instantiate(prefeb[perso]);
        //pegando componete

        // lifeSprite = UiLife[perso].GetComponent<Image>();
        // fotoSprite = UiImage[perso].GetComponent<Image>();

        //colocando imagem
        lifeSprite[perso].sprite = imagemsUi[perso];
        fotoSprite[perso].sprite = artUi[perso];     
    }
}
