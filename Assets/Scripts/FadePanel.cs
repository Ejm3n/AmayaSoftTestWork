using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadePanel : MonoBehaviour
{
    private Animator _fadeAnimator;

    private void Awake()
    {
        _fadeAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Затухание и загрузка
    /// </summary>
    public void FadeOut(int level)
    {
        _fadeAnimator.SetTrigger("FadeOut");
        StartCoroutine(LoadScene(level));
    }

    /// <summary>
    /// Затухание и рестарт
    /// </summary>
    public void RestartLevel()
    {
        _fadeAnimator.SetTrigger("FadeOut");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    /// <summary>
    /// Загрузка сцены
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadScene(int level)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }
}
