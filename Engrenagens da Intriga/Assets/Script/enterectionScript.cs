using UnityEngine;

public class enterectionScript : MonoBehaviour
{
    [Header("tipo de enteração\n 0 = Archibald\n 1 = Victoria \n 2= Seraphina")]
    [SerializeField] int tipo; //esse e o tipo de enteracao
    [Header("OBJ")]
    [SerializeField]public GameObject PlayerObj; //vai receber o player
    [SerializeField] GameObject UiObjEnterection; //obj Da Ui

    private void Update()
    {
        if (PlayerObj)
        {
            
            UiObjEnterection.SetActive(true);
        }
    }
}
