using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCounter : MonoBehaviour
{
    private int DiamondCounter = 0;
    private int CylinderGemCounter = 0;
    public Text DiamondText;
    public Text CylinderGemText;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DiamondText.text = DiamondCounter.ToString();
        CylinderGemText.text = CylinderGemCounter.ToString();
    }

    public void UpdateGemCounter(string GemName)
    {
        if (GemName == "diamond")
            DiamondCounter += 1;
        if (GemName == "cylinder")
            CylinderGemCounter += 1;
    }
}
