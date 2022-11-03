using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2((float)speed * Time.deltaTime, 0);
    }
}
