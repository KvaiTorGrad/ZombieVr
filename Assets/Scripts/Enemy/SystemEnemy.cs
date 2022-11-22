using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class SystemEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private Transform _player;
    [SerializeField] private List<StateEnemy> _enemyModel;

    private void OnEnable()
    {
        AddEnemy();
        JoystickVrPlayer.DeadEnemy += DeathZombie;
    }
    private Vector3 RandomPosition => new Vector3(Random.Range(3f, 97f), 0, Random.Range(3f, 97f));
    public void Update()
    {
        MovingEnemy();
        DistanceToPlayer();
    }
    private void MovingEnemy()
    {
        for (int i = 0; i < _enemyModel.Count; i++)
        {
            _enemyModel[i].NavMesh.SetDestination(new Vector3(_player.position.x, _enemyModel[i].transform.position.y, _player.position.z));
            AnimationEnemy(i);
        }
    }
    public void DistanceToPlayer()
    {
        for (int i = 0; i < _enemyModel.Count; i++)
        {
            if (!_enemyModel[i].IsDead)
            {
                if (_enemyModel[i].NavMesh.remainingDistance <= _enemyModel[i].NavMesh.stoppingDistance)
                {
                    _enemyModel[i].NavMesh.speed = 0f;
                    _enemyModel[i].Anim.SetBool("Attack", true);
                }
                else
                {
                    _enemyModel[i].NavMesh.speed = _enemyModel[i].Speed;
                    _enemyModel[i].Anim.SetBool("Attack", false);
                }
            }
        }
    }
    private void AnimationEnemy(int i)
    {
        var speed = Mathf.InverseLerp(0, 3f, _enemyModel[i].Speed);
        _enemyModel[i].Anim.SetFloat("Speed", speed, 3f, Time.deltaTime);
    }
    private void AddEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject zombieTransform = Instantiate(_zombie, RandomPosition, Quaternion.identity);
            _enemyModel.Add(zombieTransform.GetComponent<StateEnemy>());
            _enemyModel[i].IdEnemy = i;
        }
    }

    private void DeathZombie(int index)
    {
        _enemyModel[index].NavMesh.speed = 0;
        _enemyModel[index].IsDead = true;
        _enemyModel[index].Anim.SetBool("Death", true);
        _enemyModel[index].Source.PlayOneShot(_enemyModel[index].ClipAudio[0]) ;
        _enemyModel[index].PuppetMaster.state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        StartCoroutine(ResetEnemy(index));
    }

    private IEnumerator ResetEnemy(int index)
    {
        yield return new WaitForSecondsRealtime(5);
        RevivalEnemy(index);
    }
  private void RevivalEnemy(int index)
    {
        _enemyModel[index].transform.position = RandomPosition;
        _enemyModel[index].ResetGame();
    }

    private void OnDestroy()
    {
        JoystickVrPlayer.DeadEnemy -= DeathZombie;
    }

}


