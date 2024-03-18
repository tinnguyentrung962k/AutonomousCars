﻿using AutoMapper;
using BotTournamentManagement.Data.Entities;
using BotTournamentManagement.Data.RequestModel.MatchModel;
using BotTournamentManagement.Data.ResponseModel;
using BotTournamentManagement.Interface.IRepository;
using BotTournamentManagement.Interface.IService;

namespace BotTournamentManagement.Service
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        private readonly ITeamInMatchRepository _teamInMatchRepository;
        private readonly IMapRepository _mapRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamActivityRepository _teamActivityRepository;
        public MatchService(IMatchRepository matchRepository, IMapper mapper, ITeamInMatchRepository teamInMatchRepository, IMapRepository mapRepository, IRoundRepository roundRepository, ITournamentRepository tournamentRepository,ITeamActivityRepository teamActivityRepository)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
            _teamInMatchRepository = teamInMatchRepository;
            _mapRepository = mapRepository;
            _roundRepository = roundRepository;
            _tournamentRepository = tournamentRepository;
            _teamActivityRepository = teamActivityRepository;
        }

        public void CreateNewMatch(MatchCreatedModel matchCreatedModel)
        {
            var matchEntity = _mapper.Map<MatchEntity>(matchCreatedModel);
            var tournamentEntity = _tournamentRepository.GetById(matchCreatedModel.TournamentId);
            var roundEntity = _roundRepository.GetById(matchCreatedModel.RoundId);
            matchEntity.KeyId = tournamentEntity.KeyId + "_" + roundEntity.RoundName + "_" + matchCreatedModel.KeyId ;
            var matchList = _matchRepository.GetAll().ToList();
            foreach (var match in matchList)
            {
                if (match.KeyId.Equals(matchEntity.KeyId))
                {
                    throw new Exception("This match has been created !");
                }
            }
            _matchRepository.Add(matchEntity);
        }

        public void DeleteMatch(string id)
        {
            var chosenMatch = _matchRepository.GetById(id);
            if (chosenMatch is null)
            {
                throw new Exception("This match is not existed");
            }
            else
            {                
                var teamInMatchList = _teamInMatchRepository.GetAll().Where(x => x.MatchId == id).ToList();
                foreach (var teamInMatch in teamInMatchList)
                {
                    var teamActivitiesList = _teamActivityRepository.GetAll().Where(x => x.TeamInMatchId == teamInMatch.Id).ToList();
                    foreach (var teamActivity in teamActivitiesList)
                    {
                        _teamActivityRepository.Delete(teamActivity);
                    }
                    _teamInMatchRepository.Delete(teamInMatch);
                }
                string deleteKeyId = chosenMatch.KeyId + "_H";
                var deletedList = _matchRepository.GetBothActiveandInactive().Where(x => x.KeyId.Contains(deleteKeyId)).ToList();
                chosenMatch.KeyId = (deleteKeyId + deletedList.Count()).ToString();
                _matchRepository.Update(chosenMatch);
                _matchRepository.Delete(chosenMatch);
            }
        }

        public List<MatchResponseModel> GetAllMatches()
        {
            var matchList = _matchRepository.GetAll().OrderByDescending(x => x.KeyId).ToList();
            var responseMatchList = _mapper.Map<List<MatchResponseModel>>(matchList);
            foreach (var match in responseMatchList) {
                var mapEntity = _mapRepository.GetById(match.MapId);
                match.MapName = mapEntity.MapName;

                var roundEntity = _roundRepository.GetById(match.RoundId);
                match.RoundName = roundEntity.RoundName;

                var tournamentEntity = _tournamentRepository.GetById(match.TournamentId);
                match.TournamentName = tournamentEntity.TournamentName;
            }
            return responseMatchList;
        }

        public MatchResponseModel GetMatchById(string id)
        {
            var chosenMatch = _matchRepository.GetById(id);
            if (chosenMatch is null)
            {
                throw new Exception("This match is not existed");
            }
            var responseMatch = _mapper.Map<MatchResponseModel>(chosenMatch);
            var mapEntity = _mapRepository.GetById(responseMatch.MapId);
            responseMatch.MapName = mapEntity.MapName;

            var roundEntity = _roundRepository.GetById(responseMatch.RoundId);
            responseMatch.RoundName = roundEntity.RoundName;

            var tournamentEntity = _tournamentRepository.GetById(responseMatch.TournamentId);
            responseMatch.TournamentName = tournamentEntity.TournamentName;
            return responseMatch;
        }

        public List<MatchResponseModel> GetMatchesInTournament(string tournamentId)
        {
            var matchList = _matchRepository.GetAll().Where(p => p.TournamentId.Equals(tournamentId)).OrderBy(x => x.KeyId).ToList();
            var responseMatchList = _mapper.Map<List<MatchResponseModel>>(matchList);
            foreach (var match in responseMatchList)
            {
                var mapEntity = _mapRepository.GetById(match.MapId);
                match.MapName = mapEntity.MapName;

                var roundEntity = _roundRepository.GetById(match.RoundId);
                match.RoundName = roundEntity.RoundName;

                var tournamentEntity = _tournamentRepository.GetById(match.TournamentId);
                match.TournamentName = tournamentEntity.TournamentName;
            }
            return responseMatchList;
        }

        public void UpdateMatch(string id, MatchUpdateModel matchUpdateModel)
        {
            var chosenMatch = _matchRepository.GetById(id);
            if (chosenMatch is null)
            {
                throw new Exception("This match is not existed");
            }
            _mapper.Map(matchUpdateModel, chosenMatch);
            _matchRepository.Update(chosenMatch);
        }
    }
}
