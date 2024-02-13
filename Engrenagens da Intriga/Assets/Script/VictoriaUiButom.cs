using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class VictoriaUiButom : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] VictoriaUiManeger victoriaUiManeger;
    [SerializeField] VictoriaUiButom[] victoriaUiButoms;

    [Header("valores")]
    [SerializeField] public bool Ativo;
    [SerializeField] bool _taAtivado;
    [SerializeField] bool final;
    [SerializeField] bool _isRed;

    [Header("text")]
    [SerializeField] TMP_Text erro;

    void Update()
    {
        Invoke("atolizacao", 0.5F);    
    }

    void atolizacao()
    {
        if (!Ativo)
        {
            for (int i = 0; i < victoriaUiButoms.Length; i++)
            {
                victoriaUiButoms[i].Ativo = false;
            }
        }
        else
        {
            if (_taAtivado)
            {
                for (int i = 0; i < victoriaUiButoms.Length; i++)
                {
                    if (victoriaUiButoms[i].Ativo == false)
                    {
                        victoriaUiButoms[i].Ativo = true;
                    }
                }
                if (final)
                {
                    erro.color = Color.gray;
                    erro.text = "sucesso na sincronização";
                    Invoke("victoriaUiManeger.EnigmaResolucion", 1f);
                }
                if (_isRed)
                {
                    erro.color = Color.red;
                    erro.text = "falha na sincronização";
                    Invoke("victoriaUiManeger.Exit", 1f);
                }
            }
        }
    }

    public void ButtomActiveite()
    {
        if (_taAtivado)
        {
            for (int i = 0; i < victoriaUiButoms.Length; i++)
            {
                victoriaUiButoms[i].Ativo = false;
            }
            _taAtivado = false;
        }
        else
        {
            _taAtivado = true;
        }
    }

}
