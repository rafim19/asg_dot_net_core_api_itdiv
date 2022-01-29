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
    public class ItemHelper
    {
        public static int AddNewItem(MsShopItem data)
        {
            if (data.ItemPrice == null || data.GenesisCrystalOnly == null)
                return 0;

            try
            {
                EntityHelper.Add(new MsShopItem() 
                {
                    ItemName = data.ItemName,
                    ItemDescription = data.ItemDescription,
                    ItemPrice = data.ItemPrice,
                    GenesisCrystalOnly = data.GenesisCrystalOnly,
                    Stsrc = "A", // Pada saat awal memasukkan barang, Defaultnya 'A' (Active)
                    CreatedDt = DateTime.Now,
                    CreatedBy = "2440112891"
                });
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        public static int UpdateItem(MsShopItem data, string NIM)
        {
            var returnValue = new List<MsShopItem>();
            var shopItemDb = EntityHelper.Get<MsShopItem>().ToList();
            var temp = new MsShopItem();

            try
            {
                temp = shopItemDb.Where(item => item.ItemID == data.ItemID).Single();
                EntityHelper.Update(
                    new MsShopItem()
                    {
                        ItemID = data.ItemID,

                        // Kalau ItemName yang baru tidak di set di Body, maka pakai ItemName yang lama
                        ItemName = data.ItemName ?? temp.ItemName,
                        ItemDescription = data.ItemDescription ?? temp.ItemDescription,
                        ItemPrice = data.ItemPrice ?? temp.ItemPrice,
                        GenesisCrystalOnly = data.GenesisCrystalOnly ?? temp.GenesisCrystalOnly,
                        Stsrc = data.Stsrc ?? temp.Stsrc,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = NIM,
                        CreatedDt = temp.CreatedDt,
                        CreatedBy = temp.CreatedBy
                    });
            }
            catch
            {
                return 0;
            }

            return 1;
        }
    }
}