using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Field
{
    [Header("тут должны быть поля в ширину и высоту")]
    [SerializeField] private int _height;
    [SerializeField] private int _width;

    [SerializeField] private GameObject[,] _clickCircles;//массив кружочков
    [SerializeField] private Vector2[,] _circlesPositions;//массив местоположений кружочков

    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }
    public GameObject[,] ClickCircles { get => _clickCircles; set => _clickCircles = value; }
    public Vector2[,] CirclesPositions { get => _circlesPositions; set => _circlesPositions = value; }
}

public class PlayField : MonoBehaviour
{
    [SerializeField] private GameObject _clickCirclePrefab;//префаб кружочка
    [SerializeField] private Field[] _levels;

    private GameMode _gameMode;//класс игровых режимов, который держит все данные о текущем игровом режиме 
    private bool _finished = false;
    private List<int> _usedAnswers = new List<int>();//лист использованных ответов
    private int _currentLevel = 0;//текущая сложность
    private int _answerPosx;//позиция ответа по ширине
    private int _answerPosy;//позиция ответа по высоте
    private int _answer;//ответ который хранит значение
    private Sprite _spriteAnswer;

    /// <summary>
    /// все чистим, запускаем процесс создания следующей сложности
    /// </summary>
    private void Start()
    {
        _usedAnswers.Clear();
        _gameMode = Resources.Load<GameMode>("ScriptableObjects/GameMods/" + PlayerPrefs.GetString("Gamemode"));
        _clickCirclePrefab = Resources.Load<GameObject>("Prefabs/ClickCircle");
        NextLevel();
    }

    /// <summary>
    /// переход к следующей сложности
    /// </summary>
    public void NextLevel()
    {
        if (_currentLevel < _levels.Length)
        {
            ClearField();            
            _levels[_currentLevel].ClickCircles = new GameObject[_levels[_currentLevel ].Width, _levels[_currentLevel].Height];
            _levels[_currentLevel].CirclesPositions = new Vector2[_levels[_currentLevel].Width, _levels[_currentLevel].Height];           
            RandomizeCorrectAnswer();
            GetCirclePositions();
            Initialise_Field();
            Debug.Log(_currentLevel);
            _currentLevel++;
        }
        else
        {
            _finished = true;
        }
    }

    /// <summary>
    /// возврат значения окончена ли игра
    /// </summary>
    /// <returns></returns>
    public bool IsGameFinished()
    {
        return _finished;
    }
    /// <summary>
    /// возвращает спрайт правильного ответа
    /// </summary>
    /// <returns></returns>
    public Sprite GetAnswerSprite()
    {
        return _spriteAnswer;
    }

    /// <summary>
    /// очистка поля
    /// </summary>
    private void ClearField()
    {
        if (_currentLevel>0)
        {
            foreach (GameObject clickCircle in _levels[_currentLevel-1].ClickCircles)
            {
                Destroy(clickCircle);
            }
        }                
    }

    /// <summary>
    /// заготавливаем места для круглешков
    /// </summary>
    private void GetCirclePositions()
    {
        float heightPos = 0;
        float widthPos = 0;
        if (_levels[_currentLevel].Height > 1)
        {
            heightPos = -1 * ((_levels[_currentLevel].Height / 2) - 0.5f);
        }
        for (int y = 0; y < _levels[_currentLevel].Height; y++)
        {
            if (_levels[_currentLevel].Width > 1)
            {
                widthPos = -1 * ((_levels[_currentLevel].Width / 2) - 0.5f);
            }
            for (int x = 0; x < _levels[_currentLevel].Width; x++)
            {
                _levels[_currentLevel].CirclesPositions[x, y] = new Vector2(widthPos, heightPos);

                if (_levels[_currentLevel].Width != 1)
                    widthPos += 1;
            }
            heightPos += 1;
        }
    }

    /// <summary>
    /// создаем игровое поле, помещаем все на заготовленные позиции,
    /// берем спрайты, имя искомого нами спрайта, помещаем все данные в класс круглешков,
    /// ставим правильный ответ на заготовленное для него место
    /// </summary>
    private void Initialise_Field()
    {
        List<int> numbers = new List<int>();
        numbers.Add(_usedAnswers.Last());
        for (int x = 0; x < _levels[_currentLevel].Width; x++)
        {
            for (int y = 0; y < _levels[_currentLevel].Height; y++)
            {
                int randomedNum = Random.Range(0, _gameMode.GameModeSprites.Length);
                while (numbers.Contains(randomedNum))
                {
                    randomedNum = Random.Range(0, _gameMode.GameModeSprites.Length);
                }
                numbers.Add(randomedNum);
                _levels[_currentLevel].ClickCircles[x, y] = Instantiate(_clickCirclePrefab, new Vector2(_levels[_currentLevel].CirclesPositions[x, y].x * 2+transform.position.x,
                    _levels[_currentLevel].CirclesPositions[x, y].y * 2+ transform.position.y), Quaternion.identity, transform);                
                if (y == _answerPosy && x == _answerPosx)
                {
                    _levels[_currentLevel].ClickCircles[x, y].GetComponent<ClickCircle>().CorrectAnswer = true;
                    _levels[_currentLevel].ClickCircles[x, y].GetComponent<ClickCircle>().ChangeChildSprite(_gameMode.GameModeSprites[numbers[0]]);
                }
                else
                {
                    _levels[_currentLevel].ClickCircles[x, y].GetComponent<ClickCircle>().ChangeChildSprite(_gameMode.GameModeSprites[randomedNum]);
                }                                  
                   PopThem(x, y);                
            }
        }
    }

    /// <summary>
    /// создание эффекта bounce на все созданные круглешки
    /// </summary>
    private void PopThem(int x, int y)
    {
        _levels[_currentLevel].ClickCircles[x, y].transform.DOShakePosition(0.3f, strength: new Vector3(0.1f, 0.1f, 0),
            vibrato: 5, randomness: 5, snapping: false, fadeOut: true);

        _levels[_currentLevel].ClickCircles[x, y].transform.DOScale(0.1f, 1f);       
        _levels[_currentLevel].ClickCircles[x, y].transform.DOScale(0.7f, 1f);

    }

    /// <summary>
    /// рандомим правильный ответ, в каком месте он будет стоять
    /// </summary>
    private void RandomizeCorrectAnswer()
    {
        _answerPosx = Random.Range(0, _levels[_currentLevel].Width);
        _answerPosy = Random.Range(0, _levels[_currentLevel].Height);
        while (_usedAnswers.Contains(_answer))
        {
            _answer = Random.Range(0, _gameMode.GameModeSprites.Length);
            
        }
        _spriteAnswer = _gameMode.GameModeSprites[_answer];
        _usedAnswers.Add(_answer);
    }
}
