using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        //mainMenu = GameObject.FindWithTag("MainMenu");
        mainMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
