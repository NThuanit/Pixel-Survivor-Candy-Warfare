using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;

    private void Awake()  =>  RangeWeapon.onBulletShot += Shake;

    private void OnDestroy() => RangeWeapon.onBulletShot -= Shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Shake();
    }

    [NaughtyAttributes.Button]
    private void Shake()
    {
        Vector2 direction = Random.onUnitSphere.With(z: 0).normalized;

        transform.localPosition = Vector3.zero;

        LeanTween.cancel(gameObject);
        LeanTween.moveLocal(gameObject, direction * shakeMagnitude, shakeDuration)
            .setEase(LeanTweenType.easeShake);
    }
}
