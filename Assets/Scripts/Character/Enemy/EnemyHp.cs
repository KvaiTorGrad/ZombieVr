using RootMotion.Dynamics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHp : Hp, IHp
{
    public uint Hp { get; set; }
    private PuppetMaster _puppetMaster;
    private StateController _stateController;
    private bool _isDeath;
    protected override void Awake()
    {
        _puppetMaster = GetComponentInChildren<PuppetMaster>();
        _stateController = GetComponent<StateController>();
    }
    protected override void Start()
    {
        Hp = 10;
    }
    public void Death()
    {
        if (!_isDeath)
            StartCoroutine(StartTimerDeath());
    }

    private IEnumerator StartTimerDeath()
    {
        _isDeath = true;
        _puppetMaster.state = PuppetMaster.State.Dead;
        _stateController.transform.GetComponent<CapsuleCollider>().enabled = false;
        _stateController.transform.GetComponentInChildren<AudioSource>().enabled = false;
        _stateController.transform.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(_stateController.gameObject);
    }
}
