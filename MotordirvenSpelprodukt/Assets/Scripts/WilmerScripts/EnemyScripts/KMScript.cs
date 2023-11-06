using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMScript : EnemyScript
{

    public Animator Anim;
    public float ChaseDistance;


    [Header("Attack")]
    [SerializeField] private Collider expolisionHitbox;
    [SerializeField] public float _diveRange;
    public ParticleSystem _explosion;
    [SerializeField] public bool PlayerInpact;

    protected override void Start()
    {
        base.Start();
        expolisionHitbox.enabled = false;
        _movementSpeed = 5; //Ramp up speed kan testas
        _attackRange = 0.2f; //?? kanske inte  beh�vs
        _timeBetweenAttacks = 2; //Kanske inte beh�vs
        ChaseDistance = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateExpolsion()
    {
        
        expolisionHitbox.enabled = true;
        
    }
    public void ExplodeDie()
    {

        if (!PlayerInpact)
        {
            expolisionHitbox.enabled = true;
            Instantiate(_explosion, transform);
            //hpmanger.TakeDamage(100);
        }
        
        
    }
}
