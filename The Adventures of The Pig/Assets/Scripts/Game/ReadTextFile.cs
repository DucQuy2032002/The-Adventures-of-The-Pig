using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class ReadTextFile : MonoBehaviour
{
    string filePath;
    public TextMeshProUGUI creditText;
    [SerializeField] private Color highlightColor;


    void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "Credit.txt");
        StartCoroutine(ReadFile());
    }

    IEnumerator ReadFile()
    {
        if (filePath.Contains("://") || filePath.Contains("jar:"))
        {
            // Read files on Android/iOS
            using (UnityWebRequest www = UnityWebRequest.Get(filePath))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    creditText.text = www.downloadHandler.text;
                }
                else
                {
                    creditText.text = "Failed to load file: " + www.error;
                }
            }
        }
        else
        {
            // Read files on PC
            if (File.Exists(filePath))
            {
                creditText.text = File.ReadAllText(filePath);
            }
            else
            {
               creditText.text = "File not found: " + filePath;
            }
        }
        string hexColor = ColorUtility.ToHtmlStringRGB(highlightColor); 

        Debug.Log(hexColor);

        string[] highlightKeywords = { "THE ADVENTURES OF THE PIG", "GAME DEVELOPED BY", "PROGRAMMING & DESIGN", "MUSIC & SOUND EFFECTS", "ART & ASSETS", "TOOLS & ENGINE", "SPECIAL THANKS" };

        foreach (string keyword in highlightKeywords)
        {
            creditText.text = creditText.text.Replace(keyword, $"<color=#{hexColor}>{keyword}</color>");
        }

        creditText.text = creditText.text;
    }
}

