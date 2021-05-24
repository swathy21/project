using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CafeAPI.Models;
using CafeAPI.Services;


namespace CafeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CafesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                CafeService abcd = new CafeService();
                List<Cafe> cafes = abcd.GetAllcafes();
                if(cafes.Count <= 0)
                {
                    return NotFound("No cafes exist");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(cafes);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpGet("{id}")]
        //[HttpGet]
        public IActionResult Get([FromRoute] string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Cafe.IscafeIdValid(id, out int cafeId, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }

                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                CafeService CafeService = new CafeService();
                Cafe cafe = CafeService.GetCafe(cafeId);
                if(cafe == null)
                {
                    return NotFound("cafe with Cafe Id - " + cafeId + " does not exist.");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(cafe);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Cafe updCafe)
        {
            try
            {
                //1. Validation
                bool res = Cafe.IscafeValid(updCafe, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                CafeService sda = new CafeService();
                int numRows = sda.UpdateCafe(updCafe);
                if (numRows == 0)
                {
                    return BadRequest("Invalid Cafe. Cannot Insert.");
                }
                //3. Return Data
                return Ok(updCafe);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cafe newCafe)
        {
            try
            {
                //1. Validation
                bool res = Cafe.IscafeValid(newCafe, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                CafeService sda = new CafeService();
                // int numOfRows = sda.InsertStudent(newStudent);
                int newCafeId = sda.InsertCafe(newCafe);
                if (newCafeId <= 0)
                {
                    return BadRequest("Invalid Cafe. Cannot Insert.");
                }
                //3. Return Data
                newCafe.CafeId = newCafeId;
                return Ok(newCafe);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
       public IActionResult Delete([FromRoute]string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Cafe.IscafeIdValid(id, out int cafeId, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }
                 // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                CafeService CafeService = new CafeService();
                bool numOfRows = CafeService.DeleteCafe(cafeId);
                if(numOfRows == false)
                {
                    return NotFound("Cafe with Cafe Id - " + cafeId + " does not exist.");
}

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(cafeId);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }
                
        private void LogError(Exception ex)
        {
            //Do Something to Log an Error
        }
    }
}
