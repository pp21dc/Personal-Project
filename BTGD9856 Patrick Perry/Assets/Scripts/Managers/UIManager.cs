using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject DebugMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.Paused)
        {
            EnablePauseMenu();
        }
    }

    public void EnablePauseMenu()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadSceneAsync("Persistant Scene", LoadSceneMode.Additive);
    }
}
