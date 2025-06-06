using UnityEngine;
using TMPro;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Player player;

    [Header("Show Text")]
    [SerializeField] private GameObject ShowTextPrefab;

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFXPrefab;

    protected GameObject HPBar;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        HPBar = GetComponentInChildren<HPBar_UI>()?.gameObject;
    }

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

      protected virtual void Update()
    {
    }

    public GameObject CreateText(string text)
    {
        float xOffset = Random.Range(-1, 1);
        float yOffset = Random.Range(0.5f, 1.5f);

        Vector3 positionOffset = new Vector3(xOffset, yOffset, 0);
        GameObject newText = Instantiate(ShowTextPrefab, transform.position + positionOffset, Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = text;

        return newText;
    }

    public void MakeEntityTransparent(bool transparent)
    {
        sr.color = transparent ? Color.clear : Color.white;
    }

    public void MakeEntityTransparent_IncludingHPBar(bool transparent)
    {
        if (HPBar != null)
            HPBar.SetActive(!transparent);
        sr.color = transparent ? Color.clear : Color.white;
    }

    public void CreateHitFX(Transform targetTransform)
    {
        float zRotation = Random.Range(-80, 80);
        float xOffset = Random.Range(-0.5f, 0.5f);
        float yOffset = Random.Range(-0.5f, 0.5f);

        Vector3 generationPosition = new Vector3(targetTransform.position.x + xOffset, targetTransform.position.y + yOffset);
        Vector3 generationRotation = new Vector3(0, 0, zRotation);

        GameObject newHitFX = Instantiate(hitFXPrefab, generationPosition, Quaternion.identity);
        newHitFX.transform.Rotate(generationRotation);

        Destroy(newHitFX, 0.4f);
    }
}
