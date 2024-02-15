using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecitiomMenuStart : MonoBehaviour
{
    [SerializeField] GameObject _StartUi;
    private void Awake()
    {
        PlayerPrefs.SetInt("selectionmenu", 0);
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("selectionmenu") >= 4)
        {
            _StartUi.SetActive(true);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
