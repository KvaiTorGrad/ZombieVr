using UnityEngine;

public class InitRayCast
{
    public InitRayCast(RaycastHit hit, uint damage)
    {
        switch (LayerMask.LayerToName(hit.collider.gameObject.layer))
        {
            case "Head":
                Weapon.OnRaycastHit.Invoke(hit.collider.transform, damage);
                Thomson.Shot.Invoke();
                break;
            case "Body":
                Weapon.OnRaycastHit.Invoke(hit.collider.transform, damage - 5);
                Thomson.Shot.Invoke();
                break;
            case "Arm":
                Weapon.OnRaycastHit.Invoke(hit.collider.transform, damage - 8);
                Thomson.Shot.Invoke();
                break;
            case "Leg":
                Weapon.OnRaycastHit.Invoke(hit.collider.transform, damage - 8);
                Thomson.Shot.Invoke();
                break;
        }
    }
    public InitRayCast(RaycastHit hit)
    {
        if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "UI")
            GameManager.InitButton(hit.collider.name);
    }
}
