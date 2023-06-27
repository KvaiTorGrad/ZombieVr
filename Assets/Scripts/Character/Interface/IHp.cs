using UnityEngine;
using UnityEngine.AI;

public interface IHp
{
    public abstract uint Hp { get; set; }
    public virtual void Death() { }
}

public interface IMovement
{
    public abstract uint Speed { get; set; }
    public abstract void Move();
    public virtual void Rotate() { }
}
public interface IAttack
{
    public abstract void Attack();
}
public interface IHandAttack : IAttack
{
    public abstract uint Damage { get; set; }
}
public interface IShooting : IAttack
{
    public abstract bool EmtyBoolets { get; set; }
    public abstract uint RayDistance { get; set; }
    public abstract uint MaxBoollets { get; set; }
    public abstract uint Boollets { get; set; }
    public bool IsShoot { get; set; }
    public abstract bool IsReloadBoolets { get; set; }
    public abstract ParticleSystem Particle { get; set; }
    public abstract Animator Animator { get; set; }
    public abstract AudioSource AudioSource { get; set; }
    public abstract void StartReloadBollets();
    public abstract void EndReloadBooletsEventAnimation();
}
public interface IAIController
{
    public abstract NavMeshAgent Agent { get;  set; }
    public Animator Animator { get; set; }
    public Transform Target { get; set; }
}