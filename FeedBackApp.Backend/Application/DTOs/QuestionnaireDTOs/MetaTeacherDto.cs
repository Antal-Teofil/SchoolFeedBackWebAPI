﻿using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class MetaTeacherDto
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
