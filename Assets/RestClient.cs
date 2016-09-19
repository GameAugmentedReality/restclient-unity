using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class RestClient : MonoBehaviour {

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/restserver-game/services/example/listItems");
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show json results as text
            string json = www.downloadHandler.text;
            Debug.Log(json);

            // Or convert result from json
            Item item = JsonUtility.FromJson<Item>(json);
            Debug.Log("Name: " + item.name);
            Debug.Log("Value: " + item.value);
        }
    }

}
