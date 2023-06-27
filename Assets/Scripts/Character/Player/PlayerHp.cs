using System;

public class PlayerHp : Hp, IHp
{
    public static Action GameOver;
    public uint Hp { get; set; }
    protected override void Awake()
    {
        UiParametrs.UpdateHp += Death;
    }
    protected override void Start()
    {
        Hp = 100;
        UiParametrs.UpdateHp(Hp);
    }
    public void Death(uint hp) 
    {
        if(hp == 0 || Hp > 4294960000)
        GameManager.GameOver.Invoke();
    }
    private void OnDestroy()
    {
        UiParametrs.UpdateHp -= Death;
    }

}
