using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

namespace NavalBattle
{
     public class UserRegister : IJsonConvertible
    {
          private List<User> userData = new List<User>();

          [JsonInclude]
          public List<User> UserData
          {
               get
               {
                    return this.userData;
               }
          }

          private UserRegister()
          {
               SetUp();
          }

          private static UserRegister instance;

          public static UserRegister Instance
          {
               get
               {
                    if (instance == null)
                    {
                         instance = new UserRegister();
                    }

                    return instance;
               }
          }

          public void SetUp()
          {
               this.userData = new List<User>();
          }

          public void CreateUser(string nickName)
          {
               User user = new User(nickName);
               this.UserData.Add(user);
          }

          public void RemoveUser(User user)
          {
               if (this.UserData.Contains(user))
               {
                    throw new Exception();
               }
               this.UserData.Remove(user);
          }

          public User GetUserByNickName(Message userMessage)
          {
               User outcome = null;
               if (this.UserData.Exists(user => user.NickName == userMessage.From.Username))
               {
                    outcome = this.UserData.Find(user => user.NickName == userMessage.From.Username);
               }
               return outcome;
          }

          public string ConvertToJson(JsonSerializerOptions options)
          {
               return JsonSerializer.Serialize(this, options);
          }

          public void LoadFromJson(string json)
          {
               this.SetUp();
               User user = JsonSerializer.Deserialize<User>(json);
               JsonSerializerOptions options = new()
               {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
               };

               user = JsonSerializer.Deserialize<User>(json, options);
          }
    }
}