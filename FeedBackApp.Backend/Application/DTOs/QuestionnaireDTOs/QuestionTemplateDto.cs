﻿using FeedBackApp.Core.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionTemplateDto
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QuestionType Type { get; set; }

        [JsonProperty("answerOptions")]
        public IList<string>? AnswerOptions { get; set; } = new List<string>();
    }
}
