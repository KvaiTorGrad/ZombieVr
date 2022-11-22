using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NoJoystickVrPlayer : CharacterParametrs
{
    private Weapon _weapon = new Weapon();
    private RaycastHit hit;
    private NavMeshAgent _playerNavMesh;
    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponentInChildren<Animator>();
        _playerNavMesh = GetComponentInChildren<NavMeshAgent>();
    }
    protected override void Start()
    {
        base.Start();
        ResetGame();

        StartCoroutine(IsShooting());

    }
    public override void ResetGame()
    {
        base.ResetGame();
        _playerNavMesh.baseOffset = 1.6f;
        _playerNavMesh.speed = 5f;
        MaxHp = 500f;
        Hp = MaxHp;
        CountAllBullet = 999999999;
        ChoiceWeapon(0);
        StateEnemy.DeadPlayer += TakingDamage;
    }

    protected override void Update()
    {
        base.Update();
        var distanceToPoint = _playerNavMesh.remainingDistance;
        if (distanceToPoint < 4f)
            _playerNavMesh.SetDestination(GoToPoint());
    }

    private Vector3 GoToPoint() => new Vector3(RandomPositionPoint(), 1.6f, RandomPositionPoint());
    private int RandomPositionPoint() => Random.Range(20, 75);


    private IEnumerator IsShooting()
    {
        while (true)
        {
            ParticleSystem.Play();
            while (EnemyIndification.isEnemy)
            {
                Anim.SetBool("Shooter", true);
                FireBullet();
                yield return new WaitForSeconds(SpeedShooting);

            }
            Anim.SetBool("Shooter", false);
            ParticleSystem.Stop();
            yield return null;
        }
    }

    private void ChoiceWeapon(int idWeapon)
    {
        WeapoValue = idWeapon;
        switch (WeapoValue)
        {
            case 1:
                _weapon.Weaponry(new Gun());
                break;
            case 0:
                _weapon.Weaponry(new Automat());
                break;
        }
    }

    public void FireBullet()
    {
        if (CountActiveBullet > 0)
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            CountActiveBullet--;
            if (Physics.Raycast(rayOrigin, out hit, 10f, LayerMask.GetMask("Body")))
                ToGetShoot(Damage);
            else if (Physics.Raycast(rayOrigin, out hit, 10f, LayerMask.GetMask("Arm")))
                ToGetShoot(Damage / 2);
            else if (Physics.Raycast(rayOrigin, out hit, 10f, LayerMask.GetMask("Leg")))
                ToGetShoot(Damage / 2);
            else if (Physics.Raycast(rayOrigin, out hit, 10f, LayerMask.GetMask("Head")))
                ToGetShoot(Damage * 2);
            else if (Physics.Raycast(rayOrigin, out hit, 10f, LayerMask.GetMask("Wood")))
            {
                CreateParticleOnShootPoint("Wood");
                Source.PlayOneShot(ClipAudio[1]);
            }
            else
                Source.PlayOneShot(ClipAudio[2]);
        }
        else
            CountActiveBullet = MaxBullet;
    }

    private void ToGetShoot(float damage)
    {
        var enemy = hit.transform.GetComponentInParent<StateEnemy>();
        enemy.Hp -= damage;
        if (enemy.Hp <= 0 && enemy.IsDead == false)
        {
            base.AddEnemyKill(enemy);
        }
        Source.PlayOneShot(ClipAudio[0]);
        CreateParticleOnShootPoint("BulletVFX");
    }

    private void CreateParticleOnShootPoint(string nameParticle)
    {
        var particleBlood = Instantiate(Resources.Load(nameParticle), hit.point, hit.transform.rotation) as GameObject;
        particleBlood.transform.SetParent(hit.transform);
    }

    protected override void TakingDamage(float damage)
    {
        base.TakingDamage(damage);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerInputSystem.Player.Shoot.performed += context => StartCoroutine(IsShooting());
        PlayerInputSystem.Disable();
        StateEnemy.DeadPlayer -= TakingDamage;
    }

}
