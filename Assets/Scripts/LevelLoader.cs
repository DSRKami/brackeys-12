using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }
    
    IEnumerator LoadLevelCoroutine(int level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }

}
