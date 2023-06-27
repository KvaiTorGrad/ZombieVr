using UnityEngine;

public class EnemyAttack : Attack, IHandAttack
{
    public uint Damage { get; set; }
    private IAIController controller;
    protected override void Awake()
    {
        controller = GetComponentInParent<IAIController>();
    }
    protected override void Start()
    {
        Damage = 1;
    }
    public void Attack()
    {
        controller.Animator.SetTrigger("Attack");
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.TryGetComponent(out IHp hp) && other.transform.CompareTag("Player"))
        {
            hp.Hp -= Damage;
            UiParametrs.UpdateHp.Invoke(hp.Hp);
        }
    }
    protected override void DealDamage(Transform obj, uint damage)
    {
        base.DealDamage(obj, damage);
    }

}
