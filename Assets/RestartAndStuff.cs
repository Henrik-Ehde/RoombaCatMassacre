using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAndStuff : MonoBehaviour
{
    void Update()
    {
        //To Main Menu
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");


        //Restart
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }


    }
}
