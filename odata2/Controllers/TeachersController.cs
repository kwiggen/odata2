using System.Collections.Generic;
using System.Web.OData;
using odata2.Models;
using System.Web.Http;
using System.Linq;

namespace odata2.Controllers
{
    public class TeachersController : ODataController
    {

        public static Teacher MR_SMITH = new Teacher(34, "Mr Smith");
        public static Teacher MRS_BROWN = new Teacher(88, "Mrs Brown");
        public static Teacher MRS_JONES = new Teacher(100, "Mrs Jones");

        [EnableQuery]
        public IList<Teacher> Get()
        {
            return getTeachers();
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            var returnMe = getTeacher(key);
            if (returnMe == null)
                return NotFound();
            else
                return Ok(returnMe);
        }

        public IHttpActionResult Post([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Teacher fc = new Teacher(121212, teacher.Name);
            return Ok<Teacher>(fc);
        }


        /////////////////////////////////////////
        /// PRIVATE
        /////////////////////////////////////////


        public static Teacher getTeacher(int id)
        {
            return TEACHERS.Where(teacher => teacher.Id == id).First();
        }


        private IList<Teacher> getTeachers()
        {
            return TEACHERS;
        }

        private static Teacher[] TEACHERS = new Teacher[]
        {
            MR_SMITH, MRS_BROWN, MRS_JONES
        };
    }
}
