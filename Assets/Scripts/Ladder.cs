using UnityEngine;
using StarterAssets;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private FirstPersonController fpsInput;
    public bool canClimb = false;
    public float climbSpeed = 3.2f;

    void Start() => fpsInput = player.GetComponent<FirstPersonController>();

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            fpsInput.enabled = false;
            canClimb = !canClimb;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            fpsInput.enabled = true;
            canClimb = !canClimb;
        }
    }

    void Update()
    {
       if (canClimb)
       {
           if (Input.GetKey(KeyCode.W))
           {
               player.transform.position += Vector3.up / climbSpeed;
           }

           if (Input.GetKey(KeyCode.S))
           {
               player.transform.position += Vector3.down / climbSpeed;
           }
       } 
    }
}
