using UnityEngine;
using UnityEngine.EventSystems;

public class enterectionScript : MonoBehaviour
{
    [Header("tipo de enteração\n 0 = Archibald\n 1 = Victoria \n 2= Seraphina")]
    [SerializeField] int tipo; //esse e o tipo de enteracao
    [Header("OBJ")]
    [SerializeField] GameObject eventSystem;
    [SerializeField]public GameObject PlayerObj; //vai receber o player
    [SerializeField] GameObject UiObjEnterection; //obj Da Ui
    UiEnterection UiEnterection;

    bool espanou = false;

    private void Update()
    {
        if (PlayerObj && !espanou)
        {
            GameObject filho = Instantiate(UiObjEnterection,transform.position,transform.rotation, PlayerObj.transform);
            UiEnterection = filho.GetComponent<UiEnterection>();
            UiEnterection.Pai = gameObject;
            eventSystem.SetActive(false);
            espanou = true;
            Invoke("recarga",1f);
        }
    }

    void recarga()
    {
        PlayerObj = null;
        espanou = false;
    }

}
