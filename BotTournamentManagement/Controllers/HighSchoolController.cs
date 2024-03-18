﻿using BotTournamentManagement.Constant;
using BotTournamentManagement.Data.RequestModel.HighSchoolModel;
using BotTournamentManagement.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BotTournamentManagement.Controllers
{
    [ApiController]
    public class HighSchoolController : ControllerBase
    {
        private readonly IHighSchoolService _highSchoolService;
        public HighSchoolController(IHighSchoolService highSchoolService)
        {
            _highSchoolService = highSchoolService;
        }
        [HttpGet]
        [Route(WebApiEndpoint.HighSchool.GetAllHighSchool)]
        public IActionResult GetHighSchoolList()
        {
            try
            {
                return Ok(_highSchoolService.GetListHighSchools());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.HighSchool.CreateHighSchool)]
        public IActionResult CreateNewSchool(HighSchoolCreatedModel highSchoolCreatedModel)
        {
            try
            {
                _highSchoolService.AddSchool(highSchoolCreatedModel);
                return Ok("Created Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route(WebApiEndpoint.HighSchool.GetSingleHighSchool)]
        public IActionResult GetSchoolById(string id)
        {
            try
            {
                return Ok(_highSchoolService.GetHighSchoolById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.HighSchool.UpdateHighSchool)]
        public IActionResult UpdateASchool(string id, HighSchoolUpdateModel highSchoolUpdateModel)
        {
            try
            {
                _highSchoolService.UpdateSchool(id, highSchoolUpdateModel);
                return Ok("Updated Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.HighSchool.DeleteHighSchool)]
        public IActionResult DeleteSchool(string id)
        {
            try
            {
                _highSchoolService.DeleteSchool(id);
                return Ok("Deleted Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
