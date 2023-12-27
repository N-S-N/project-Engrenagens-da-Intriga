using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecitiomMenuStart : MonoBehaviour
{

    void Update()
    {
        if(PlayerPrefs.GetInt("selectionmenu") >= 4)
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
