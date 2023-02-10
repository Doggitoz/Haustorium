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
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.SetState(GameState.Playing);
        }
        if (finished)
        {
            background.color = new Color(0, 0, 0, (timer / timeToFade));
            timer += Time.deltaTime;
            if (timer > timeToFade + 1f)
            {
                //START PLAYING STATE
                GameManager.Instance.SetState(GameState.Playing);
            }
            return;
        }
        else if (!source.isPlaying)
        {
            if (indexInCinematic == ListOfClips.Length)
            {
                finished = true;
                return;
            }
            source.PlayOneShot(ListOfClips[indexInCinematic]);
            indexInCinematic++;
        }
    }
}
