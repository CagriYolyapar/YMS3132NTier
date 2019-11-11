using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Project.MODEL.Entities;

namespace Project.DAL.StrategyPattern
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {

            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();

                context.AppUsers.Add(ap);




            }

            context.SaveChanges();

            for (int i = 1; i < 11; i++)
            {
                AppUserDetail apd = new AppUserDetail();

                apd.ID = i; //Birebir ilişkisi oldugundan dolayı id'leri bu sekilde verdik..

                apd.FirstName = new Name("tr").FirstName();

                apd.LastName = new Name("tr").LastName();

                apd.Address = new Address("tr").Locale;

                context.Proflies.Add(apd);

               

            }

            context.SaveChanges();

            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();

                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(100);

                for (int x = 0; x < 30; x++)
                {
                    Product p = new Product();

                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.UnitsInStock = rnd.Next(5, 500);
                    p.ImagePath = new Images().Nightlife();
                    c.Products.Add(p);


                    
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }


        }
    }
}
