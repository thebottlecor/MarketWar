using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public Rigidbody2D playerRg;
    public PlayerUnit player;

    public static EventHandler<int> skillButtonEvent;

    private float castingTime;
    public bool IsCasting => castingTime > 0f;
    public GameObject castingEffect;


    private void Update()
    {
        Vector3 dir = Vector3.zero;

        if (IsCasting)
        {
            castingTime += -1f * Time.deltaTime;
            castingEffect.SetActive(true);
        }
        else
        {
            castingEffect.SetActive(false);

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1f;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                if (skillButtonEvent != null)
                    skillButtonEvent(this, 1);
            }
        }

        if (dir != Vector3.zero)
        {
            playerRg.velocity = dir * player.speed;
        }
        else
        {
            playerRg.velocity = Vector3.zero;
        }

        Vector3 camPos = player.transform.position;
        camPos.z = -10f;
        Camera.main.transform.position = camPos;
    }

    public void SetCast(float castingTime)
    {
        this.castingTime = castingTime;
    }
}
