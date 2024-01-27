using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public bool play = false;
    public bool howto = false;
    public bool quit = false;
    public bool back = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OnMouseEnter()
        {
            Debug.Log("Ping!");
        }
    }

    public void Play() { Application.LoadLevel(1); }

    public void HowTo() { Application.LoadLevel(2); }

    public void Quit() { Application.Quit(); }

    public void Back() { Application.LoadLevel(0); }

    public void OnMouseUp()
{
    if (play)
    {
            Debug.Log("Ping!");
        Application.LoadLevel(1);
    }

    if (howto)
    {
        Application.LoadLevel(2);
    }

    if (quit)
    {
        Application.Quit();
    }

    if (back)
        {
            Application.LoadLevel(0);
        }

}
}
