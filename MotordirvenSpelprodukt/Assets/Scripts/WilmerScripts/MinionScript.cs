using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float CurrentHealth;
    public float MaxHealth;
    public float MovementSpeed;
    public float AttackSpeed;
    public float AttackRange;
    public float AttackCooldown;
    public float LastAttackTime;
    void Start()
    {
        //AttackCooldown = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        //Death animation
    }
    public void TakeDamage()
    {
        
    }
    public void DealDamage()
    {

    }
}
