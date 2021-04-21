using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStarted = false;
    public bool Failed = false;
    public bool GameWon = false;
    public GameObject character;
    public AnimationStateController StateController;
    public GameObject StartIcon;
    public GameObject RestartText;
    public GameObject GameOverText;
    public GameObject LevelWon;
    public GameObject DiamondBonus;
    public GameObject CylinderBonusPoint;
    public GameObject Canvas;
    
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !GameStarted)
        {
            StartIcon.gameObject.SetActive(false);
            GameStarted = true;
            StateController.ResetAnim("IsStarted", true);
        }
        if(Input.GetMouseButtonDown(0) && (Failed || GameWon))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void GameOver()
    {
        character.GetComponent<Rigidbody>().useGravity = true;
        character.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        StateController.ResetAnim("IsFalling", true);
    }


    public void CylinderBonusPointMove(GameObject obj)
    {
        GameObject stickBonus = Instantiate(CylinderBonusPoint);
        stickBonus.transform.SetParent(Canvas.transform);

        stickBonus.transform.localPosition = new Vector3((obj.transform.localPosition.x * 60) + 38, 0, 0);
        stickBonus.transform.DOMoveY(800, 0.8f);

        StartCoroutine(DelayedDestroyObj(stickBonus));

    }
    public void DiamondBonusMove(GameObject obj)
    {
        GameObject diamondBonus = Instantiate(DiamondBonus);
        diamondBonus.transform.SetParent(Canvas.transform);
        diamondBonus.transform.localPosition = new Vector3((obj.transform.localPosition.x * 40) + 10, 0, 0);
        diamondBonus.transform.DOMove(new Vector3(580f, 1110f, 0), 1f);

        StartCoroutine(DelayedDestroyObj(diamondBonus));

    }

    private IEnumerator DelayedDestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(obj);

    }
}
