using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntertainmentManager : MonoBehaviour
{
    //For Testing
    public TextMeshProUGUI EntertainmentText;
    public TextMeshProUGUI CrowdText;
    public GameObject OOCPopUp;


    [SerializeField] float _entertainmentPoints;
    public float GetETP()
    {
        return _entertainmentPoints;
    }


    [Header("UI Arrow")]
    [SerializeField] private Transform _indicatorArrow;
    private float _indicatorArrowrRotateAngle;


    //ETP = Entartainment Points

    [SerializeField] private float _maxETP = 100;
    private float _ETPThreshold;
    public float GetETPThreshold()
    {
        return _ETPThreshold;
    }
    private float _startETP;
    private float _currentThreshold;

   



    [Header("OOC- Out Of Combat")]
    public GameObject[] EnemyGameObjects;
    public GameObject PlayerCharacter;
    [SerializeField] [Range(0, 10)] float _scanEnemyArea;
    [SerializeField] float _timeOutOfCombatCounter = 0;
    [SerializeField] float _timeOutOfCombatThreshold;


    [Header("Conditions")]

    [SerializeField] private bool _isOutOfCombat; //OOC
   // [SerializeField] private bool _startComboWindowTimer;
    

    void Start()
    {

        EnemyGameObjects = GameObject.FindGameObjectsWithTag("EnemyTesting");



        //ETP
        _startETP = _maxETP / 2;
        _ETPThreshold = _maxETP / 2;
        _entertainmentPoints = _startETP;
        //_indicatorArrowrRotateAngle = 90;

        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateETPArrow();
        _indicatorArrowrRotateAngle = Mathf.Clamp(_indicatorArrowrRotateAngle, 0, 180);

        CheckIfOutOfCombat();

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    _isOutOfCombat = !_isOutOfCombat;
        //}

        if (_isOutOfCombat)
        {
            OutOfCombatDecreaseOverTime();
        }

        if(_entertainmentPoints < _ETPThreshold)
        {
            CrowdText.text = "Booooooo!!";
        }
        else
        {
            CrowdText.text = "Let's GOOOOO!!";
            CrowdText.color = Color.black;
        }

        //_indicatorArrowrRotateAngle = (180 / _maxETP) * _entertainmentPoints;

        _entertainmentPoints =  Mathf.Clamp(_entertainmentPoints, 0, _maxETP);
        EntertainmentText.text = "ETP: " + Mathf.Round(_entertainmentPoints).ToString();



        //For testing
        OOCPopUp.SetActive(_isOutOfCombat);


    }

    void UpdateETPArrow()
    {
        _indicatorArrowrRotateAngle = 180 + (-180 / _maxETP) * _entertainmentPoints;
        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);
    }

    void CheckIfOutOfCombat()
    {
        //Scan for enemies

        foreach (GameObject enemies in EnemyGameObjects)
        {
            float dist = Vector3.Distance(enemies.transform.position, PlayerCharacter.transform.position);
            if (dist > _scanEnemyArea)
            {
                _timeOutOfCombatCounter += Time.deltaTime;

                if (_timeOutOfCombatCounter >= _timeOutOfCombatThreshold)
                {
                    Debug.Log("Out of Combat");
                    _isOutOfCombat = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q)) //"Switch _isOutOfCombat to false when attacking is detected, Placeholder for now"
                {
                    _isOutOfCombat = false;

                }
                _timeOutOfCombatCounter = 0;
            }
        }
    }

    void OutOfCombatDecreaseOverTime()
    {
        //_indicatorArrowrRotateAngle = Mathf.Clamp(_indicatorArrowrRotateAngle, 0, 180);

        //_indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);

        _entertainmentPoints -= (Time.deltaTime); //Every second ETP -1

    }

    public void DecreseETP(int amoutToDecrese)
    {
        _entertainmentPoints -= amoutToDecrese;
    }
    public void increaseETP(int amoutToIncrease)
    {
        _entertainmentPoints += amoutToIncrease;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerCharacter.transform.position, _scanEnemyArea);
        
    }
}
