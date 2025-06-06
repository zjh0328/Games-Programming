using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    private TextMeshPro myText;

    [SerializeField] private float appearingSpeed = 1f;
    [SerializeField] private float colorLosingSpeed = 1f;
    [SerializeField] private float lifeTime = 1f;
    private float textTimer;

    private void Awake()
    {
        myText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        textTimer = lifeTime;
    }

    private void Update()
    {
        transform.position += Vector3.up * appearingSpeed * Time.deltaTime;

        textTimer -= Time.deltaTime;

        if (textTimer < 0)
        {
            float alpha = myText.color.a - colorLosingSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha); 

            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);

            if (alpha <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
