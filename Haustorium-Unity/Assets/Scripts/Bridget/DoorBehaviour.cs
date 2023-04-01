using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public AudioClip MainDoor;
    private Animator DWF_Door;
  
    void Start()
    {
        DWF_Door = GetComponent<Animator>();
        Debug.Log(DWF_Door);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playHinge();
            DWF_Door.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playHinge();
            DWF_Door.SetBool("IsOpen", false);
        }
    }

    public void playHinge()
    {
        AudioSource.PlayClipAtPoint(MainDoor, transform.position, 25);
    }

}
