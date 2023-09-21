using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorLobby : MonoBehaviourPunCallbacks //MonoBehaviourPunCallbacks para que se puedan usar otros metodos virtuales
{

    #region PanelInicio

    [Header("PANEL INICIO")]
    [SerializeField] private GameObject panelInicio;
    [SerializeField] private TMP_InputField inputNickname;
    [SerializeField] private Button botonConectar;
    [SerializeField] private TMP_Text mensajeInicio;

    private void Awake()
    {

        //Deshabilitamos el clic del Boton Conectar
        botonConectar.interactable = false;
        botonConectar.onClick.AddListener(Iniciar);

        //Borrar mensaje de prueba
        mensajeInicio.text = string.Empty;

    }

    private void Start()
    {

        mensajeInicio.text = "Conectandose . . .";

        //Intentamos conectarnos
        PhotonNetwork.ConnectUsingSettings();

    }

    //Este metodo se ejecutara cuando nos conectamos al servidor
    public override void OnConnectedToMaster()
    {
        
        //Nos intentamos unir al Lobby
        PhotonNetwork.JoinLobby();

    }

    //Este metodo se ejecuta cuando entramos al Lobby
    public override void OnJoinedLobby()
    {

        //Un delay de 1s para evitar errores
        Invoke("Conectado", 1);

    }


    private void Conectado()
    {

        mensajeInicio.text = String.Empty;
        botonConectar.interactable = true;

    }

    public void Iniciar()
    {

        //Obtenemos el string del Input Field
        string nickname = inputNickname.text;

        //Verificamos que tenga un nickname
        if (nickname == string.Empty)
        {
            mensajeInicio.text = "El Nickname esta vacio!";
            return;
        }

        //Asignamos nuestro Nickname de manera online
        PhotonNetwork.NickName = nickname;

    }

    #endregion PanelInicio

}
