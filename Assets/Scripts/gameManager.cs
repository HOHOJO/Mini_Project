using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public GameObject card;
    public Text timeTxt;
    public GameObject endTxt;
    float time = 0.0f;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public AudioSource audioSource;
    public AudioClip match;

    // Start is called before the first frame update

    void Start()
    {
        Time.timeScale = 1.0f;

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void Awake()
    {
        I = this;
    }


    //같은 카드 찾기

    public void isMatched()
    {
        //카드 sprite 이름 가져오기
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        //이름 비교
        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            // 게임 종료
            if (cardsLeft == 2)
            {
                //endTxt.SetActive(true);
                //Time.timeScale = 0.0f;
                Invoke("GameEnd", 1.0f);
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        //종료
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }
}
