using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ResetScript : MonoBehaviour
{
    public TMP_InputField edtUser, edtOTP, edtNewPass;
    public TMP_Text txtMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetMK(){
        StartCoroutine(ResetPassword());
        ResetPassword();
    }
    IEnumerator ResetPassword()
    {
        //Lấy giá trị từ Input Field
        string user = edtUser.text;
        string otp = edtOTP.text;
        string newpass = edtNewPass.text;
        txtMessage.text = "";

        ResetModel resetModel = new ResetModel(user, otp, newpass);

        string jsonStringRequest = JsonConvert.SerializeObject(resetModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/reset-password", "POST");
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
