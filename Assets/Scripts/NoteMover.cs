using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float speed = 10f;
    public int laneIndex;
    public bool wasJudged = false;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
