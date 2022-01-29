using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/GameStats")]
public class GameStats : ScriptableObject
{
    [SerializeField] private bool _isGameOver = true;



    public bool isGameOver { get { return _isGameOver; } set { _isGameOver = value; } }




}
