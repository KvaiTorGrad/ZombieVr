using System;
using System.Collections;
using UnityEngine;

public class Thomson : Weapon, IShooting
{
    public uint RayDistance { get; set; }
    public uint Damage { get; set; }
    public uint MaxBoollets { get; set; }
    public uint Boollets { get; set; }
    public ParticleSystem Particle { get; set; }
    public Animator Animator { get; set; }
    public AudioSource AudioSource { get; set; }
    public bool EmtyBoolets { get; set; }
    public bool IsShoot { get; set; }
    public bool IsReloadBoolets { get; set; }
    public static Action Shot;
    protected override void OnEnable()
    {
        Shot += StartShot;
        UiParametrs.UpdateBoolet(Boollets);
    }
    protected override void Start()
    {
        RayDistance = 15;
        Damage = 10;
        MaxBoollets = 30;
        Boollets = MaxBoollets;
        UiParametrs.UpdateBoolet(Boollets);
        Particle = GetComponentInChildren<ParticleSystem>();
        Animator = GetComponent<Animator>();
        AudioSource = GetComponentInChildren<AudioSource>();
    }
    public void Attack()
    {
            if (CardboardReticlePointer.hit.collider != null && !IsShoot && !IsReloadBoolets)
                new InitRayCast(CardboardReticlePointer.hit, Damage);
    }
    private void StartShot()
    {
        StartCoroutine(Shoting());
    }
    private IEnumerator Shoting()
    {
        IsShoot = true;
        if (!EmtyBoolets)
        {
            Boollets--;
            UiParametrs.UpdateBoolet(Boollets);
            Animator.SetTrigger("Shooter");
            Particle.Play();
            AudioSource.PlayOneShot(AudioSource.clip, 0.04f);
            IsEmptyBoolets();
        }
        else
            StartReloadBollets();
        yield return new WaitForSeconds(0.1f);
        IsShoot = false;
    }
    private void IsEmptyBoolets()
    {
        if (Boollets == 0)
            EmtyBoolets = true;
    }
    public void StartReloadBollets()
    {
        Animator.SetTrigger("Reload");
        IsReloadBoolets = true;
    }
    public void EndReloadBooletsEventAnimation()
    {
        Boollets = MaxBoollets;
        UiParametrs.UpdateBoolet(Boollets);
        EmtyBoolets = false;
        IsReloadBoolets = false;
    }
    private void OnDisable()
    {
        Shot -= StartShot;
    }
}