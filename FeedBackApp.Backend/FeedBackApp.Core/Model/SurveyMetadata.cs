﻿
using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class SurveyMetadata
    {
        public string Id { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public IList<StudentSet> StudentSets { get; set; } = new List<StudentSet>();

        public IList<QuestionTemplate> QuestionnaireTemplate { get; set; } = new List<QuestionTemplate>();

        public IList<MetaTeacher> Teachers { get; set; } = new List<MetaTeacher>();

        public IList<QuestionnaireCreationParam> CreationParams { get; set; } = new List<QuestionnaireCreationParam>();
    }
}
