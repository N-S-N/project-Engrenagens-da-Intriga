using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecitiomMenuStart : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("selectionmenu", 0);
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("selectionmenu") >= 4)
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
