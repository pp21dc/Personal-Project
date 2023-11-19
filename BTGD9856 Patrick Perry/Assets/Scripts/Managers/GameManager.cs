/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
    
    private int list

    void Start()
    {
        *//*StartCoroutine("LoadLevel", levelNames[0]);*//*
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
        currentLevelName = levelName;

        //initialize player etc
        isLoading = false;
    }
}
*/