using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoader : MonoBehaviour
{
    public void LoadSceneOption(){
        SceneManager.LoadScene("Option",LoadSceneMode.Additive);
    }
    public void UnloadScene(string scene){
        SceneManager.UnloadSceneAsync(scene);
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
