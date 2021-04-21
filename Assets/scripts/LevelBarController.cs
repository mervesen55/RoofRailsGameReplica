using UnityEngine;
using UnityEngine.UI;

public class LevelBarController : MonoBehaviour
{
    public GameObject character;
    public Image fill;
    public float DivideValue = 180;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = character.transform.position.z / DivideValue;
    }
}
