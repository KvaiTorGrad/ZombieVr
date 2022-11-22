using System.Collections;
using UnityEngine;

public class JoystickVrPlayer : CharacterParametrs
{

    private RaycastHit hit;
    private Weapon _weapon = new Weapon();

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponentInChildren<Animator>();
    }
    public void OnEnable()
    {
        PlayerInputSystem.Enable();
    }
    protected override void Start()
    {
        base.Start();
        ResetGame();

        PlayerInputSystem.Player.ChoiceWeaponsDown.performed += context => ChoiceWeapon(0);
        PlayerInputSystem.Player.ChoiceWeaponsUp.performed += context => ChoiceWeapon(1);
        PlayerInputSystem.Player.Shoot.started += context => StartCoroutine(Shooting());
    }

    public override void ResetGame()
    {
        base.ResetGame();
        Speed = 5f;
        MaxHp = 500f;
        Hp = MaxHp;
        CountAllBullet = 999999999;
        ChoiceWeapon(1);
        StateEnemy.DeadPlayer += TakingDamage;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 direction = PlayerInputSystem.Player.Move.ReadValue<Vector2>();
        Move(direction);
    }
    private void Move(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        Rb.velocity = moveDirection * Speed;
        Rb.AddForce(Vector3.down * 20, ForceMode.Force);
    }
    private IEnumerator Shooting()
    {

        if (WeapoValue == 1)
        {
            ParticleSystem.Play();
            while (PlayerInputSystem.Player.Shoot.IsPressed())
            {
                yield return new WaitForSeconds(SpeedShooting);
                Anim.SetBool("Shooter", PlayerInputSystem.Player.Shoot.IsPressed());
                FireBullet();
            }
            ParticleSystem.Stop();
        }
        else
        {
            yield return null;
            Anim.SetBool("Shooter", PlayerInputSystem.Player.Shoot.IsPressed());
            FireBullet();
        }

        yield break;
    }

    private void ChoiceWeapon(int idWeapon)
    {
        WeapoValue = idWeapon;
        switch (WeapoValue)
        {
            case 0:
                _weapon.Weaponry(new Gun());
                break;
            case 1:
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
        PlayerInputSystem.Player.Shoot.performed -= context => StartCoroutine(Shooting());
        PlayerInputSystem.Disable();
        StateEnemy.DeadPlayer -= TakingDamage;
    }

}




