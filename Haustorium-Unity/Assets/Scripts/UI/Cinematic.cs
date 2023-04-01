using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    [SerializeField] AudioClip[] ListOfClips;
    [SerializeField] Image background;
    [SerializeField] Image fadeBackground;
    [SerializeField] Sprite[] backgroundImages;
    [SerializeField] AudioSource source;

    public bool fadeToBlack;
    public bool skipToNextScene;

    int indexInCinematic = 0;
    float timeToFade = 1.5f;
    float timer = 0f;

    bool finished = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && skipToNextScene)
        {
            GameManager.Instance.SetState(GameState.Playing);
        }
        if (finished)
        {
            if (fadeToBlack)
            {
                fadeBackground.color = new Color(0, 0, 0, (timer / timeToFade));
                timer += Time.deltaTime;
            }
            if (timer > timeToFade + 1f && skipToNextScene)
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
            background.sprite = backgroundImages[indexInCinematic];
            indexInCinematic++;
        }
    }
}
