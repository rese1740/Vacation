using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [Header("Settings")]
    public float gageCount = 0;
    public float gageMaxCount = 100;
    public bool generateOnTag = false;
    public bool generateSuccess = false;

    [Header("Object Setting")]
    [SerializeField] private Slider gageSlider;

    private void Start()
    {
        gageSlider.maxValue = gageMaxCount;
    }


    private void Update()
    {
        if (gageCount >= gageMaxCount)
        {
            generateSuccess = true;
            Debug.Log("Generation Successful!");
        }
        if (Input.GetKey(KeyCode.G) && generateOnTag)
        {
            gageCount += 1 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.G) && generateOnTag)
        {
            gageCount = 0;
        }
        

        if(generateSuccess)
        {
            gageCount = gageMaxCount;
        }

        gageSlider.value = gageCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            generateOnTag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        generateOnTag = false;
    }

}
