using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

namespace NavalBattle
{
     public class UserRegister : IJsonConvertible
    {
          private static List<GameUser> userData = new List<GameUser>();

          [JsonInclude]
          public List<GameUser> UserData
          {
               get
               {
                    return userData;
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
               userData = new List<GameUser>();
          }

          public void CreateUser(string nickName, long id)
          {
               GameUser user = new GameUser(nickName, id);
               userData.Add(user);
          }

          /// <summary>
          /// Remueve un usuario de la lista de usuarios.
          /// </summary>
          /// <param name="user"></param>
          public void RemoveUser(GameUser user)
          {
               if (UserData.Contains(user))
               {
                    throw new Exception();
               }
               userData.Remove(user);
          }

          /// <summary>
          /// Encuentra un User en la lista de Users por su nombre.
          /// </summary>
          /// <param name="nickName"></param>
          /// <returns></returns>
          public GameUser GetUserByNickName(string nickName)
          {
               GameUser outcome = null;
               if (userData.Exists(user => nickName == user.NickName))
               {
                    outcome = userData.Find(user => nickName == user.NickName);
               }
               return outcome;
          }

          /// <summary>
          /// Encuentra un User en la lista de Users por su id.
          /// </summary>
          /// <param name="chatId"></param>
          /// <returns></returns>
          public GameUser GetUserById(long chatId)
          {
               GameUser outcome = null;
               if (userData.Exists(user => chatId == user.ChatId))
               {
                    outcome = userData.Find(user => chatId == user.ChatId);
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