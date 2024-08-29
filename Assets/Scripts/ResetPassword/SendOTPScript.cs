using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendOTPScript : MonoBehaviour
{
    public TMP_InputField edtUser;
    public TMP_Text txtMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuiOTP(){
        StartCoroutine(SendOTP());
        SendOTP();
    }


    IEnumerator SendOTP()
    {
        //Lấy giá trị từ Input Field
        string user = edtUser.text;
        txtMessage.text = "";

        SendOTPModel sendOTP = new SendOTPModel(user);

        string jsonStringRequest = JsonConvert.SerializeObject(sendOTP);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/send-otp", "POST");
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
            MessageModel message = JsonConvert.DeserializeObject<MessageModel>(jsonString);
            txtMessage.text = message.notification;

        }
        request.Dispose();
    }

}
