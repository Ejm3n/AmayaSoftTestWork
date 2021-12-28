using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game mode", menuName = "Game mode")]
public class GameMode : ScriptableObject
{
    public Sprite GameModsAvatar;
    public string GameModsName;
    public Sprite[] GameModeSprites;
}
