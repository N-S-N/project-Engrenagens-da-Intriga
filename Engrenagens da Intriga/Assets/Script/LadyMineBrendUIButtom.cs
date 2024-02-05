using UnityEngine;

public class LadyMineBrendUIButtom : MonoBehaviour
{
    [Header("Buttom")]
    [SerializeField] int Corespondente;

    [Header("scripts")]
    [SerializeField] LadyUiManeger lady;

    public void ADDNumber()
    {
        if (lady.Decenas >= 11111111) return;
        if(lady.CauntCorretsInfo == 0)
        {
            lady.CauntCorretsInfo += Corespondente;
        }
        else
        {
            lady.CauntCorretsInfo = (10 * lady.CauntCorretsInfo) + Corespondente;
        }
    }
}
