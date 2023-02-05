using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCinematic : MonoBehaviour
{
    [SerializeField] AudioClip[] ListOfClips;
    [SerializeField] Image background;
    [SerializeField] AudioSource source;

    int indexInCinematic = 0;
    float timeToFade = 1.5f;
    float timer = 0f;

    bool finished = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (finished)
        {
            timer += Time.deltaTime;
            background.color = new Color(0, 0, 0, 255 * (timer / timeToFade)); 

            if (timer > timeToFade)
            {
                //START PLAYING STATE
                GameManager.GM.SetState(GameState.Playing);
            }
            return;
        }
        if (!source.isPlaying)
        {
            if (indexInCinematic == ListOfClips.Length)
            {
                finished = true;
            }
            source.PlayOneShot(ListOfClips[indexInCinematic]);
            indexInCinematic++;
        }
    }
}
