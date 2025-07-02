using UnityEngine;

public class KeyBeamController : MonoBehaviour
{
    public GameObject keyBeam1; // Aキー
    public GameObject keyBeam2; // Dキー
    public GameObject keyBeam3; // Jキー
    public GameObject keyBeam4; // Lキー

    public GameObject keyBeamInfo1; // Aキー
    public GameObject keyBeamInfo2; // Dキー
    public GameObject keyBeamInfo3; // Jキー
    public GameObject keyBeamInfo4; // Lキー

    void Start()
    {
        // 全て最初は非表示に
        keyBeam1.SetActive(false);
        keyBeam2.SetActive(false);
        keyBeam3.SetActive(false);
        keyBeam4.SetActive(false);

        keyBeamInfo1.SetActive(false);
        keyBeamInfo2.SetActive(false);
        keyBeamInfo3.SetActive(false);
        keyBeamInfo4.SetActive(false);
    }

    void Update()
    {
        keyBeam1.SetActive(Input.GetKey(KeyCode.A));
        keyBeam2.SetActive(Input.GetKey(KeyCode.D));
        keyBeam3.SetActive(Input.GetKey(KeyCode.J));
        keyBeam4.SetActive(Input.GetKey(KeyCode.L));

        keyBeamInfo1.SetActive(Input.GetKey(KeyCode.A));
        keyBeamInfo2.SetActive(Input.GetKey(KeyCode.D));
        keyBeamInfo3.SetActive(Input.GetKey(KeyCode.J));
        keyBeamInfo4.SetActive(Input.GetKey(KeyCode.L));
    }
}
