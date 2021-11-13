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
    
    public bool taskCompleted = false;

    public GameObject panel, btnNext;

    public bool end = false;
    // private string passou1 = "";

    private PlayerController player;
    private SoundManager soundManager;

    private void Awake()
    {
        // configura variaveis no inicio
        _taskController = GameObject.FindWithTag("task").GetComponent<TaskController>();
        _deliveryController = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        txtEnd = GameObject.FindWithTag("txtTask").GetComponent<TMP_Text>();
        panel = GameObject.Find("Panel");
        btnNext = GameObject.Find("btnNext");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // passou1 = PlayerPrefs.GetString("fase1");
    }

    private void Start()
    {
        // esconde painel de pause e botao de proxima fase
        panel.SetActive(false);
        btnNext.SetActive(false);
    }

    void Update()
    {
        // checa se a task foi completada
        if (_deliveryController.contEntregue >= _taskController.minEntregas)
        {
            this.taskCompleted = true;
            txtEnd.fontStyle = FontStyles.Strikethrough;
        }
        
        txtEnd.text = TaskController.taskTxt.text; // mostra a task atual
        
        
    }

    public bool EndScreen(int x, bool b) 
    {
        Time.timeScale = x; // pausa
        panel.SetActive(b);

        if (this.taskCompleted == true && end == true) // se passou de fase, mostra botao de proxima fase
        {
            btnNext.SetActive(true);
            player.audioSource.clip = soundManager.som[7];
            player.audioSource.Play();
            // if (SceneManager.GetActiveScene().name.ToString() == "Fase1")
            // {
            //     PlayerPrefs.SetString("fase1", "passou");
            // }
        }
        else if (this.taskCompleted == false && end == true)
        {
            player.audioSource.clip = soundManager.som[3];
            player.audioSource.Play();
        }
        
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
