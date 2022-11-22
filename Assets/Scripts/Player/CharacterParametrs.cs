using UnityEngine;

public abstract class CharacterParametrs : MonoBehaviour, ICharactrerParametrs
{
    private float _speed;
    private float _hp;
    private float _maxhp;
    private int _damage;
    private int _countActiveBullet;
    private int _countAllBullet;
    private float _speedShooting;
    private int _weapoValue;
    private int _maxBullet;
    private Rigidbody _rb;
    private CapsuleCollider _capsuleCollider;
    private Animator _anim;
    private AudioSource _source;
    private PlayerInputSystem _playerInputSystem;
    private ParticleSystem _particleSystem;
    private AudioClip[] _audioClip;
    private GameObject[] _weapons;

    public virtual float Speed { get => _speed; set => _speed = value; }
    public virtual float Hp { get => _hp; set => _hp = value; }
    public virtual float MaxHp { get => _maxhp; set => _maxhp = value; }
    public virtual int Damage { get => _damage; set => _damage = value; }
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value; }
    public int WeapoValue { get => _weapoValue; set => _weapoValue = value; }
    public float SpeedShooting { get => _speedShooting; set => _speedShooting = value; }
    public int CountActiveBullet { get => _countActiveBullet; set => _countActiveBullet = value; }
    public int CountAllBullet { get => _countAllBullet; set => _countAllBullet = value; }

    protected virtual Rigidbody Rb { get => _rb; private set => _rb = value; }
    protected virtual CapsuleCollider CapsuleCollider { get => _capsuleCollider; private set => _capsuleCollider = value; }
    public virtual Animator Anim { get => _anim; set => _anim = value; }
    public virtual AudioSource Source { get => _source; set => _source = value; }
    public virtual AudioClip[] ClipAudio { get => _audioClip; set => _audioClip = value; }

    protected virtual PlayerInputSystem PlayerInputSystem { get => _playerInputSystem; private set => _playerInputSystem = value; }
    public virtual ParticleSystem ParticleSystem { get => _particleSystem; set => _particleSystem = value; }

    public GameObject[] Weapons { get => _weapons; set => _weapons = value; }

    public delegate void IsDeathEnemy(int enemy);
    public static event IsDeathEnemy DeadEnemy, CountDeadEnemy;

    public delegate void MainHp(float hp, float maxHp);
    public static event MainHp MainHpInit;

    public delegate void GameOver();
    public static event GameOver IsGameOver;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();
        PlayerInputSystem = new PlayerInputSystem();
        Weapons = new GameObject[2]
        {
            GameObject.FindGameObjectWithTag("Thomson"),
            GameObject.FindGameObjectWithTag("HandGun45")
        };
        ClipAudio = new AudioClip[3]
        {
           Resources.Load("SFX/Bullet/Body Head (Headshot) 4") as AudioClip,
           Resources.Load("SFX/Bullet/Wood Light 5") as AudioClip,
            Resources.Load("SFX/Bullet/03138 (mp3cut.net)") as AudioClip
        };
    }
    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void FixedUpdate() { }
    protected virtual void OnDisable() { }

    public virtual void ResetGame() { }

    protected virtual void AddEnemyKill(StateEnemy enemy)
    {
        DeadEnemy(enemy.IdEnemy);
        CountDeadEnemy(1);
    }
    protected virtual void TakingDamage(float damage)
    {
        Hp -= damage;
        MainHpInit(Hp, MaxHp);
        if (Hp <= 0)
            IsGameOver();
    }

}
