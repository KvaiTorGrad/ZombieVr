using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    private int _zombieWave;
    void Update()
    {
        if (GameManager.IsGameStart && !GameManager.IsGameOver)
        {
            if (InitCountZombie())
            {
                _zombieWave++;
                SpawnZombie(_zombieWave);
            }
        }
    }
    private bool InitCountZombie() => FindObjectsOfType<StateController>().Length == 0 ? true : false;

    private void SpawnZombie(int zombieToSpawn)
    {
        for (int i = 0; i < zombieToSpawn; i++)
        {
            Instantiate(_zombie, RandomPos(), RandomRotate());
        }

    }
    private Vector3 RandomPos() => new Vector3(Random.Range(0, 70f),0, Random.Range(0, 70f));
    private Quaternion RandomRotate() => Quaternion.Euler(0,Random.Range(0f, 360f),0);
}
