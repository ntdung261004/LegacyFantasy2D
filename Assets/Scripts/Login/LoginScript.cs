using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void DN(){
        StartCoroutine(Login());
        Login();
    }
    IEnumerator Login()
    {
        //Lấy giá trị từ Input Field
        string user = edtUser.text;
        string pass = edtPass.text;
        txtMessage.text = "";

        LoginModel loginModel = new LoginModel(user, pass);
        string jsonStringRequest = JsonConvert.SerializeObject(loginModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            UserMessageModel message = JsonConvert.DeserializeObject<UserMessageModel>(jsonString);
            txtMessage.text = message.notification;
        }
        request.Dispose();
    }
}
