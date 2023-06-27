using UnityEngine;

[CreateAssetMenu(menuName = "State/DeadState")]
public class DeadState : State
{
    public override void Run()
    {
        if (IsFinished) return;
        Deth();
    }
    public void Deth()
    {
        if (Hp.Hp == 0 || Hp.Hp > 4294960000)
            Hp.Death();
    }

}
