using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick playerJoystick;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        
    }

   

    private void FixedUpdate()
    {
        rig.linearVelocity = playerJoystick.GetMoveVector() * moveSpeed * Time.fixedDeltaTime;
    }

}
