﻿using BotTournamentManagement.Data.Entities;
using BotTournamentManagement.Data.RequestModel;
using BotTournamentManagement.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace BotTournamentManagement.Interface.IService
{
    public interface ITournamentService
    {
        List<TournamentResponseModel> GetAllTournament();
        void CreateNewTournament(TournamentCreatedModel tournamentCreatedModel);
        void UpdateTournament(string keyId, [FromForm] TournamentCreatedModel tournamentCreatedModel);
        void DeleteTournament(string id);
        TournamentResponseModel GetTournamentById(string id);
    }
}
