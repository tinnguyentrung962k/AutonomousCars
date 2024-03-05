﻿namespace BotTournamentManagement.Data.ResponseModel
{
    public class TeamResponseModel
    {
        public string Id { get; set; }
        public string KeyId { get; set; }
        public string TeamName { get; set; }
        public HighSchoolResponseModel highSchoolResponseModel { get; set; }
        public List<PlayerResponseModel>? playerResponseModels { get; set; }
    }
}
