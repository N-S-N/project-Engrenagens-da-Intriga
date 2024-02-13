using TMPro;
using UnityEngine;

public class enemyUiBaseInformacion : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] LadyUiManeger ladyUiManeger;
    
    [Header("OBJ")]
    [SerializeField] GameObject Enimy;
    [SerializeField] GameObject senhaOBJ;

    DataBaseEnemy dataBaseEnemy;

    [Header("Text")]
    [SerializeField] TMP_Text _nome;
    [SerializeField] TMP_Text _idade;
    [SerializeField] TMP_Text _ID;
    [SerializeField] TMP_Text _cargo;
    [SerializeField] TMP_Text _senha;
    [SerializeField] TMP_Text _setor;

    void Start()
    {
        Enimy = ladyUiManeger.Pai;
        dataBaseEnemy = Enimy.GetComponent<DataBaseEnemy>();
        if ((int)dataBaseEnemy.cargo == 4) senhaOBJ.SetActive(true) ;
        info();
    }

    void info()
    {
        _nome.text = dataBaseEnemy.Nome;
        _idade.text = dataBaseEnemy.Idade.ToString();
        _ID.text = dataBaseEnemy.ID.ToString();
        _cargo.text = dataBaseEnemy.cargo.ToString();
        _senha.text = dataBaseEnemy.Senha.ToString();
        _setor.text = dataBaseEnemy.setor.ToString();
    }


}
