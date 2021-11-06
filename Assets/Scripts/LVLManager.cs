using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LVLManager : MonoBehaviour
{
    public GameObject[] btnFases;
    public string x;
    
    void Start()
    {
        // x = PlayerPrefs.GetString("fase1");
        //
        // if (x == "passou")
        // {
        //     btnFases[1].SetActive(true);
        // }
    }
}