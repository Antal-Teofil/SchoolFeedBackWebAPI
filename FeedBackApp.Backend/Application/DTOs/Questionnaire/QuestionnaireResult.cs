﻿
using Newtonsoft.Json;

namespace Application.DTOs.Questionnaire
{
    public class QuestionnaireResult
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; } = string.Empty;

        [JsonProperty("answer")]
        public string Answer { get; set; } = string.Empty;
    }
}
