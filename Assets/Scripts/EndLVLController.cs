using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class EndLVLController : MonoBehaviour
{
    private TaskController _taskController;
    private DeliveryController _deliveryController;
    public TMP_Text txtEnd;
    
    private bool taskCompleted = false;

    public GameObject panel, btnNext;

    public bool end = false;

    private void Awake()
    {
        _taskController = GameObject.FindWithTag("task").GetComponent<TaskController>();
        _deliveryController = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        txtEnd = GameObject.FindWithTag("txtTask").GetComponent<TMP_Text>();
        panel = GameObject.Find("Panel");
        btnNext = GameObject.Find("btnNext");
    }

    private void Start()
    {
        panel.SetActive(false);
        btnNext.SetActive(false);
    }

    void Update()
    {
        if (_deliveryController.contEntregue >= _taskController.minEntregas)
        {
            this.taskCompleted = true;
            btnNext.SetActive(true);
        }
        
        txtEnd.text = TaskController.taskTxt.text;
        if (this.taskCompleted == true)
        {
            txtEnd.fontStyle = FontStyles.Strikethrough;
        }
    }

    public bool EndScreen(int x, bool b)
    {
        Time.timeScale = x; // pausa
        panel.SetActive(b);
        return true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            end = true;
            EndScreen(0, true);
        }
    }
}
