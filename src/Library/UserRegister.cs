using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

namespace NavalBattle
{
     public class UserRegister : IJsonConvertible
    {
          private List<GameUser> userData = new List<GameUser>();

          [JsonInclude]
          public List<GameUser> UserData
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
               this.userData = new List<GameUser>();
          }

          public void CreateUser(string nickName)
          {
               GameUser user = new GameUser(nickName);
               this.userData.Add(user);
          }

          public void RemoveUser(GameUser user)
          {
               if (this.UserData.Contains(user))
               {
                    throw new Exception();
               }
               this.userData.Remove(user);
          }

          public GameUser GetUserByNickName(string nickName)
          {
               GameUser outcome = null;
               if (this.userData.Exists(user => nickName == user.NickName))
               {
                    outcome = this.userData.Find(user => nickName == user.NickName);
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
               GameUser user = JsonSerializer.Deserialize<GameUser>(json);
               JsonSerializerOptions options = new()
               {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
               };

               user = JsonSerializer.Deserialize<GameUser>(json, options);
          }
    }
}