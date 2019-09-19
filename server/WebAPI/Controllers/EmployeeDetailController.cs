using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Handler;
using System.Linq;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /* public class EmployeeDetailController : ControllerBase
     {
         private readonly AuthenticationContext _context;

         public EmployeeDetailController(AuthenticationContext context)
         {
             _context = context;
         }*/

    public class EmployeeDetailController : ControllerBase
    {
        // private IEmployeeDetailsHandler _employeeHandler { get; set; }
        // private readonly IEmployeeDetailsHandler _handler;
        //private readonly AuthenticationContext _context;
        private IEmployeeDetailsHandler _handler;


        public EmployeeDetailController(IEmployeeDetailsHandler handler)
        {
            _handler = handler;
        }

        // GET: api/EmployeeDetail
        // [HttpGet]
        /* public IEnumerable<EmployeeDetail> GetEmployeeDetails()
         {
             return _context.EmployeeDetails;
         }*/

        // GET: api/EmployeeDetail
        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails()
        {
            // var employeeDetails = await _handler.GetEmployeeDetailsAsync();
            // return (employeeDetails);
            try
            {
                var posts = await _handler.GetEmployeeDetails();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/EmployeeDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            //EmployeeDetailHandler employeeHandler = new EmployeeDetailHandler(_context);
            EmployeeDetail employeeDetail = await _handler.GetEmployeeDetail(id);

            if (employeeDetail == null)
            {
                return NotFound();
            }

            return Ok(employeeDetail);
        }

        //// PUT: api/EmployeeDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDetail([FromRoute] int id, [FromBody] EmployeeDetail employeeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeDetail.EId)
            {
                return BadRequest();
            }

            //_context.Entry(employeeDetail).State = EntityState.Modified;
            //var x = await _handler.PutEmployeeDetailAsync(id,employeeDetail);

            try
            {
                var x = await _handler.PutEmployeeDetailAsync(id, employeeDetail);

               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_handler.EmployeeDetailExists(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        //// POST: api/EmployeeDetail
       [HttpPost]
       public async Task<IActionResult> PostEmployeeDetail([FromBody] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var eId = await _handler.PostEmployeeDetail(employeeDetail);
                    if (eId >= 0)
                    {
                        //_context.EmployeeDetails.Add(employeeDetail);
                        //await _context.SaveChangesAsync();
                        //return Ok(eId);
                        return CreatedAtAction("GetEmployeeDetail", new { id = employeeDetail.EId }, employeeDetail);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
        
         // DELETE: api/EmployeeDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDetail([FromRoute] int id)
        {
            int result = 0;
            if (id == 0)
            {
                return BadRequest();
            }

            // var employeeDetail = await _handler.EmployeeDetails.FindAsync(id);
            // if (employeeDetail == null)
            // {
            //    return NotFound();
            // }

            // _context.EmployeeDetails.Remove(employeeDetail);
            // await _context.SaveChangesAsync();
            try
            {
                result = await _handler.DeleteEmployeeDetail(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
          //return Ok(employeeDetail);
        }

      
    }
}