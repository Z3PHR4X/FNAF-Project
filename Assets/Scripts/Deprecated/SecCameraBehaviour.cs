using UnityEngine;

public class SecCameraBehaviour : MonoBehaviour
{
    public bool isDeprecated = true;
    [SerializeField]
    private int _rotationSpeed = 2;
    [SerializeField]
    private int _maxRotation = 60;

    private float rotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        rotationOffset = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }

    void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(28, rotationOffset + Mathf.PingPong(Time.time * _rotationSpeed, _maxRotation), 0);
    }
}