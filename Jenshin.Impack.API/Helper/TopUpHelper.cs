using Jenshin.Impack.API.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jenshin.Impack.API.Output;
using Jenshin.Impack.API.Model.Request;

namespace Jenshin.Impack.API.Helper
{
    public class TopUpHelper
    {
        public static string TopUp(TopUpRequestDTO data)
        {
            string message = $"{data.Amount} Genesis Crystals has been topped up to {data.Email}";
            var users = EntityHelper.Get<MsUser>().ToList();
            var userBal = EntityHelper.Get<MsUserBalance>().ToList();

            try
            {
                users = users.Where(user => user.UserEmail.Equals(data.Email)).ToList();

                // Kalau User tidak ditemukan
                if (users.Capacity.Equals(0))
                    throw new Exception("User not found!");

                var joinedTables = users.Join(
                    userBal,
                    u => u.UserID,
                    ub => ub.UserID,
                    (u, ub) => new MsUserBalance
                    {
                        UserID = ub.UserID,
                        UserPrimogemAmount = ub.UserPrimogemAmount,
                        UserGenesisCrystalAmount = ub.UserGenesisCrystalAmount
                    }
                ).First();

                EntityHelper.Update(new MsUserBalance
                {
                    UserID = joinedTables.UserID,
                    UserPrimogemAmount = joinedTables.UserPrimogemAmount,

                    // Kalau Amount tidak dimasukkan di dalam header, ganti amount jadi 0 agar tidak ter-Update
                    UserGenesisCrystalAmount = joinedTables.UserGenesisCrystalAmount + (data.Amount ?? 0)
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
        }
    }
}