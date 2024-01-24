using UnityEngine;

public class UiEnterection : MonoBehaviour
{
    Archibald playerScript;
    enterectionScript PaiScriopt;
    [SerializeField] Canvas canva;

    private void OnEnable()
    {
        PaiScriopt = GetComponentInParent<enterectionScript>();
        playerScript = PaiScriopt.PlayerObj.GetComponent<Archibald>();
        canva.worldCamera = playerScript.mainCamera;
    }

    public void EnigmaResolucion()
    {
        playerScript.resoveu = true;
        Destroy(PaiScriopt.gameObject);

    }
}
