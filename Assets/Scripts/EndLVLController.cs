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

    public GameObject panel;

    private void Awake()
    {
        _taskController = GameObject.FindWithTag("task").GetComponent<TaskController>();
        _deliveryController = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        txtEnd = GameObject.Find("txtEnd").GetComponent<TMP_Text>();
        panel = GameObject.Find("Panel");
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (_deliveryController.contEntregue >= _taskController.minEntregas)
        {
            this.taskCompleted = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            txtEnd.text = TaskController.taskTxt.text;
            if (this.taskCompleted == true)
            {
                txtEnd.fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
