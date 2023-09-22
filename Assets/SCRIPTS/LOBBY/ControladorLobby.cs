using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//MonoBehaviourPunCallbacks para que se puedan usar otros metodos virtuales
public class ControladorLobby : MonoBehaviourPunCallbacks
{

    #region PanelInicio

    [Header("PANEL INICIO")]
    [SerializeField] private GameObject panelInicio;
    [SerializeField] private TMP_InputField inputNickname;
    [SerializeField] private Button botonConectar;
    [SerializeField] private TMP_Text mensajeInicio;

    private void Awake()
    {

        //Paneles
        panelInicio.gameObject.SetActive(true);
        panelSala.gameObject.SetActive(false);

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

        //Contamos cuantas salas hay disponibles
        int salas = PhotonNetwork.CountOfRooms;

        //Si no hay salas creadas, las creamos
        if (salas == 0)
        {
            //Importar using Photon.Realtime;
            RoomOptions salaConfig = new RoomOptions() { MaxPlayers = 10 };
            PhotonNetwork.CreateRoom("XP", salaConfig);
        }
        //Si ya hay una sala creada, nos unimos
        else
        {
            PhotonNetwork.JoinRoom("XP");
        }

    }

    #endregion PanelInicio

    #region PanelSala

    [Header("\nPANEL SALA")]
    [SerializeField] private GameObject panelSala;
    [SerializeField] private Transform panelJugadores;
    [SerializeField] private SlotPlayer pfSlotPlayer;

    //Se ejecuta cuando creamos o nos unimos a una sala
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        panelInicio.SetActive(false);
        panelSala.SetActive(true);

    }


    #endregion PanelSala

}
