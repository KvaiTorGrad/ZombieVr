using System;
using TMPro;
using UnityEngine;

public class Weapon : Attack
{
    private bool _isTarget;
    [SerializeField] private TMP_Text _text;
    public TMP_Text Text { get => _text; set => _text = value; }
    public bool IsTarget { get => _isTarget; set => _isTarget = value; }
    public static Action<Transform, uint> OnRaycastHit;
    protected override void Awake()
    {
        OnRaycastHit += DealDamage;
    }
    protected override void DealDamage(Transform obj, uint damage)
    {
        var hp = obj.GetComponentInParent<IHp>();
            hp.Hp -= damage;
    }
    protected override void OnDestroy()
    {
        OnRaycastHit -= DealDamage;
    }

}
