using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int dealDamage = 100;

    public int GetDamage()
    {
        return dealDamage;
    } 

    public void Hit()
    {
        Destroy(gameObject);
    }
}
