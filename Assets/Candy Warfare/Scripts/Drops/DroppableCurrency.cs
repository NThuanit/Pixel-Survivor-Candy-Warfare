using System.Collections;
using UnityEngine;

public abstract class DroppableCurrency : MonoBehaviour, ICollectable
{
    private bool collected;

    private void OnEnable()
    {
        collected = false;
    }

    public void Collect(Player player)
    {
        if (collected) return;

        collected = true;

        StartCoroutine(MoveToWardsPlayer(player));
    }


    IEnumerator MoveToWardsPlayer(Player player)
    {
        float timer = 0;

        Vector2 intialPosition = transform.position;
        //Vector2 targetPosition = player.GetCenter();

        while (timer < 1)
        {
            Vector2 targetPosition = player.GetCenter();

            transform.position = Vector2.Lerp(intialPosition, targetPosition, timer);

            timer += Time.deltaTime;

            yield return null;
        }

        Collected();
    }

    protected abstract void Collected();
}
