using Jenshin.Impack.API.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jenshin.Impack.API.Output;
using System.Web.Http;
using System.Net.Http;

namespace Jenshin.Impack.API.Helper
{
    public class UserHelper
    {
        public static List<User> GetAllUser()
        {
            var returnValue = new List<User>();
            var Users = EntityHelper.Get<MsUser>();

            try
            {
                returnValue = Users.Select(x => new User { 
                    Name = x.UserName, AdventureRank = x.UserAdventureRank, Email = x.UserEmail, Signature = x.UserSignature 
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }


        public static List<SpecificUser> GetSpecificUser(string Email, string Username)
        {
            var returnValue = new List<SpecificUser>();
            var users = EntityHelper.Get<MsUser>().ToList();
            var usersBalance = EntityHelper.Get<MsUserBalance>().ToList();

            try
            {
                users = users.Where(user => user.UserName == Username || user.UserEmail == Email).ToList();
                returnValue = users.Join(
                    usersBalance,
                    u => u.UserID,
                    ub => ub.UserID,
                    (u, ub) => new SpecificUser
                    {
                        ID = u.UserID,
                        Name = u.UserName,
                        Primogem = ub.UserPrimogemAmount,
                        GenesisCrystal = ub.UserGenesisCrystalAmount
                    }).ToList();

                // Kalau Kosong (User tidak ditemukan)
                if (returnValue.Count().Equals(0))
                {
                    throw new Exception("404-Account not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }
    }
}