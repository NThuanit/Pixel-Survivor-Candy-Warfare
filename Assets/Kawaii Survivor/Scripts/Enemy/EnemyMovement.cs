using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;


    // Update is called once per frame
    void Update()
    {
        //if (player != null) 
        //    FollowPlayer();
    }

    public void StorePlayer(Player player)
    {
        this.player = player;   
    }
  
    public void FollowPlayer()
    {
        Vector2 dicrection = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + dicrection * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

}
