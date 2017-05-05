using System.Collections.Generic;
using System.Web.OData;
using odata2.Models;
using System.Web.Http;
using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Web.OData.Routing;

namespace odata2.Controllers
{
    public class CoursesController : ODataController
    {
        [EnableQuery]
        public IList<Course> Get()
        {
            return getCourses(999);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            var returnMe = getCourse(key);
            if (returnMe == null)
                return NotFound();
            else
                return Ok(returnMe);
        }

        public IHttpActionResult Post([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //add an int to our Course
            course.Id = GetRandomNumber(0, int.MaxValue - 1);
            COURSES.Add(course);
            return Ok<Course>(course);
        }

        [EnableQuery]
        //[ODataRoute("Courses({key})/Teacher")]
        public IHttpActionResult GetTeacher([FromODataUri] int key)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();
            else
                return Ok(course.Teacher);
        }

        [EnableQuery]
        //[ODataRoute("courses({key})/resources")]
        public IHttpActionResult GetResources([FromODataUri] int key)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();
            else
                return Ok(course.Resources);
        }

        [EnableQuery]
        [ODataRoute("courses({key})/odata2.Models.externalCourse/location")]
        [ODataRoute("courses({key})/odata2.Models.externalCourse/location/$ref")]
        public IHttpActionResult GetLocation([FromODataUri] int key)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();
            else
            {
                ExternalCourse ext = course as ExternalCourse;
                if (ext == null)
                    return NotFound();
                else
                    return Ok(ext.Location);
            }
        }

        [EnableQuery]
        [ODataRoute("courses({key})/resources({key2})/wrappedResource")]
        public IHttpActionResult GetWrappedResource([FromODataUri] int key, [FromODataUri] int key2)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();
            else
            {
                ResourceWrapper returnMe = course.Resources.Where(x => x.Id == key2).First();
                if (returnMe == null)
                    return NotFound();
                else
                    return Ok(returnMe.WrappedResource);
            }
        }

        [AcceptVerbs("PUT")]
        public IHttpActionResult CreateRef([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();

            switch (navigationProperty)
            {
                case "teacher":
                    course.Teacher = TeachersController.MRS_JONES;
                    break;
                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            return StatusCode(HttpStatusCode.NoContent);
      
        }

        [AcceptVerbs("POST")]
        [ODataRoute("courses({key})/resources")]
        public IHttpActionResult PostResourceWrapper([FromODataUri] int key, [FromBody] ResourceWrapper wrap)
        {
            var course = getCourse(key);
            if (course == null)
                return NotFound();

            wrap.Id = GetRandomNumber(0, int.MaxValue - 1);

            course.Resources.Add(wrap);

            return StatusCode(HttpStatusCode.NoContent);

        }



        /////////////////////////////////////////
        /// PRIVATE
        /////////////////////////////////////////

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        private Course getCourse(int id)
        {
            return COURSES.Where(course => course.Id == id).First();
        }


        private IList<Course> getCourses(int id)
        {
            return COURSES;
        }

        private static Course MATH = CREATE_MATH();
        private static Course SCIENCE = CREATE_SCIENCE();
        private static Course SOCIAL_STUDIES = CREATE_SOCIAL_STUDIES();



        private static List<Course> COURSES = initCourses();

        
        private static Course CREATE_MATH()
        {
            Course returnMe = new Course(100, "Math");
            returnMe.Teacher = TeachersController.MRS_BROWN;
            return returnMe;
        }

        private static Course CREATE_SCIENCE()
        {
            var l_2 = new InPersonCourse(12345, "Science");
            l_2.Location = new Location(56, "Lawence Lab of Physics");
            l_2.Teacher = TeachersController.MR_SMITH;
            var linkResource = new LinkResource("xythos", "http://www.xythos.com");
            var wrapper = new ResourceWrapper(445, false, linkResource);
            l_2.Resources.Add(wrapper);
            var fileResource  = new FileResource("image.png", "http://graph.microsoft.com/drives/sdfsf/items/sdfdsf");
            var wrapper2 = new ResourceWrapper(555, true, fileResource);
            l_2.Resources.Add(wrapper2);
            return l_2;
        }

        private static Course CREATE_SOCIAL_STUDIES()
        {
            var returnMe = new ExternalCourse(7777, "Social Studies");
            var externalLocation = new ExternalLocation();
            externalLocation.Id = "http://externalLocationUri.com/Location(56)";
            returnMe.Location = externalLocation;
            returnMe.Teacher = TeachersController.MRS_JONES;
            return returnMe;
        }

        private static List<Course> initCourses()
        {
            var returnMe = new List<Course>();
            returnMe.Add(MATH);
            returnMe.Add(SCIENCE);
            returnMe.Add(SOCIAL_STUDIES);
            return returnMe;
        }
       
    }
}