using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAnimationMarco : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    private string _currentCombo = ""; //
    private float _desiredWeight = 0;
    private float _weight = 0;
    private float _weightChanger = -0.025f;
    private float _inputTimer = 0;
    public float _timeBetweenInputs = 0f;


    private void Awake()
    {
        
    }
    void Start()
    {
        _player = GetComponent<Player>();
        _player.OnAttackPressed += Player_OnAttack;

        _inputTimer = _timeBetweenInputs;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        HandleInput(e.attackType1);
    }

    private void HandleInput(Player.OnAttackPressedEventArgs.AttackType attackType)
    {
        // If combat layer is disabled, allow new input.

        if (_inputTimer >= _timeBetweenInputs)
        {
            switch (attackType)
            {
                case Player.OnAttackPressedEventArgs.AttackType.Light:
                    _currentCombo += "L";
                    break;
                case Player.OnAttackPressedEventArgs.AttackType.Heavy:
                    _currentCombo += "H";
                    break;
                default:
                    break;
            }
            _animator.SetTrigger("Trigger_" + _currentCombo);
            _inputTimer = 0;
        }
    }

    private void Update()
    {
        _inputTimer += Time.deltaTime;

        HandleAnimationLayers();
    }

    private void HandleAnimationLayers()
    {
        if (_weight != _desiredWeight)
        {
            _weight -= (1 * _weightChanger);

            if (_weight < 0f)
            {
                _weight = 0.01f;
            }
            else if (_weight > 1f)
            {
                _weight = 0.99f;
            }

            _animator.SetLayerWeight(_animator.GetLayerIndex("CombatTree"), _weight);
        }
    }

    public void DisableCombatLayer()
    {
        Debug.Log("DisableCombatLayer : Combo=" + _currentCombo);
        _desiredWeight = 0.01f;
        _weightChanger = 0.025f;
        _weight = 0.99f;
    }

    public void EnableCombatLayer()
    {
        Debug.Log("EnableCombatLayer: Combo=" + _currentCombo);
        _desiredWeight = 0.99f;
        _weightChanger = -0.025f;
        _weight = 0.01f;
    }
}
