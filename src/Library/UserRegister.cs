using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

namespace NavalBattle
{
     /// <summary>
     /// Clase donde se registran y guardan los 
     /// </summary>
     public class UserRegister
    {
          private List<GameUser> userData = new List<GameUser>();

          [JsonInclude]
          /// <summary>
          /// Gets de la lista de usuarios registrados.
          /// </summary>
          /// <value></value>
          public List<GameUser> UserData
          {
               get
               {
                    return this.userData;
               }
               set
               {
                    this.userData = value;
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

          /// <summary>
          /// Método que aplica el patrón Creator para crear y añadir un usuario a la lista de usuarios.
          /// </summary>
          /// <param name="nickName">Nombre del usuario.</param>
          /// <param name="id">Id del usuario.</param>
          public void CreateUser(string nickName, long id)
          {
               GameUser user = new GameUser(nickName, id);
               this.userData.Add(user);
          }

          /// <summary>
          /// Por la ley de demeter y para evitar el alto acoplamiento se crea este método para verificar si un usarios 
          /// está en la lista de usuarios y que otro objeto no deba de conocer todas la conexiones internas.
          /// </summary>
          /// <param name="user"></param>
          /// <returns></returns>
          public bool ContainsUser(GameUser user)
          {
               if(this.userData.Contains(user))
               {
                    return true;
               }
               else
               {
                    return false;
               }
          }

          /// <summary>
          /// Por la ley de demeter y para evitar el alto acoplamiento se crea este método para añadir usuarios a la lista
          /// de usuarios y además que otro objeto no deba de conocer todas la conexiones internas.
          /// </summary>
          /// <param name="item"></param>
          public void Add(GameUser item)
          {
               this.userData.Add(item);
          }

          /// <summary>
          /// Encuentra un User en la lista de Users por su nombre.
          /// </summary>
          /// <param name="nickName">Nombre del usuario.</param>
          /// <returns>GameUser.</returns>
          public GameUser GetUserByNickName(string nickName)
          {
               GameUser outcome = null;
               if (this.userData.Exists(user => nickName == user.NickName))
               {
                    outcome = this.userData.Find(user => nickName == user.NickName);
               }
               return outcome;
          }

          /// <summary>
          /// Encuentra un User en la lista de Users por su id.
          /// </summary>
          /// <param name="chatId">Id del chat por el que se habla con el usuario.</param>
          /// <returns>GameUser.</returns>
          public GameUser GetUserById(long chatId)
          {
               GameUser outcome = null;
               if (this.userData.Exists(user => chatId == user.ChatId))
               {
                    outcome = this.userData.Find(user => chatId == user.ChatId);
               }
               return outcome;
          }
    }
}