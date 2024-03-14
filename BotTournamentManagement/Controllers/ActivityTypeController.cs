﻿using BotTournamentManagement.Constant;
using BotTournamentManagement.Data.RequestModel.ActivityModel;
using BotTournamentManagement.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BotTournamentManagement.Controllers
{
    [ApiController]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeService _activityTypeService;
        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }
        [HttpGet]
        [Route(WebApiEndpoint.ActivityType.GetAllActivity)]
        public IActionResult GetActivityTypeList()
        {
            try
            {
                return Ok(_activityTypeService.GetAllActivityTypes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route(WebApiEndpoint.ActivityType.GetSingleActivity)]
        public IActionResult GetActivityTypeById(string id)
        {
            try
            {
                return Ok(_activityTypeService.GetActivityTypeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.ActivityType.CreateActivity)]
        public IActionResult CreateActivityType(ActivityTypeCreatedModel activityTypeCreatedModel)
        {
            try
            {
                _activityTypeService.CreateNewActivityType(activityTypeCreatedModel);
                return Ok("Created Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.ActivityType.UpdateActivity)]
        public IActionResult UpdateActivityType(string id, ActivityTypeUpdateModel model)
        {
            try
            {
                _activityTypeService.UpdateActivityType(id, model);
                return Ok("Updated Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Organizer")]
        [Route(WebApiEndpoint.ActivityType.DeleteActivity)]
        public IActionResult DeleteActivityType(string id)
        {
            try
            {
                _activityTypeService.DeleteActivityType(id);
                return Ok("Deleted Successfully !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

