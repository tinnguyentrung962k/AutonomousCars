﻿using AutoMapper;
using BotTournamentManagement.Data.Entities;
using BotTournamentManagement.Data.RequestModel;
using BotTournamentManagement.Data.ResponseModel;
using BotTournamentManagement.Interface.IRepository;
using BotTournamentManagement.Interface.IService;
using BotTournamentManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BotTournamentManagement.Service
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMapper _mapper;
        public TournamentService(ITournamentRepository tournamentRepository, IMapper mapper) 
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        public void CreateNewTournament(TournamentCreatedModel tournamentCreatedModel)
        {
            var tournamentList = _tournamentRepository.GetAll();
            foreach (TournamentEntity tournament in tournamentList)
            {
                if (tournament.KeyId.Equals(tournamentCreatedModel.KeyId))
                {
                    throw new Exception("This tournament Id is existed !");
                }
            }
            var tournamentEntity = _mapper.Map<TournamentEntity>(tournamentCreatedModel);
            _tournamentRepository.Add(tournamentEntity);
        }

        public void DeleteTournament(string id)
        {
            var chosenTournament = _tournamentRepository.GetById(id);
            if (chosenTournament is null)
            {
                throw new Exception("This tournament is not existed");
            }
            else
            {
                _tournamentRepository.Delete(chosenTournament);
            }
        }

        public List<TournamentResponseModel> GetAllTournament()
        {
            var tournamentList = _tournamentRepository.GetAll();
            if (!tournamentList.Any())
            {
                throw new Exception("This tournament list is empty");
            }
            var responseTnmList = _mapper.Map<List<TournamentResponseModel>>(tournamentList);
            return responseTnmList;
        }

        public TournamentResponseModel GetTournamentById(string id)
        {
            var chosenTournament = _tournamentRepository.GetById(id);
            if (chosenTournament is null)
            {
                throw new Exception("This tournament is not existed");
            }
            var responseTournament = _mapper.Map<TournamentResponseModel>(chosenTournament);
            return responseTournament;
        }

        public void UpdateTournament(string id, [FromForm] TournamentUpdateModel tournamentUpdateModel)
        {
            var chosenTournament = _tournamentRepository.GetById(id);
            if (chosenTournament is null)
            {
                throw new Exception("This map is not existed");
            }
            var tournamentList = _tournamentRepository.GetAll();
            foreach (var tournament in tournamentList)
            {
                if (tournament.KeyId.Equals(tournamentUpdateModel.KeyId))
                {
                    throw new Exception("This map ID existed");
                }
            }
            _mapper.Map(tournamentUpdateModel, chosenTournament);
            _tournamentRepository.Update(chosenTournament);
        }
    }
}
