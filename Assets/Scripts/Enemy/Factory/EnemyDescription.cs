using UnityEngine;
using Random = UnityEngine.Random;
using System;

[Serializable]
public class EnemyDescription
{
    [SerializeField] private string _species;
    [Space(2)]
    [SerializeField] private int _forceMin;
    [SerializeField] private int _forceMax;
    [Space(2)]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [Space(2)]
    [SerializeField] private float _minHp;
    [SerializeField] private float _maxHp;

    public float Speed() => Random.Range(_minSpeed, _maxSpeed);
    public float Hp() => Random.Range(_minHp, _maxHp);
    public int Force() => Random.Range(_forceMin, _forceMax);
}
