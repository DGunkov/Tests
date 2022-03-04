using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform joystick_trans;
    public Transform handle_trans;
    public Transform player_pos;
    public Rigidbody player;

    Vector3 cam_pos;

    void Start()
    {
        cam_pos = Camera.main.transform.position;
    }

    void FixedUpdate()
    {
        //Движение камеры
        Camera.main.transform.position = player_pos.position + new Vector3(0, 30, -25);
        //Движение игрока
        player.velocity = new Vector3(joystick.Horizontal, player.velocity.y, joystick.Vertical) * 25;
        //Поворот игрока
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            player_pos.rotation = Quaternion.LookRotation(player.velocity);
        }
        
    }
}
