using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class SlotPlayer : MonoBehaviour
{

    //Cuando se utilizan propiedades se pone un "_" para establecer que es private
    private Player _player;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private Transform mesh;

    public Player Player 
    { 

        get=> _player;
        set
        {
            _player = value;
            nickname.text = value.NickName;
        }

    }

}
