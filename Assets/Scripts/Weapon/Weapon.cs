using UnityEngine;

class Weapon
{
    public void Weaponry(Firearms firearms)=> firearms.SetWeapon();
}

class Firearms : MonoBehaviour
{
    protected CharacterParametrs _characterParametrs;
    protected void GetWeapon() => _characterParametrs = FindObjectOfType<CharacterParametrs>();
    public virtual void SetWeapon()
    {
        GetWeapon();
    }
}

class Gun : Firearms
{
    public override void SetWeapon()
    {
        GetWeapon();
        _characterParametrs.MaxBullet = 15;
        _characterParametrs.CountActiveBullet = 15;
        _characterParametrs.Weapons[0].SetActive(false);
        _characterParametrs.Weapons[1].SetActive(true);
        _characterParametrs.Damage = 10;
        _characterParametrs.SpeedShooting = 2f;
        _characterParametrs.Anim = _characterParametrs.Weapons[1].GetComponent<Animator>();
        _characterParametrs.Source = _characterParametrs.Weapons[1].GetComponentInChildren<AudioSource>();
        _characterParametrs.ParticleSystem = _characterParametrs.Weapons[1].GetComponentInChildren<ParticleSystem>();

    }
}
class Automat : Firearms
{
    public override void SetWeapon()
    {
        GetWeapon();
        _characterParametrs.MaxBullet = 30;
        _characterParametrs.CountActiveBullet = 30;
        _characterParametrs.Weapons[0].SetActive(true);
        _characterParametrs.Weapons[1].SetActive(false);
        _characterParametrs.Damage = 25;
        _characterParametrs.SpeedShooting = 0.1f;
        _characterParametrs.Anim = _characterParametrs.Weapons[0].GetComponent<Animator>();
        _characterParametrs.Source = _characterParametrs.Weapons[0].GetComponentInChildren<AudioSource>();
        _characterParametrs.ParticleSystem = _characterParametrs.Weapons[0].GetComponentInChildren<ParticleSystem>();
    }
}