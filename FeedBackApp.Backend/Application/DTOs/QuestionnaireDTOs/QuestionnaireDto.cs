﻿using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionnaireDto
    {
        [JsonProperty("surveyId")]
        public string SurveyId { get; set; } = string.Empty;

        [JsonProperty("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonProperty("studentEmail")]
        public string StudentEmail { get; set; } = string.Empty;

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonProperty("questionnaireResult")]
        public List<QuestionAnswerDto> QuestionnaireResults { get; set; } = new();
    }
}
