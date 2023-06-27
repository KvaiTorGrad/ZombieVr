using System;
using TMPro;
using UnityEngine;
public class UiParametrs : MonoBehaviour
{
   [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _booletText;

    public static Action<uint> UpdateHp;
    public static Action<uint> UpdateBoolet;

    private void Awake()
    {
        UpdateHp += InvokeHp;
        UpdateBoolet += InvokeCountBullet;
    }
    private void InvokeHp(uint hp)
    {
        _hpText.text = $"Hp: {hp}";
    }
    private void InvokeCountBullet(uint countBullets)
    {
        _booletText.text = $"Bullets: {countBullets}";
    }
    private void Update()
    {
        if(CardboardReticlePointer.hit.collider != null && !GameManager.IsGameStart || GameManager.IsGameOver)
        new InitRayCast(CardboardReticlePointer.hit); 
    }
    private void OnDestroy()
    {
        UpdateBoolet -= InvokeCountBullet;
        UpdateHp -= InvokeHp;
    }
}
