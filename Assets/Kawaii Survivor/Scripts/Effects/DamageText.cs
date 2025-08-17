using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate(string damage, bool isCriticalHit)
    {
        damageText.text = damage.ToString();

        animator.Play("Animate");
        damageText.color = isCriticalHit ? Color.yellow : Color.white;

    }
}
