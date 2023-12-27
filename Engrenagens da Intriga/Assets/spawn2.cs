using UnityEngine;
using UnityEngine.UI;

public class spawn2 : MonoBehaviour
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
    Archibald archibald;

    [Header("public")]
    public GameObject playerObj;

    [Header("imagems")]
    [SerializeField] Image[] lifeSprite;
    [SerializeField] Image[] fotoSprite;

    [Header("camera")]
    [SerializeField] Camera cam;

    private int perso;

    private void Awake()
    {
        //spanando o buneco
        perso = PlayerPrefs.GetInt("Player" + player) - 1;
        playerObj = Instantiate(prefeb[perso], transform.position, transform.rotation);

        //playerObj = Instantiate(prefeb[perso]);
        //pegando componete

        // lifeSprite = UiLife[perso].GetComponent<Image>();
        // fotoSprite = UiImage[perso].GetComponent<Image>();

        //colocando imagem
        for (int i = 0; i < lifeSprite.Length; i++)
        {
            lifeSprite[i].sprite = imagemsUi[perso];
            fotoSprite[i].sprite = artUi[perso];
        }
    }
    private void Start()
    {
        archibald = playerObj.GetComponent<Archibald>();
        archibald.mainCamera = cam;
    }
}
