using UnityEngine;

public class escadaScript : MonoBehaviour
{
    [SerializeField] GameObject _fim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _fim.SetActive(true);


    }


}
