using UnityEngine;

public class ParentController : MonoBehaviour
{
    private int speed = 10;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameStarted)
        {
            transform.position += transform.forward * Time.deltaTime * speed;


            if (Input.GetKey("a"))
            {
                if (transform.position.x > -2)
                    transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
            }
            if (Input.GetKey("d"))
            {
                if (transform.position.x < 2)
                    transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
            }

        }



    }

    public void StopMoving()
    {
        speed = 0;
    }
        
}
