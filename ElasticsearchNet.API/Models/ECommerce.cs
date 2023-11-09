﻿using System.Text.Json.Serialization;

namespace ElasticsearchNet.API.Models.ECommerce
{
    public class ECommerce
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("customer_first_name")]
        public string CustomerFirstName { get; set; } = null!;
        [JsonPropertyName("customer_last_name")]
        public string CustomerLastName { get; set; } = null!;
        [JsonPropertyName("customer_full_name")]
        public string CustomerFullName { get; set; } = null!;
        [JsonPropertyName("taxful_total_price")]
        public double TaxFulTotalPrice { get; set; }
        [JsonPropertyName("category")]
        public string[]? Category { get; set; } = null!;
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("products")]
        public Product[] Products { get; set; }
        [JsonPropertyName("geoip.city_name")]
        public string CityName { get; set; } = null!;
    }

    public class Product
    {
        [JsonPropertyName("product_id")]
        public long Id { get; set; }
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = null!;
    }
}