using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;
using System.Collections.Generic;

public class ThreadCom : MonoBehaviour
{
    private float seconds = 0;
    private bool finished = true;

    private string url = "http://192.168.47.66:8080";

    private string nomeJogador = "";

    void Start()
    {

    }

    IEnumerator atirar(string nome)
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "/restserver-game/services/multiplayer/atirar/" + nome);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
    }

    IEnumerator consultar(long momento)
    {
        finished = false;
        UnityWebRequest www = UnityWebRequest.Get(url + "/restserver-game/services/multiplayer/consultar/" + momento);
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

            //Tiro[] statArray = JsonConvert.DeserializeObject<Tiro[]>(json);
            Tiro[] tiros = JsonHelper.FromJson<Tiro>(json);
            Debug.Log(tiros.Length);
            foreach(Tiro tiro in tiros)
            {
                Debug.Log(tiro.nome + " - " + tiro.momento);
            }
        }
        finished = true;
    }

    void Update()
    {
        seconds += Time.deltaTime;
        if(seconds > 2 && finished)
        {
            seconds = 0;
            StartCoroutine(consultar(0));
        }
    }

    void OnGUI()
    {
        nomeJogador = GUI.TextField(new Rect(20, 150, 400, 100), nomeJogador, 25);

        if (GUI.Button(new Rect(20, 40, 200, 100), "Atirar"))
        {
            StartCoroutine(atirar(nomeJogador));
            Debug.Log(nomeJogador + " atirou");
        }
    }
}
