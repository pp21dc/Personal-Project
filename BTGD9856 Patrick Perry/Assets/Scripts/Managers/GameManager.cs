using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            if (!instance)
            {
                Debug.Log("No game manager Present!!!");
            }
            return instance;
        }
    }


    [SerializeField]
    private string[] levelNames;

    private bool isLoading = false;
    private int currentLevel = 0;
    private string currentLevelName;

    private int list;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
  
    }
    void Start()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        StartCoroutine("LoadLevel", levelNames[0]);
    }

    private IEnumerator LoadLevel(string levelName)
    {
        isLoading = true;

        //uload current level scene


        //load next level scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        currentLevelName = levelName;

        //initialize player etc
        isLoading = false;

        player.transform.position = GameObject.Find("PlayerSpawn").transform.position;
    }

    private IEnumerator UnloadLevel(string levelName)
    {
        isLoading = true;

        //uload current level scene


        //load next level scene
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(levelName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //initialize player etc
        isLoading = false;
    }
    public void LevelComplete()
    {
        StartCoroutine(UnloadLevel(levelNames[currentLevel]));
        currentLevel++;
        if(currentLevel < levelNames.Length)
        {
            StartCoroutine(LoadLevel(levelNames[currentLevel]));
        }
    }
}
