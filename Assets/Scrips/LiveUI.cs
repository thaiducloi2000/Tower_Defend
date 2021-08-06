using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    public Text liveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        liveText.text = PlayerStats.lives.ToString()+" Lives ";
    }
}
