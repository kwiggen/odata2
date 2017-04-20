using System.Collections.Generic;
using System.Web.OData;
using odata2.Models;
using System.Web.Http;
using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;

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

        [AcceptVerbs("POST", "PUT")]
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