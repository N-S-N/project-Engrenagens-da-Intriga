using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoriaUiButom : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] VictoriaUiManeger victoriaUiManeger;
    [SerializeField]public VictoriaUiButom[] victoriaUiButoms;

    [Header("valores")]
    [SerializeField] public bool Ativo;
    [SerializeField] public bool _taAtivado;
    [SerializeField] bool final;
    [SerializeField] bool _isRed;

    [Header("text")]
    [SerializeField] TMP_Text erro;

    [Header("sprite")]
    [SerializeField] Sprite[] vavula;
    public Image[] Canos;

    Image imagem;
    private void Start()
    {
        imagem = GetComponent<Image>();
    }

    void Update()
    {
        Invoke("atolizacao", 0.5F);
        Invoke("canosColor", 0.5F);
    }

    void atolizacao()
    {
        if (!Ativo)
        {
            for (int i = 0; i < victoriaUiButoms.Length; i++)
            {
                for (int j = 0;j < victoriaUiButoms[i].victoriaUiButoms.Length; j++) 
                {
                    if (victoriaUiButoms[i].victoriaUiButoms[j].Ativo)
                    {
                        victoriaUiButoms[i].Ativo = true;
                    }
                    else
                    {
                        victoriaUiButoms[i].Ativo = false;
                    }
                }
            }
        }
        else
        {
            if (_taAtivado)
            {
                for (int i = 0; i < victoriaUiButoms.Length; i++)
                {

                    victoriaUiButoms[i].Ativo = true;

                }
                if (final)
                {
                    erro.color = Color.green;
                    erro.text = "sucesso na sincronização";
                    victoriaUiManeger.EnigmaResolucion();
                }
                if (_isRed)
                {
                    erro.color = Color.red;
                    erro.text = "falha na sincronização";
                    victoriaUiManeger.Exit();
                }
            }
            else
            {
                

            }
        }
    }

    void canosColor()
    {
        if (Ativo)
        {
            if (_taAtivado)
            {
                for (int i = 0; i < Canos.Length; i++)
                {
                    Canos[i].color = Color.grey;
                }
            }
            else
            {
                for (int i = 0; i < Canos.Length; i++)
                {
                    for (int e = 0; e < victoriaUiButoms.Length; e++)
                    {
                        for (int c = 0; c < victoriaUiButoms[e].Canos.Length; c++) 
                        {
                            if (Canos[i] == victoriaUiButoms[e].Canos[c]) 
                            {
                                if (victoriaUiButoms[e].Ativo && victoriaUiButoms[e]._taAtivado)
                                {
                                    Canos[i].color = Color.grey;
                                }
                                else
                                {
                                    Canos[i].color = Color.white;
                                }
                            }
                        }
                    }
                }
            }
        }      
        else
        {
            for (int i = 0; i < Canos.Length; i++)
            {
                for (int e = 0; e < victoriaUiButoms.Length; e++)
                {
                    for (int c = 0; c < victoriaUiButoms[e].Canos.Length; c++)
                    {
                        if (Canos[i] == victoriaUiButoms[e].Canos[c])
                        {
                            if (victoriaUiButoms[e].Ativo && victoriaUiButoms[e]._taAtivado)
                            {
                                Canos[i].color = Color.grey;
                            }
                            else
                            {
                                Canos[i].color = Color.white;
                            }
                        }
                        
                    }
                }
            }
        }
    }

    public void ButtomActiveite()
    {
        if (_taAtivado)
        {
            _taAtivado = false;
            imagem.sprite = vavula[0];

        }
        else
        {
            _taAtivado = true;
            imagem.sprite = vavula[1];
            for (int i = 0; i < victoriaUiButoms.Length; i++)
            {
                victoriaUiButoms[i].Ativo = true;
            }
        }
    }

}
