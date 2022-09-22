using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class BagStoreContextSeed
    {
        public static async Task SeedAsync(BagStoreContext db)
        {
            if (await db.Categories.AnyAsync() || await db.Brands.AnyAsync() || await db.Products.AnyAsync())
                return;

            var sho = new Category() { Name = "Shoulder Bags" };
            var bac = new Category() { Name = "Backpacks" };
            var han = new Category() { Name = "Handbags" };
            var spo = new Category() { Name = "Sports Bags" };
            await db.Categories.AddRangeAsync(sho, bac, han, spo);

            var nik = new Brand() { Name = "Nike" };
            var adi = new Brand() { Name = "Adidas" };
            var ber = new Brand() { Name = "Bershka" };
            var man = new Brand() { Name = "Mango" };
            var tom = new Brand() { Name = "Tommy Hilfiger" };
            await db.Brands.AddRangeAsync(nik, adi, ber, man, tom);

            await db.Products.AddRangeAsync(
                new Product() { Category = bac, Brand = nik, PictureUri = "01.jpg", Price = 569m, Name = "Nike Y Nk Elmntl Fa19 Backpack" },
                new Product() { Category = bac, Brand = nik, PictureUri = "02.jpg", Price = 609.90m, Name = "Nike Unisex Backpack" },
                new Product() { Category = spo, Brand = nik, PictureUri = "03.jpg", Price = 629m, Name = "Nike Nk Acdmy Team M Duff Black Football Sports Bag" },
                new Product() { Category = bac, Brand = adi, PictureUri = "04.jpg", Price = 729m, Name = "adidas Unisex Backpack Power Vi" },
                new Product() { Category = han, Brand = adi, PictureUri = "05.jpg", Price = 677.97m, Name = "adidas Women's Daily Hand Bag W Mm Tote" },
                new Product() { Category = spo, Brand = adi, PictureUri = "06.jpg", Price = 492.81m, Name = "adidas Unisex Blue Black White Sports Bag 45x23x20cm" },
                new Product() { Category = han, Brand = ber, PictureUri = "07.jpg", Price = 429.95m, Name = "Bershka Basic Tote Bag" },
                new Product() { Category = han, Brand = man, PictureUri = "08.jpg", Price = 389.99m, Name = "Mango Dark Yellow Bucket Bag" },
                new Product() { Category = sho, Brand = man, PictureUri = "09.jpg", Price = 379.99m, Name = "Mango Crossbody Bag" },
                new Product() { Category = sho, Brand = tom, PictureUri = "10.jpg", Price = 1129m, Name = "Tommy Hilfiger Polyester Navy Women Crossbody Bag" },
                new Product() { Category = spo, Brand = tom, PictureUri = "11.jpg", Price = 3325.26m, Name = "Tommy Hilfiger Men Sports Bag" },
                new Product() { Category = bac, Brand = adi, PictureUri = "12.jpg", Price = 389.67m, Name = "adidas Bag" }
                );
            await db.SaveChangesAsync();
        }
    }
}
