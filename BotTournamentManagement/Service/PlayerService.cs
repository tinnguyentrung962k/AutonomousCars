﻿using AutoMapper;
using BotTournamentManagement.Data.Entities;
using BotTournamentManagement.Data.RequestModel.PlayModel;
using BotTournamentManagement.Data.ResponseModel;
using BotTournamentManagement.Interface.IRepository;
using BotTournamentManagement.Interface.IService;

namespace BotTournamentManagement.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        
        public PlayerService(IPlayerRepository playerRepository,ITeamRepository teamRepository, IMapper mapper) 
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }
        public void CreateNewPlayer(PlayerCreatedModel playerCreatedModel)
        {
            var existingPlayer = _playerRepository.GetAll().Where(p => p.KeyId.Equals(playerCreatedModel.KeyId)).FirstOrDefault();
            if (existingPlayer is not null)
            {
                throw new Exception("This player is existed");
            }
            var newPlayer = _mapper.Map<PlayerEntity>(playerCreatedModel);
            var teamEntity = _teamRepository.GetAll().Where(p => p.Id.Equals(newPlayer.TeamId)).FirstOrDefault();
            newPlayer.KeyId = teamEntity.KeyId + "_" + playerCreatedModel.KeyId; 
            _playerRepository.Add(newPlayer);
        }

        public void DeletePlayer(string id)
        {
            var chosenPlayer = _playerRepository.GetById(id);
            if (chosenPlayer is null)
            {
                throw new Exception("This player doesn't exited");
            }
            string deleteKeyId = chosenPlayer.KeyId + "_H";
            var deletedList = _playerRepository.GetBothActiveandInactive().Where(x => x.KeyId.Contains(deleteKeyId)).ToList();
            chosenPlayer.KeyId = (deleteKeyId + deletedList.Count()).ToString();
            _playerRepository.Update(chosenPlayer);
            _playerRepository.Delete(chosenPlayer);
        }

        public List<PlayerResponseModel> GetAllPlayers()
        {
            var playerList = _playerRepository.GetAll().OrderBy(x=> x.KeyId).ToList();
            var responsePlayerList = _mapper.Map<List<PlayerResponseModel>>(playerList);
            foreach (var playerResponse in responsePlayerList)
            {
                var teamEntity = _teamRepository.GetById(playerResponse.TeamId);
                playerResponse.TeamName = teamEntity.TeamName;
            }
            return responsePlayerList;
        }

        public PlayerResponseModel GetPlayerById(string id)
        {
            var chosenPlayer = _playerRepository.GetById(id);
            if (chosenPlayer is null)
            {
                throw new Exception("No existed player");
            }
            var responsePlayer = _mapper.Map<PlayerResponseModel>(chosenPlayer);
            var teamEntity = _teamRepository.GetById(chosenPlayer.TeamId);
            responsePlayer.TeamName = teamEntity.TeamName;
            return responsePlayer;
        }

        public List<PlayerResponseModel> GetPlayerByTeamId(string teamId)
        {
            var playerList = _playerRepository.GetAll().Where(p => p.TeamId.Equals(teamId)).OrderBy(x => x.KeyId).ToList();
            var responsePlayerList = _mapper.Map<List<PlayerResponseModel>>(playerList);
            foreach( var playerResponse in responsePlayerList)
            {
                var teamEntity = _teamRepository.GetById(playerResponse.TeamId);
                playerResponse.TeamName = teamEntity.TeamName;
            }
            return responsePlayerList;
        }

        public void UpdatePlayer(string id, PlayerUpdatedModel playerUpdateModel)
        {
            var chosenPlayer = _playerRepository.GetById(id);
            if (chosenPlayer is null) {
                throw new Exception("Player is not existed");
            }
            var existingPlayer = _playerRepository.GetAll().Where(p => p.KeyId.Equals(playerUpdateModel.KeyId) && !p.KeyId.Equals(chosenPlayer.KeyId)).FirstOrDefault();
            if (existingPlayer is not null)
            {
                throw new Exception("This player Id is existed");
            }
            _mapper.Map(playerUpdateModel, chosenPlayer);
            _playerRepository.Update(chosenPlayer);
        }
    }
}
