﻿namespace ShoeStore.Models.ProductModels
{
    public class ProductMD
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public int? CategoryId { get; set; }

        public string? Image { get; set; }

        public decimal Price { get; set; }
    }
}
