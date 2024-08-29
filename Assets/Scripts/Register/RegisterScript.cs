using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterScript : MonoBehaviour
{

    public TMP_InputField edtUser, edtPass, edtRePass;
    public TMP_Text txtMessage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DangKy(){
        StartCoroutine(Register());
        Register();
    }
    IEnumerator Register()
    {
        //Lấy giá trị từ Input Field
        string user = edtUser.text;
        string pass = edtPass.text;
        string repass = edtRePass.text;
        txtMessage.text = "";

        if(pass.Equals(repass)){

            UserRegisterModel userRegister = new UserRegisterModel(user, pass);
            //Gọi API 
            string jsonStringRequest = JsonConvert.SerializeObject(userRegister);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/register", "POST");
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
        }else{
            //Hiển thị thông báo    
            txtMessage.text = "Mật khẩu không trùng nhau";
        }      
    }
}
