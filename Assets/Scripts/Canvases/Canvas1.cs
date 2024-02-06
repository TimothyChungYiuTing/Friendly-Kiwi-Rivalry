using UnityEngine;
using UnityEngine.UI;

public class Canvas1 : MonoBehaviour
{
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHearts(int hearts)
    {
        Heart1.color = new Color(0.93f, 1f, 0.9f);
        Heart2.color = new Color(0.93f, 1f, 0.9f);
        Heart3.color = new Color(0.93f, 1f, 0.9f);
        if (hearts < 3)
            Heart3.color = new Color(0.22f, 0.2f, 0.3f);
        if (hearts < 2)
            Heart2.color = new Color(0.22f, 0.2f, 0.3f);
        if (hearts < 1)
            Heart1.color = new Color(0.22f, 0.2f, 0.3f);
    }
}
