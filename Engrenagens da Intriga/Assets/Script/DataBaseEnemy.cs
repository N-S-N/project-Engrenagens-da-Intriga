using UnityEngine;

public class DataBaseEnemy : MonoBehaviour
{
    [Header("Banco De Dados")]
    [SerializeField] string[] _nome;
    //[SerializeField] int[] _ID;
    [SerializeField] int[] _idade;

    [Header("senha de carda setro na segunte odem " +
        "\r\n\t recepção " +
        "\r\n\t AlaMilitar " +
        "\r\n\t AlaMedicam " +
        "\r\n\t SetorTecnologico")]
    [SerializeField] int[] _senha;

    [Header("informação crucisas")]
    [SerializeField] bool _cargoAleatorio;
    [SerializeField] public Cargo cargo;
    [SerializeField] public Setor setor;

    [Header("informação finais")]
    public string Nome;
    public int ID;
    public int Idade;
    public int Senha; 

    public enum Cargo
    {
        Auxiliar,
        assistente,
        Analista,
        supervisor,
        Gerente
    }

    public enum Setor
    {
        recepção,
        AlaMilitar,
        AlaMedica,
        SetorTecnologico
    }


    private void Start()
    {
        
        //sistema de aleotisasao
        #region aleotortiação cargo
        if (_cargoAleatorio)
        {
            int ram = Random.Range(0, 4);
            if(ram == 0)
            {
                cargo = Cargo.Auxiliar;
            }
            else if(ram == 1)
            {
                cargo = Cargo.assistente;
            }
            else if (ram == 2)
            {
                cargo = Cargo.Analista;
            }
            else
            {
                cargo = Cargo.supervisor;
            }
        }
        #endregion
        //Debug.Log(cargo.ToString());

        #region aleotortiação nome
        int ram1 = Random.Range(0, _nome.Length);
        Nome = _nome[ram1];

        #endregion

        #region aleotortiação ID
        //int ram2 = Random.Range(0, _ID.Length);
        //ID = _ID[ram1];
        int ram2 = Random.Range(0, 9999999);
        ID = ram2;
        #endregion

        #region aleotortiação idade
        int ram3 = Random.Range(0, _idade.Length);
        Idade = _idade[ram3];

        #endregion

        #region determinar a senha
        if (cargo == Cargo.Gerente)
        {
            Senha = _senha[(int)setor];
        }
        #endregion
    }
}
