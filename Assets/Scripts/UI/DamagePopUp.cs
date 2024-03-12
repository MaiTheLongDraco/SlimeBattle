using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private TextMesh popUp;
    public float fadeDuration = 2f; 
    public float startDelay = 1f;
    public int startSize ;
    public bool isCrit ;
    [SerializeField] private Color critColor;
    [SerializeField] private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        //SetUp(startSize, isCrit);
    }
    public void SetUp(int size, bool isCritical)
	{
        if(isCritical)
		{
            popUp.color = critColor;
            popUp.fontSize = size;

        }
        else
		{
            popUp.color = defaultColor;
            popUp.fontSize = size;

        }
    }
    public void SetText(int set)
	{
        popUp.text = set.ToString();
	}
    IEnumerator FadeText()
    {

        yield return new WaitForSeconds(startDelay);

        Color originalColor = popUp.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            popUp.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        popUp.color = targetColor;
        if (popUp.color.a <= 0) Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeText());
    }
}
