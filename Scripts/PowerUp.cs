using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Health Settings")]
    public bool healtPowerUp = false;
    public int healtAmouth = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmouth = 5;
    [Header("Transform Settings")]
    [SerializeField] private Vector3 turnVektor;

    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;
    [SerializeField] private Vector3 scaleVektor;
    private float scaleFactor;
    private Vector3 startScale;
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnVektor);
        SinusWawe();
    }

    private void SinusWawe()
    {
        if(period <= 0f)
        {
            period = 0.1f;
        }
        float cycles = Time.timeSinceLevelLoad / period;
        const float piX2 = Mathf.PI * 2;
        float sinusWawe = Mathf.Sin(piX2 * cycles);

        scaleFactor = sinusWawe / 2 + 0.5f;
        Vector3 offset = scaleFactor * scaleVektor;
        transform.localScale = startScale + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if(healtPowerUp)
        {
            other.gameObject.GetComponent<target>().GetHealth += healtAmouth;
        }
        else if(ammoPowerUp)
        {
            other.gameObject.GetComponent<Attack>().GetBullet += ammoAmouth;
        }
        Destroy(gameObject);
    }
}
