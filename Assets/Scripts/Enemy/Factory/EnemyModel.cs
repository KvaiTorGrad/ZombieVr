using UnityEngine;

public class EnemyModel
{
    private EnemyDescription _description;
    public EnemyModel(EnemyDescription description)
    {
        _description = description;
    }

    public float SetSpeed => _description.Speed();
    public float SetHp => _description.Hp();
    public int SetForce => _description.Force(); 

}
