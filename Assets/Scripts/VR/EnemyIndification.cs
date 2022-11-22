using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndification : MonoBehaviour
{
    public static bool isEnemy;
    public void OnPointerEnter() => isEnemy = true;

    public void OnPointerExit()=> isEnemy = false;
}
