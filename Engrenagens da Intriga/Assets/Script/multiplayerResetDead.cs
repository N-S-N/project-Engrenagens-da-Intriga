using UnityEngine;
using UnityEngine.SceneManagement;


public class multiplayerResetDead : MonoBehaviour
{
    [SerializeField] GameObject _canvaDead;
    [SerializeField] GameObject _CanvaUi;
    private void Awake()
    {
        PlayerPrefs.SetInt("deadPlayer",0);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("playerCont") == 0) return;
        if (PlayerPrefs.GetInt("playerCont") == PlayerPrefs.GetInt("deadPlayer"))
        {
            _CanvaUi.SetActive(false);
            _canvaDead.SetActive(true);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
