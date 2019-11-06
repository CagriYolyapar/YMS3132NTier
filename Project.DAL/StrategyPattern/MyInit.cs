using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            //istendigi takdirde Veritabanı olusturulurken baslangıc verileri icin buraya kod yazılabilir.
        }
    }
}
