﻿using AutoMapper;
using BotTournamentManagement.Data.Entities;
using BotTournamentManagement.Data.RequestModel.TournamentModel;
using BotTournamentManagement.Data.ResponseModel;
using BotTournamentManagement.Interface.IRepository;
using BotTournamentManagement.Interface.IService;

namespace BotTournamentManagement.Service
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        public TournamentService(ITournamentRepository tournamentRepository, IMapper mapper, IMatchRepository matchRepository) 
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
            _matchRepository = matchRepository;
        }

        public void CreateNewTournament(TournamentCreatedModel tournamentCreatedModel)
        {
            var tournamentList = _tournamentRepository.GetAll().ToList();
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
                var matchInTournament = _matchRepository.GetAll().Where(x => x.TournamentId.Equals(id)).ToList();
                if (matchInTournament.Any())
                {
                    throw new Exception("This tournament contains matches data, can't be deleted.");
                }
                string deleteKeyId = chosenTournament.KeyId + "_H";
                var deletedList = _tournamentRepository.GetBothActiveandInactive().Where(x => x.KeyId.Contains(deleteKeyId)).ToList();
                chosenTournament.KeyId = (deleteKeyId + deletedList.Count()).ToString();
                _tournamentRepository.Update(chosenTournament);
                _tournamentRepository.Delete(chosenTournament);
            }
        }

        public List<TournamentResponseModel> GetAllTournament()
        {
            var tournamentList = _tournamentRepository.GetAll().OrderByDescending(x=>x.KeyId).ToList();
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

        public void UpdateTournament(string id, TournamentUpdateModel tournamentUpdateModel)
        {
            var chosenTournament = _tournamentRepository.GetById(id);
            if (chosenTournament is null)
            {
                throw new Exception("This tournament is not existed");
            }
            var tournamentList = _tournamentRepository.GetAll().ToList();
            foreach (var tournament in tournamentList)
            {
                if (tournament.KeyId.Equals(tournamentUpdateModel.KeyId) && !tournament.KeyId.Equals(chosenTournament.KeyId))
                {
                    throw new Exception("This tournament ID existed");
                }
            }
            _mapper.Map(tournamentUpdateModel, chosenTournament);
            _tournamentRepository.Update(chosenTournament);
        }
    }
}
