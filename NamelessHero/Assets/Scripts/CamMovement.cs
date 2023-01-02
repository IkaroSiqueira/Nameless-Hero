using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    //CamRoom
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    //Change camera setting in inspector
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camSpeed;
    private float lookAhead;

    private void Update()
    {

        //NewRoomCam
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);

        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * camSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
