using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //카드열기
    public void openCard()
    {
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if (gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }
    }
    //카드없애기
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }
    //카드닫기
    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
