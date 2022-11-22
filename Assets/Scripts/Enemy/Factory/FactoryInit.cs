using System;
using System.Collections.Generic;

public class FactoryInit
{
    private Dictionary<int, Func<int, EnemyModel>> mobFactory;

    public void Init(EnemyDescriptions descriptions)
    {
        mobFactory = new Dictionary<int, Func<int, EnemyModel>>()
        {
            {0, (level) => new EnemyModel(descriptions.ListZombie[level])}
        };
    }

    public EnemyModel CreateMobModel(int idMob, int level) => mobFactory[idMob](level);

}
