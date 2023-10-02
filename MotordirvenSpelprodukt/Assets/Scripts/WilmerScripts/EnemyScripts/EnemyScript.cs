using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamagable
{
    protected float _currentHealth;
    protected float _maxHealth;
    protected float _movementSpeed;
    protected float _attackSpeed; //?
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _lastAttackTime;
    protected float _stunDuration;
    protected bool _stunned;
    private ParticleSystem stunEffect;

    [Header("Dismembrent")]
    public DismemberentEnemyScript _dismembrentScript;

    public float Currenthealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float StunDuration { get { return _stunDuration; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
    public bool Stunned { get { return _stunned; } set { _stunned = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _dismembrentScript = GetComponent<DismemberentEnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(this.name + "Took damage from player");
       
        _currentHealth -= damage;
        Debug.LogWarning(_currentHealth);
        if (_currentHealth <= 0)
        {
            _dismembrentScript.GetKilled();
            Die();
        }
    }

    public void GetPushedBack(int damage)
    {
        //PushedBack
        //TakeDamage(damage);
    }

    public void TakeBleedDamage(int damage)
    {

    }
    public void GetSlowed(/*int damage*/)
    {

    }
    public void GetStunned(float stunSuration)
    {
        Stunned = true;
        _stunDuration = stunSuration;
       
    }

    public void Die()
    {

    }
}
