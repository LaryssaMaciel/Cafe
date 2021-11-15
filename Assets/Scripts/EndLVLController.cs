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
    // auxiliares
    private TaskController _taskController;
    private DeliveryController _deliveryController;
    private PlayerController player;
    
    public TMP_Text txtEnd; // texto da task na UI/Pause/Endgame
    public bool taskCompleted = false; // se a tarefa foi completa
    public GameObject panel, btnNext, btnResume; // painel de pause, botao de proxima fase
    public bool end = false; // se chegou no fim da fase
    private SoundManager soundManager; // audio

    private void Awake()
    {
        // configura variaveis no inicio
        _taskController = GameObject.FindWithTag("task").GetComponent<TaskController>();
        _deliveryController = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        txtEnd = GameObject.FindWithTag("txtTask").GetComponent<TMP_Text>();
        panel = GameObject.Find("Panel");
        btnNext = GameObject.Find("btnNext");
        btnResume = GameObject.Find("btnResume");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // passou1 = PlayerPrefs.GetString("fase1");
    }

    private void Start()
    {   // esconde painel de pause e botao de proxima fase
        panel.SetActive(false);
        btnNext.SetActive(false);
    }

    void Update()
    {
        txtEnd.text = TaskController.taskTxt.text; // mostra a task atual

        // checa se a task foi completada
        if (_deliveryController.contEntregue >= _taskController.minEntregas)
        {
            this.taskCompleted = true;
            txtEnd.fontStyle = FontStyles.Strikethrough; // marca o texto
        }
    }

    public bool EndScreen(int x, bool b) // tela de pause/endgame
    {
        Time.timeScale = x; // pausa/despausa
        panel.SetActive(b); // mostra/esconde painel
        if (this.taskCompleted && end) // se completou task e chegou no fim da fase
        {   
            AudioUpdate(7); // toca audio de vitoria
            btnNext.SetActive(true); // mostra btn de proxima fase
            btnResume.SetActive(false); // esconde btn resume
        }
        // se chegou ao fim mas nao completou fase
        else if (!this.taskCompleted && end) { AudioUpdate(3); } // toca som de derrota
        return true; // retorna funcao
    }

    void AudioUpdate(int audio) // atualiza audio
    {
        player.GetComponent<PlayerController>().audioSource.clip = soundManager.som[audio];
        player.GetComponent<PlayerController>().audioSource.Play();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") // se player colidiu com fim da fase
        {   // chegou ao fim
            end = true; 
            EndScreen(0, true);
        }
    }
}
