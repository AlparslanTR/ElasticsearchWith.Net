﻿using System.Text.Json.Serialization;

namespace BlogUI.Models
{
    public class Blog
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; } = null!;
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
    }
}
