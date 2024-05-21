using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscenes : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    public GameObject Cutscene1;
    public GameObject Cutscene2;

    public Canvas canvas;
    public Text cutsceneTextPrefab; // Reference to a prefab of the Text element
    public string text1 = "Hello, this is the first slide.";
    public string text2 = "Hello, this is the second slide.";
    public float textSpeed = 0.05f; // Speed at which the text appears

    private SpriteRenderer cutscene1Renderer;
    private SpriteRenderer cutscene2Renderer;
    private Text cutscene1Text;
    private Text cutscene2Text;

    private enum CutsceneState { None, FadeIn1, Wait1, FadeOut1, FadeIn2, Wait2, FadeOut2 }
    private CutsceneState state;

    private void Start()
    {
        // Instantiate Cutscene1 and get its SpriteRenderer
        if (Cutscene1 != null)
        {
            GameObject instance1 = Instantiate(Cutscene1);
            instance1.transform.position = Vector3.zero;
            cutscene1Renderer = instance1.GetComponent<SpriteRenderer>();
            if (cutscene1Renderer != null)
            {
                cutscene1Renderer.color = new Color(cutscene1Renderer.color.r, cutscene1Renderer.color.g, cutscene1Renderer.color.b, 0f); // Start transparent
            }
        }

        // Instantiate Cutscene2 and get its SpriteRenderer
        if (Cutscene2 != null)
        {
            GameObject instance2 = Instantiate(Cutscene2);
            instance2.transform.position = Vector3.zero;
            cutscene2Renderer = instance2.GetComponent<SpriteRenderer>();
            if (cutscene2Renderer != null)
            {
                cutscene2Renderer.color = new Color(cutscene2Renderer.color.r, cutscene2Renderer.color.g, cutscene2Renderer.color.b, 0f); // Start transparent
            }
        }

        // Instantiate the text for Cutscene1
        if (cutsceneTextPrefab != null && canvas != null)
        {
            cutscene1Text = Instantiate(cutsceneTextPrefab, canvas.transform);
            cutscene1Text.text = "";
            cutscene1Text.rectTransform.anchoredPosition = new Vector2(0, -Screen.height / 3); // Position in the lower third
            cutscene1Text.gameObject.SetActive(false); // Start inactive
        }

        // Instantiate the text for Cutscene2
        if (cutsceneTextPrefab != null && canvas != null)
        {
            cutscene2Text = Instantiate(cutsceneTextPrefab, canvas.transform);
            cutscene2Text.text = "";
            cutscene2Text.rectTransform.anchoredPosition = new Vector2(0, -Screen.height / 3); // Position in the lower third
            cutscene2Text.gameObject.SetActive(false); // Start inactive
        }

        state = CutsceneState.FadeIn1; // Start with the first cutscene fade-in
    }

    private void Update()
    {
        switch (state)
        {
            case CutsceneState.FadeIn1:
                if (FadeIn(cutscene1Renderer))
                {
                    cutscene1Text.gameObject.SetActive(true); // Activate text
                    StartCoroutine(RevealText(cutscene1Text, text1));
                    state = CutsceneState.Wait1;
                }
                break;

            case CutsceneState.Wait1:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    state = CutsceneState.FadeOut1;
                }
                break;

            case CutsceneState.FadeOut1:
                if (FadeOut(cutscene1Renderer))
                {
                    cutscene1Text.gameObject.SetActive(false); // Deactivate text
                    state = CutsceneState.FadeIn2;
                }
                break;

            case CutsceneState.FadeIn2:
                if (FadeIn(cutscene2Renderer))
                {
                    cutscene2Text.gameObject.SetActive(true); // Activate text
                    StartCoroutine(RevealText(cutscene2Text, text2));
                    state = CutsceneState.Wait2;
                }
                break;

            case CutsceneState.Wait2:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    state = CutsceneState.FadeOut2;
                }
                break;

            case CutsceneState.FadeOut2:
                if (FadeOut(cutscene2Renderer))
                {
                    cutscene2Text.gameObject.SetActive(false); // Deactivate text
                    gameObject.SetActive(false); // Deactivate the script after the second cutscene fades out
                }
                break;
        }
    }

    private bool FadeIn(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) return true;

        spriteRenderer.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
        if (spriteRenderer.color.a >= 1f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            return true; // Fade-in complete
        }
        return false; // Fade-in not yet complete
    }

    private bool FadeOut(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) return true;

        spriteRenderer.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
        if (spriteRenderer.color.a <= 0f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            return true; // Fade-out complete
        }
        return false; // Fade-out not yet complete
    }

    private IEnumerator RevealText(Text textComponent, string text)
    {
        textComponent.text = "";
        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
