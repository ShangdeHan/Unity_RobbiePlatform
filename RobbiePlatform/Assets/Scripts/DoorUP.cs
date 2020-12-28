using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUP : MonoBehaviour
{
    Animator anim;
    int openID;

    void Start()
    {
        anim = GetComponent<Animator>();
        openID = Animator.StringToHash("Open");
        GameManager.RegisterDoorUP(this);
    }

    public void Open()
    {
        anim.SetTrigger(openID);
        AudioManager.PlayDoorOpenAudio();
    }
}
