using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IHasProgress
{
    // Placeholder f�r nuvarande vapnets v�rden
    //-----------------------------------------
    float damage = 1;
    float range = 1;
    //-----------------------------------------

    public GameInput GameInput { get { return _gameInput; } }
    public event EventHandler OnChangeControllerTypeButtonPressed;
    public event EventHandler OnStartEvade;
    public event EventHandler OnDisableMovement;
    public event EventHandler OnEnableMovement;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public EventHandler<OnAttackPressedEventArgs> OnAttackPressed;
    public class OnAttackPressedEventArgs : EventArgs
    {
        public enum AttackType
        {
            Light,
            Heavy
        }
        public CurrentAttackSO CurrentAttackSO;
        public AttackType attackType;
        public Weapon weaponSO;
    }


    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _timeBetweenInputs = 0f;
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;
    [SerializeField] private Weapon _currentWeapon;

    [Header("Health settings")]
    [SerializeField] float _maxHealth;

    private float _currentHealth;
    private CurrentAttackSO _currentAttackSO;

    private PlayerDash _playerDash;

    private float _inputTimer = 0;

    private string _input;



    private void Awake()
    {
        _playerDash = GetComponent<PlayerDash>();
    }
    void Start()
    {
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;
        _gameInput.OnEvadeButtonPressed += GameInput_OnEvadeButtonPressed;

        _playerDash.EvadePerformed += PlayerDash_OnEvadePerformed;

        _currentHealth = _maxHealth;
        _input = "";

        InitializeHealth();

        Debug.Log("Health: " +  _currentHealth);
    }

    private void GameInput_OnEvadeButtonPressed(object sender, EventArgs e)
    {
 
        OnDisableMovement?.Invoke(this, EventArgs.Empty);
        
        OnStartEvade?.Invoke(this, EventArgs.Empty);
    }

    public void Knockbacked(object sender, EventArgs e)
    {

        OnDisableMovement?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerDash_OnEvadePerformed(object sender, EventArgs e)
    {
        OnEnableMovement?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
        if (_inputTimer >= _timeBetweenInputs)
        {
            _input += "L";
            if (GetCurrentAttackSO(_input) == null)
            {
                _input = "L";
            }
            OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs {CurrentAttackSO = GetCurrentAttackSO(_input), attackType = OnAttackPressedEventArgs.AttackType.Light, weaponSO = _currentWeapon });
            _inputTimer = 0;
        }
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
  

        if (_inputTimer >= _timeBetweenInputs)
        {
            _input += "H";

            if (GetCurrentAttackSO(_input) == null)
            {
                _input = "H";
            }

            OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs {CurrentAttackSO = GetCurrentAttackSO(_input), attackType = OnAttackPressedEventArgs.AttackType.Heavy, weaponSO = _currentWeapon });
            _inputTimer = 0;
        }
    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        OnChangeControllerTypeButtonPressed?.Invoke(this, e);
        TakeDamage();
    }

    void Update()
    {

    }

    private void InitializeHealth()
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = _currentHealth / _maxHealth });
    }
    private CurrentAttackSO GetCurrentAttackSO(string name)
    {
        foreach (CurrentAttackSO currentAttackSO in _AttackSOArray)
        {
            if (currentAttackSO.name.ToLower() == name.ToLower())
            {
                return currentAttackSO;
            }
        }
        return null;
    }


    //Will be replaced with the damagable interface method
    private void TakeDamage()
    {
        _currentHealth -= 20;
        Debug.Log("Health: " + _currentHealth);

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized =  _currentHealth / _maxHealth });
    }
}