using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class selecao : MonoBehaviour
{
    [Header("player\n do 1 ao 4")]
    [SerializeField] int player;
    [Header("0 = nenhum buneco\n 1 = Archibald \n 2 = Victoria  \n 3 = Capitão \n 4 = Lady ")]
    public int bunecoSelecinado = 0;  
    [Header("imagem & texto")]
    [SerializeField] Sprite[] imagem;
    [SerializeField] string[] nomeBuneco;
    [SerializeField] TMP_Text neme;
    [SerializeField] Image imagemselection;
    [Header("scripts")]
    [SerializeField] selecao[] seler;
    [Header("butom")]
    [SerializeField] Button buton;
    [SerializeField] Button cima;
    [SerializeField] Button baixo;

    private int boneco = 1;

    private void Update()
    {
        if (bunecoSelecinado != 0)
        {
            buton.interactable = false;
            return;
        }
        //nome e imagem
        neme.text = nomeBuneco[boneco-1];
        
        imagemselection.sprite = imagem[boneco - 1];
        
        //ativar e desativar a selecao
        int a = seler.Length;
        int b = 0;
        for (int i = 0; i < seler.Length; i++)
        {
            if (boneco == seler[i].bunecoSelecinado)
            {
                if (buton != null)
                {
                    buton.interactable = false;
                }
                b = 0;
            }
            else
            {
                b++;
                if (a <= b)
                {
                    if (buton != null)
                    { 
                        if (!buton.interactable)
                        {
                            buton.interactable = true;
                        }
                    }
                }
            }
        }
    }

    public void selecionou()
    {
        int a = seler.Length;
        int b = 0;
        for (int i = 0; i < seler.Length; i++)
        {
            if (boneco != seler[i].bunecoSelecinado)
            {
                b++;
                if (a == b) {
                    bunecoSelecinado = boneco;
                    PlayerPrefs.SetInt("Player"+player, bunecoSelecinado);
                    buton.interactable = false;
                    cima.interactable = false;
                    baixo.interactable = false;
                }
            }
        }
    }

    public void setacima()
    {
        if (boneco != 4) 
        {
            boneco++;
        }
        else
        {
            boneco = 1;
        }
    }

    public void setabaixo()
    {
        if (boneco != 1)
        {
            boneco--;
        }
        else
        {
            boneco = 4;
        }
    }
}
