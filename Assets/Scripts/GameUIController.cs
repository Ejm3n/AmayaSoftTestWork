using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Image _answerImage;
    [SerializeField] private CanvasGroup _gameCanvasGroup;
    [SerializeField] private CanvasGroup _endCanvasGroup;
    private PlayField _playField;

    private void Awake()
    {
        _playField = FindObjectOfType<PlayField>();
        ChangeStates(_endCanvasGroup, _gameCanvasGroup);
    }

    private void Update()
    {
        if (_playField.IsGameFinished())
        {
            ChangeStates(_gameCanvasGroup, _endCanvasGroup);
        }
        _answerImage.sprite = _playField.GetAnswerSprite();
    }

    /// <summary>
    /// изменить состояние канвас групп - одну скрыть, вторую включить
    /// </summary>
    /// <param name="whatOff"></param>
    /// <param name="whatOn"></param>
    private void ChangeStates(CanvasGroup whatOff, CanvasGroup whatOn)
    {
        ChangeCanvasGroup(whatOff, false);
        ChangeCanvasGroup(whatOn, true);
    }

    /// <summary>
    /// сделать видимым либо невидимым данный канвас груп
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="what"></param>
    private void ChangeCanvasGroup(CanvasGroup canvasGroup, bool what)
    {
        if (what)
            canvasGroup.alpha = 1;
        else
            canvasGroup.alpha = 0;
        canvasGroup.interactable = what;
        canvasGroup.blocksRaycasts = what;
    }
}
