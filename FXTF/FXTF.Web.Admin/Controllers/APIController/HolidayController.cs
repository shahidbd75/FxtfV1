using FXTF.BusinessLayer.Admin.Classes;
using FXTF.Lib.AdminModel.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FXTF.Web.Admin.Controllers.APIController
{
    [RoutePrefix("api/Holiday")]
    public class HolidayController : ApiController
    {
        public HolidayController()
        {

        }

        /// <summary>
        /// GetAll Holiday
        /// </summary>
        /// <returns> Holiday view model list</returns>
        // GET: /api/Holiday/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<Holiday>> GetAll()
        {
            var result = await new HoliDayBL().GetAllHoliday();
            return result;
        }



        /// <summary>
        /// GetByID Holiday
        /// </summary>
        /// <returns> Holiday list by ID</returns>
        // GET: /api/Holiday/GetByID
        [Route("GetByID/{ID:long}")]
        [HttpGet]
        public async Task<Holiday> GetByID(long ID)
        {
            var result = await new HoliDayBL().GetHolidayByID(ID);
            return result;
        }


    }
}