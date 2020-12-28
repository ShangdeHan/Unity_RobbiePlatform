using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    int faderID;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        faderID = Animator.StringToHash("Fade");
        GameManager.RegisterSceneFader(this);
    }

    public void FadeOut()
    {
        anim.SetTrigger(faderID);
    }
}
