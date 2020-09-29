using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var isExistProduct = productCollection.Find(p => true).Any();
            if (!isExistProduct)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    Name="Iphone X",
                    Sumary="There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...",
                    Description=@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into
                                electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                                and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageFile="product-1.png",
                    Price=650.00M,
                    Category="Smart Phone"
                },
                  new Product()
                {
                    Name="Samsung Note 20 Ultra",
                    Sumary="There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...",
                    Description=@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into
                                electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                                and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageFile="product-2.png",
                    Price=750.00M,
                    Category="Smart Phone"
                },
                    new Product()
                {
                    Name="Iphone 12",
                    Sumary="There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...",
                    Description=@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into
                                electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                                and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageFile="product-3.png",
                    Price=1650.00M,
                    Category="Smart Phone"
                },
                      new Product()
                {
                    Name="Xiaomi Mimix",
                    Sumary="There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...",
                    Description=@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into
                                electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                                and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageFile="product-4.png",
                    Price=550.00M,
                    Category="Smart Phone"
                },
                        new Product()
                {
                    Name="Huawei Mate X Pro",
                    Sumary="There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...",
                    Description=@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into
                                electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                                and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageFile="product-5.png",
                    Price=650.00M,
                    Category="Smart Phone"
                },
            };
        }
    }
}
