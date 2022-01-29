using Jenshin.Impack.API.Model;
using Binus.WS.Pattern.Entities;
using System;
using System.Linq;
using Jenshin.Impack.API.Model.Request;

namespace Jenshin.Impack.API.Helper
{
    public class PurchaseHelper
    {
        public static string PurchaseItem(PurchaseRequestDTO data)
        {
            string message = "Purchase Successful!";

            var userBalDb = EntityHelper.Get<MsUserBalance>().ToList();
            var shopItemDb = EntityHelper.Get<MsShopItem>().ToList();
            var userBal = new MsUserBalance();
            var shopItem = new MsShopItem();

            void updateUserBalance(MsUserBalance userBalance, MsShopItem shopItem)
            {
                EntityHelper.Update(new MsUserBalance()
                {
                    UserID = userBalance.UserID,
                    UserPrimogemAmount = userBalance.UserPrimogemAmount,
                    UserGenesisCrystalAmount = userBalance.UserGenesisCrystalAmount,
                });
            }

            void addToTrPurchase(Guid userId, Guid itemId, int amount)
            {
                EntityHelper.Add(new TrUserPurchase()
                {
                    UserID = userId,
                    ItemID = itemId,
                    PurchaseAmount = amount,
                });
            }

            try
            {
                userBal = userBalDb.Where(user => user.UserID == data.UserID).FirstOrDefault();
                shopItem = shopItemDb.Where(item => item.ItemID == data.ItemID).FirstOrDefault();

                // Kalau BISA Combine Primogem & Genesis Crystal
                if (shopItem.GenesisCrystalOnly.Equals(false))
                {
                    // Kalau Primogem + Genesis Crystal TIDAK CUKUP
                    if (userBal.UserPrimogemAmount + userBal.UserGenesisCrystalAmount < shopItem.ItemPrice * data.Amount)
                        return "Insufficient Funds";

                    // Kalau Primogem + Genesis Crystal CUKUP
                    else if (userBal.UserPrimogemAmount + userBal.UserGenesisCrystalAmount >= shopItem.ItemPrice * data.Amount)
                    {
                        userBal.UserGenesisCrystalAmount -= ((int)shopItem.ItemPrice * data.Amount) - userBal.UserPrimogemAmount;
                        userBal.UserPrimogemAmount = 0;
                        updateUserBalance(userBal, shopItem);
                        addToTrPurchase(data.UserID, data.ItemID, data.Amount);
                    }

                    // Kalau Primogem saja CUKUP
                    else if (userBal.UserPrimogemAmount >= shopItem.ItemPrice * data.Amount)
                    {
                        userBal.UserPrimogemAmount -= (int)shopItem.ItemPrice * data.Amount;
                        updateUserBalance(userBal, shopItem);
                        addToTrPurchase(data.UserID, data.ItemID, data.Amount);
                    }
                }

                // Kalau TIDAK BISA Combine Primogem & Genesis Crystal
                else if (shopItem.GenesisCrystalOnly.Equals(true))
                {
                    // Kalau Genesis Crystal TIDAK CUKUP
                    if (userBal.UserGenesisCrystalAmount < shopItem.ItemPrice * data.Amount)
                        return "Insufficient Funds";

                    // Kalau Genesis Crystal CUKUP
                    else
                    {
                        userBal.UserGenesisCrystalAmount -= (int)shopItem.ItemPrice * data.Amount;
                        updateUserBalance(userBal, shopItem);
                        addToTrPurchase(data.UserID, data.ItemID, data.Amount);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
        }
    }
}