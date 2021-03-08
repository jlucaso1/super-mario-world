using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void RestartLevel()
    {
        StartCoroutine(GameOverCouroutine());
    }
    IEnumerator GameOverCouroutine()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Dead);
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
