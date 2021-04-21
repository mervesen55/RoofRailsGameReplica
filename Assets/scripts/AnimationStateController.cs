using UnityEngine.Animations.Rigging;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private bool isFinished = false;
    public Animator animator;
    public Rigidbody rb;
    public StickController stick;
    public ParentController parentController;
    public bool stickPosHasBeenReset = false;
    public static AnimationStateController instance;
    public GemCounter gemCounter;
    

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "cylinder")
        {
            GameManager.instance.CylinderBonusPointMove(parentController.gameObject);
            gemCounter.UpdateGemCounter("cylinder");
            Destroy(other.transform.gameObject);
            stick.ExpandStick();
        }
        if(other.tag == "fail")
        {
            GameManager.instance.RestartText.SetActive(true);
            GameManager.instance.GameOverText.SetActive(true);
            GameManager.instance.Failed = true;
            parentController.StopMoving();
            transform.GetComponent<RigBuilder>().enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "roof")
        {
            
            StickController.instance.IsSliding = false;
            ResetAnim("IsSliding", false);
            ResetAnim("IsJumping", false);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            stick.ResetContraints();

            if (!stickPosHasBeenReset)
            {
                stick.ResetPos();
                stickPosHasBeenReset = true;
            }

        }
        if (collision.collider.tag == "finish")
        {
            if (!isFinished)
            {
                GameManager.instance.GameWon = true;
                GameManager.instance.RestartText.gameObject.SetActive(true);
                GameManager.instance.LevelWon.gameObject.SetActive(true);
                isFinished = true;
                animator.SetTrigger("IsFinished");               
                CloseGravity();
                parentController.StopMoving();
                transform.GetComponent<RigBuilder>().enabled = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
         

        }
    }

    public void OpenGravity()
    {
        rb.useGravity = true;
    }
    public void CloseGravity()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        
    }

    public void CloseContraints()
    {
        rb.constraints = RigidbodyConstraints.None;
        
    }

    public void ResetAnim(string TriggerName, bool value)
    {
        animator.SetBool(TriggerName, value);
    }

   
}
