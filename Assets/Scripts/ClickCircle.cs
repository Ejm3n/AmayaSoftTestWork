using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickCircle : MonoBehaviour
{
    public bool CorrectAnswer = false;
    private ParticleSystem _particles;
    private SpriteRenderer _childSprite;//ссылка на изображение дочернего объекта, меняется в скрипте PlayField
    private GameObject _childObj;//ссылка на дочерний игровой объект(спрайт)    

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        _childObj = transform.GetChild(0).gameObject;
        _childSprite = _childObj.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// назначить новый спрайт для дочернего объекта
    /// </summary>
    /// <param name="newSprite"></param>
    public void ChangeChildSprite(Sprite newSprite)
    {
        _childSprite.sprite = newSprite;
    }

    /// <summary>
    /// при нажатии на правильный ответ включает систему частиц
    /// </summary>
    private void OnMouseUp()
    {
        if (CorrectAnswer && !(EventSystem.current.IsPointerOverGameObject()))
        {
            StartCoroutine(Clicked());
            StartCoroutine(WaitTillNextLvl());
        }        
        else if(!(EventSystem.current.IsPointerOverGameObject()))
        {
            StartCoroutine(Clicked());
        }
    }

    /// <summary>
    /// при нажатии на объект трясется экран
    /// </summary>
    private IEnumerator Clicked()
    {
        _childObj.transform.DOShakePosition(0.3f, strength: new Vector3(0.1f, 0.1f, 0), vibrato: 5,randomness: 5, snapping: false, fadeOut: true);
        _childObj.transform.DOScale(0.1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _childObj.transform.DOScale(0.8f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _childObj.transform.DOScale(0.5f, 0.04f);
        yield break;
    }

    /// <summary>
    /// включение следующего уровня сложности
    /// </summary>
    private IEnumerator WaitTillNextLvl()
    {
        _particles.Play();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayField>().NextLevel();
    }
}
