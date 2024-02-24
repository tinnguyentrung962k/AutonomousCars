﻿using BotTournamentManagement.Data.RequestModel.MatchModel;
using BotTournamentManagement.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace BotTournamentManagement.Controllers
{
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }
        [HttpGet]
        [Route("api/[controller]/get-all-matches")]
        public IActionResult GetMatchList()
        {
            try
            {
                return Ok(_matchService.GetAllMatches());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/get-a-match-by-id/")]
        public IActionResult GetMatchById(string id)
        {
            try
            {
                return Ok(_matchService.GetMatchById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/create-match")]
        public IActionResult CreateMatch(MatchandTeamCreatedModel matchandTeamCreatedModel)
        {
            try
            {
                _matchService.CreateNewMatch(matchandTeamCreatedModel);
                return Ok("Created Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[HttpPut]
        //[Route("api/[controller]/update-match")]
        //public IActionResult UpdateMatch(MatchandTeamUpdateModel model)
        //{
        //    try
        //    {
        //        _matchService.UpdateMatch(model);
        //        return Ok("Updated Successfully !");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        [HttpDelete]
        [Route("api/[controller]/delete-match")]
        public IActionResult DeleteMatch(string id)
        {
            try
            {
                _matchService.DeleteMatch(id);
                return Ok("Deleted Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

