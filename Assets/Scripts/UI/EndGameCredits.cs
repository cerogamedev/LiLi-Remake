using System.Collections;
using UnityEngine;
using TMPro;

public class EndGameCredits : MonoBehaviour
{
    [TextArea] public string[] creditsText;
    public float letterDelay = 0.1f;
    public float lineDelay = 1.5f;
    public float fadeDuration = 2f;
    public TextMeshProUGUI creditsDisplay;

    private IEnumerator Start()
    {
        creditsDisplay.text = "";
        for (int i = 0; i < creditsText.Length; i++)
        {
            string line = creditsText[i];
            for (int j = 0; j < line.Length; j++)
            {
                creditsDisplay.text += line[j];
                yield return new WaitForSeconds(letterDelay);
            }
            yield return new WaitForSeconds(lineDelay);
            StartCoroutine(FadeOut(creditsDisplay));
            yield return new WaitForSeconds(fadeDuration);
            creditsDisplay.text = "";
            creditsDisplay.color = Color.white;

            // Check if this is the last line of credits text
            if (i == creditsText.Length - 1)
            {
                // Wait for the fade-out to complete
                yield return StartCoroutine(FadeOut(creditsDisplay));

                // Close the game
                Application.Quit();
            }
        }
    }

    private IEnumerator FadeOut(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
    }
    public void OpenURL()
    {
        Application.OpenURL("https://www.patreon.com/cerenbeybi");
    }

}
