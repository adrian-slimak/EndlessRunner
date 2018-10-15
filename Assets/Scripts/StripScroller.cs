using UnityEngine;
using System.Collections;

public class StripScroller : MonoBehaviour
{

    public float scrollSpeed;

    private Renderer renderer;
    private Vector2 savedOffset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        Vector2 transform = new Vector2(scrollSpeed * Time.deltaTime, 0f);

        renderer.sharedMaterial.SetTextureOffset("_MainTex", renderer.sharedMaterial.GetTextureOffset("_MainTex") + transform);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
