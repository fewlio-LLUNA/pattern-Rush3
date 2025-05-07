using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            Destroy(other.gameObject);
            // Missˆ—‚ğ‚±‚±‚Å’Ç‰Á‚µ‚Ä‚à—Ç‚¢
            // comment
        }
    }
}
