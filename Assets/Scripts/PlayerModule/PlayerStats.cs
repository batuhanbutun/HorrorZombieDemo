using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _health = 100f;




    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Health { get { return _health; } set { _health = value; } }

}
