using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StickController : MonoBehaviour
{
    private Rigidbody rb;
    public bool IsSliding = false;
    public static StickController instance;
    public GameObject dropStick;
    public GameObject character;
    public GameObject parent;
    public AnimationStateController stateController;
    public GameManager gameManager;
    public ParentController parentController;
    public GemCounter gemCounter;
    public ParticleSystem Slidepart;
    public ParticleSystem obstaclePart;
    
  
    // Start is called before the first frame update
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y - character.transform.position.y) > 5)
        {

            stateController.OpenGravity();
            character.transform.SetParent(null);
            stateController.CloseContraints();
            parentController.StopMoving();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "obstacle")
        {
            Vector3 partPos = new Vector3(collision.collider.transform.position.x - 0.1f, transform.position.y, collision.collider.transform.position.z - 0.5f); ;
            ParticleSystem newPart = Instantiate(obstaclePart, partPos, Quaternion.identity);
            newPart.Play();
            Destroy(newPart, 2);
            float positionDif = parent.transform.position.x - collision.transform.position.x;
            float hangover = (transform.localScale.y/2 - Mathf.Abs(transform.position.x - collision.transform.position.x) /2); //kopan parcanın uzunlugu            
            
            float direction = positionDif > 0 ? 1f : -1f;
            if (Mathf.Abs(collision.collider.transform.position.x - parent.transform.position.x) < Mathf.Abs(transform.localPosition.x))
            {
                hangover = transform.localScale.y / 2 + Mathf.Abs(collision.collider.transform.position.x - transform.position.x) / 2;
            }
            float newYScale = transform.localScale.y - Mathf.Abs(hangover); // çubuğun yeni boyutu
            transform.localScale = new Vector3(0.3f, newYScale, 0.3f);
            float newXposition =  (collision.transform.position.x + ((transform.localScale.y) *direction));          
            transform.position = new Vector3(newXposition, character.transform.position.y + 1.06f,
                character.transform.position.z + 0.37f);
            SpawnHangout(hangover, direction, collision);
            //rb.AddForce(-(transform.position.x - character.transform.position.x), 0, 0);
            transform.DOMoveX(character.transform.position.x, 0.3f);
            //Vector3.Lerp(transform.position, new Vector3(character.transform.position.x, transform.position.y, transform.position.z), 0.5f);
            //StartCoroutine(CenterStick());
           
        }
        if (collision.collider.tag == "slide")
        {
            
            IsSliding = true;
            rb.constraints = RigidbodyConstraints.None;
            stateController.ResetAnim("IsSliding", true);
            stateController.ResetAnim("IsJumping", false);
            stateController.CloseGravity();
            character.transform.position = new Vector3(transform.position.x, transform.position.y -2,
                transform.position.z);
        }
        if(collision.collider.tag == "diamond")
        {
            GameManager.instance.DiamondBonusMove(parent);
            gemCounter.UpdateGemCounter("diamond");
            Destroy(collision.transform.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "slide")
        {
            Vector3 partPos = new Vector3(collision.collider.transform.position.x, collision.collider.transform.position.y,
                transform.position.z);
            ParticleSystem newPart = Instantiate(Slidepart, partPos, Quaternion.identity);
            newPart.transform.Rotate(90, 0, 0, Space.World);
            newPart.Play();
            Destroy(newPart, 0.15f);

        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "roofEndTrigger")
        {
            rb.useGravity = true;
            stateController.ResetAnim("IsJumping", true);
            
        }
        if (other.tag == "roofStartTrigger")
        {

            rb.useGravity = false;
            AnimationStateController.instance.stickPosHasBeenReset = false;
        }
        if (other.tag == "slideStartTrigger")
        {
            rb.useGravity = true;
            stateController.ResetAnim("IsJumping", true);
            
        }
        if (other.tag == "slideEndTrigger")
        {
            character.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            
            IsSliding = false;
            stateController.ResetAnim("IsJumping", true);
            stateController.ResetAnim("IsSliding", false);
            transform.position = transform.position + new Vector3(0, 0.2f, 0);
            stateController.OpenGravity();
        }
    }
    private void SpawnHangout(float hangover, float direction, Collision obstacle)
    {
        float hangoutPosX = obstacle.transform.position.x + (- hangover)*direction;
        Vector3 newDropStickPosition = new Vector3(hangoutPosX, transform.position.y, transform.position.z);
        GameObject newDropStick = Instantiate(dropStick, newDropStickPosition, Quaternion.identity);
        newDropStick.transform.localScale = new Vector3(0.3f, hangover, 0.3f);
        newDropStick.transform.Rotate(0, 0, 90);
        Destroy(newDropStick, 5f);
    }
    private IEnumerator CenterStick()
    {
        yield return new WaitForSeconds(2);
        transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
    }

    public void ExpandStick()
    {
        transform.localScale += new Vector3(0, 0.5f, 0);
    }
    public void ResetContraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
    }

    public void ResetPos()
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1.25f,
            character.transform.position.z + 0.37f);
    }


}
