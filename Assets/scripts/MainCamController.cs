using UnityEngine;

public class MainCamController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public ParticleSystem WindEffect;

    private void LateUpdate()
    {
        if (!GameManager.instance.Failed && !GameManager.instance.GameWon)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z) + offset;
            WindEffect.transform.position = Camera.main.transform.position + new Vector3(0, 0, 30);
        }
    }
}
