public interface ICharactrerParametrs 
{
    public float Speed { get; set; }
    public float MaxHp { get; set; }
    public float Hp { get; set; }

    public int Damage { get; set; }

    public virtual void ResetGame() { }

}
