using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDescriptions", menuName = "EnemyDescriptions", order = 51)]
public class EnemyDescriptions : ScriptableObject
{
    [SerializeField] private List<EnemyDescription> _listZombie;

    public List<EnemyDescription> ListZombie => _listZombie;

}
