using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Animator BlurFade;
    [SerializeField]
    string sceneName1 = "Level1";
    [SerializeField]
    string sceneName2 = "Level2";
    [SerializeField]
    string sceneName3 = "Level3";
    [SerializeField]
    string sceneNameFree = "FreeRoam";
    [SerializeField]
    string sceneNameCredits = "CreditsScene";

    public void Loader1()
    {
        BlurFade.SetTrigger("End");
        Invoke("LoadScene1", 5);                             // loads onto the Level 1 scene
    }

    public void Loader2()
    {
        BlurFade.SetTrigger("End");
        Invoke("LoadScene2", 5);                             // loads onto the Level 2 scene
    }

    public void Loader3()
    {
        BlurFade.SetTrigger("End");
        Invoke("LoadScene3", 5);                             // loads onto the Level 3 scene
    }

    public void LoaderFree()
    {
        BlurFade.SetTrigger("End");
        Invoke("LoadSceneFreeRoam", 5);                      // loads onto the Free Roam scene
    }

    public void LoaderCredits()
    {
        BlurFade.SetTrigger("End");
        Invoke("LoadSceneCredits", 5);                      // loads onto the Credits scene
    }

    void LoadScene1()
    {
        SceneManager.LoadScene(sceneName1);                  // function onto the Level 1 scene
    }

    void LoadScene2()
    {
        SceneManager.LoadScene(sceneName2);                  // function onto the Level 2 scene
    }

    void LoadScene3()
    {
        SceneManager.LoadScene(sceneName3);                  // function onto the Level 3 scene
    }

    void LoadSceneFreeRoam()
    {
        SceneManager.LoadScene(sceneNameFree);               // function onto the tutorial scene
    }

    void LoadSceneCredits()
    {
        SceneManager.LoadScene(sceneNameCredits);               // function onto the tutorial scene
    }
}
