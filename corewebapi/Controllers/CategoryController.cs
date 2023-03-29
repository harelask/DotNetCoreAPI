using corewebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace corewebapi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ILogger<CategoryController> _logger;
        public CategoryController(DatabaseContext databaseContext, ILogger<CategoryController> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        [HttpGet("GetCategory")]
        public IEnumerable<Category> Get()
        {
            IEnumerable<Category> categorylist = new List<Category>();

            categorylist = (from category in _databaseContext.Category
                            select category).ToList();

            return categorylist;
        }

        [HttpGet("GetCategoryGetAlfromJoin")]
        public IEnumerable<object> GetCategoryGetAlfromJoin()
        {
            IEnumerable<object> product = from c in _databaseContext.Category
                                          join P in _databaseContext.Product_Master on c.CategoryId equals P.Category_id
                                          select (c);
            return product;

        }

        [HttpGet("GetCategoryFromStoredProcedure/{id}")]
        public IEnumerable<Category> GetCategoryFromStoredProcedure(int id)
        {

            var categorylist = _databaseContext.Category.FromSqlRaw("EXEC GetCategoryList @CategoryId",
                                                           new SqlParameter("@CategoryId", id)
                                                           ).ToList();
            return categorylist;
        }
        [HttpPut("CreateCategory")]
        public void CreateAdd(Category category)
        {
            _databaseContext.Category.Add(category);
            _logger.Log(LogLevel.Trace, (category.CategoryName + " is added in the database"));
            _databaseContext.SaveChanges();
        }
        [HttpPost("UpdateCategory")]
        public void Update(Category category)
        {
            _databaseContext.Category.Update(category);
            _databaseContext.SaveChanges();
        }
        [HttpDelete(Name = "DeleteCategory")]
        public void Delete(Category category)
        {
            _databaseContext.Category.Remove(category);
            _databaseContext.SaveChanges();
        }
        [HttpGet("GetMISReport")]
        public IEnumerable<Category> GetMISReport(string id)
        {

            var categorylist = _databaseContext.Category.FromSqlRaw("EXEC GetCategoryList @CategoryId",
                                                           new SqlParameter("@CategoryId", id)
                                                           ).ToList();
            return categorylist;
        }

        [HttpGet("GetMISReport2")]
        public IEnumerable<Category> GetMISReport2(string id)
        {

            var categorylist = _databaseContext.Category.FromSqlRaw("EXEC GetCategoryList @CategoryId",
                                                           new SqlParameter("@CategoryId", id)
                                                           ).ToList();
            return categorylist;
        }

        [HttpGet("GetMISReport3")]
        public IEnumerable<Category> GetMISReport3(string id)
        {

            var categorylist = _databaseContext.Category.FromSqlRaw("EXEC GetCategoryList @CategoryId",
                                                           new SqlParameter("@CategoryId", id)
                                                           ).ToList();
            return categorylist;
        }

    }
}


// PERSON TABLE API

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    private readonly ILogger<PersonController> _logger;
    public PersonController(DatabaseContext databaseContext, ILogger<PersonController> logger)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    [HttpGet(Name = "GetPersonData")]

    public IEnumerable<Person> GetPersonData()
    {
        IEnumerable<Person> personlist = new List<Person>();

        personlist = (from person in _databaseContext.Person
                      select person).ToList();

        return personlist;
    }


    [HttpPost(Name = "CreatePersonData")]
    public void CreatePersonData(Person person)
    {
        _databaseContext.Person.Add(person);
        _logger.Log(LogLevel.Trace, (person.firstname + person.lastname + person.email + person.phone));
        _databaseContext.SaveChanges();
    }

    [HttpPut(Name = "UpdatePersonData")]
    public void UpdatePersonData(Person person)
    {
        _databaseContext.Person.Update(person);
        _databaseContext.SaveChanges();
    }


    // Working on API and NOT in FrontEnd

    [HttpDelete("DeletePersonData")]
    public void DeletePersonData(Person person)
    {
        _databaseContext.Person.Remove(person);
        _databaseContext.SaveChanges();
    }

    [HttpDelete("DeleteByid/{perId}")]
    public IEnumerable<Person> DeleteByid(int perId)
    {
        var deletelist = _databaseContext.Person.FromSqlRaw($"EXEC delPerson @perId",
                                                    new SqlParameter("@perId", perId));

        return deletelist;
    }


    // Working on Both API and FrontEnd

    [HttpDelete("DeleteProductAsync/{perId}")]
    public async Task<int> DeleteProductAsync(int perId)
    {
        return await Task.Run(() => _databaseContext.Database.ExecuteSqlInterpolatedAsync($"delPerson {perId}"));
    }


    [HttpDelete("DeletePerson/{perId}")]
    public void Delete(int perId)
    {
        var res1 = _databaseContext.Person.Where(a => a.perId == perId).FirstOrDefault();
        _databaseContext.Person.Remove(res1);
        _databaseContext.SaveChanges();
    }

}





    // Student



    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ILogger<StudentController> _logger;
        public StudentController(DatabaseContext databaseContext, ILogger<StudentController> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }


    // Student Detail Table

        [HttpGet("GetStudentData")]
        public IEnumerable<object> GetStudentData()
        {
             

        IEnumerable<object> studentlist = from s in _databaseContext.studentDetail
                                            select new
                                            {
                                                s.stuId,
                                                s.regNo,
                                                s.studentName,
                                                s.DOB,
                                                s.gender,
                                                s.istrue,
                                                date = s.DOB.ToString("yyyy-MM-dd")
                                            };

            return studentlist;
        }

        [HttpPost(Name = "CreateStudent")]
        public void CreateStudent(studentDetail student)
        {
            _databaseContext.studentDetail.Add(student);
            _logger.Log(LogLevel.Trace, (student.regNo + student.studentName + student.DOB + student.gender + student.istrue));
            _databaseContext.SaveChanges();
        }

        [HttpPut(Name = "UpdateStudent")]
        public void UpdatePersonData(studentDetail student)
        {
            _databaseContext.studentDetail.Update(student);
            _databaseContext.SaveChanges();
        }

        [HttpDelete("DeleteStudent/{stuId}")]
        public ActionResult<IEnumerable<int>> DeleteStudent(int stuId)
        {
            try
            {
                var res1 = _databaseContext.studentDetail.Where(a => a.stuId == stuId).FirstOrDefault();
                _databaseContext.studentDetail.Remove(res1);
                _databaseContext.SaveChanges();
            return Ok(res1);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");    
                return StatusCode(202, ex.Message);
            }
            
        }



    // Student Mark Table

    [HttpGet("GetMarkFromStudent")]
    public IEnumerable<object> GetMarkFromStudent()
    {
        IEnumerable<object> stuMark = from sm in _databaseContext.studentMark
                                      join sd in _databaseContext.studentDetail on sm.studentId equals sd.stuId
                                      select new
                                      {
                                        sm.markId,
                                        sm.studentId,
                                        sd.studentName,
                                        sm.tamil,
                                        sm.english,
                                        sm.maths,
                                        sm.science,
                                        sm.social,
                                        sd.istrue,
                                        Total=sm.tamil+sm.english+sm.maths+sm.science+sm.social
                                      };
        return stuMark;
    }

    [HttpGet("GetSingleData/{regno}/{dob}")]
    public IEnumerable<object> GetSingleData(string regno, DateTime dob)
    {
        IEnumerable<object> stuMark1 = (from sm in _databaseContext.studentMark
                                       join sd in _databaseContext.studentDetail on sm.studentId equals sd.stuId
                                       where (sd.regNo == regno && sd.DOB == dob)
                                       select new
                                       {
                                           sm.markId,
                                           sm.studentId,
                                           sd.studentName,
                                           sm.tamil,
                                           sm.english,
                                           sm.maths,
                                           sm.science,
                                           sm.social,
                                           sd.istrue,
                                           Total = sm.tamil + sm.english + sm.maths + sm.science + sm.social
                                       });
        return stuMark1;
    }

    [HttpPost("InsertStudentMark")]
    public void InsertStudentMark(studentMark studentmark)
    {
        _databaseContext.studentMark.Add(studentmark);
        _logger.Log(LogLevel.Trace, (studentmark.studentId + studentmark.tamil + studentmark.english + studentmark.maths + studentmark.science + studentmark.social +"added"));
        _databaseContext.SaveChanges();
    }

    [HttpPut("UpdateStudentMark")]
    public void UpdateStudentMark(studentMark studentmark)
    {
        _databaseContext.studentMark.Update(studentmark);
        _databaseContext.SaveChanges();
    }

    [HttpDelete("DeleteMark/{markId}")]
    public void DeleteMark(int markId)
    {
        var res2 = _databaseContext.studentMark.Where(a => a.markId == markId).FirstOrDefault();
        _databaseContext.studentMark.Remove(res2);
        _databaseContext.SaveChanges();
    }


    // Get All Student Rank

    [HttpGet(Name = "GetAllStudentByRank")]
    public IEnumerable<object> GetAllStudentByRank()
    {
        var rankresult = (from sm in _databaseContext.studentMark
                          join sd in _databaseContext.studentDetail on sm.studentId equals sd.stuId
                          orderby sm.tamil + sm.english + sm.maths + sm.science + sm.social descending
                          select new
                          {
                              sm.markId,
                              sm.studentId,
                              sd.studentName,
                              sm.tamil,
                              sm.english,
                              sm.maths,
                              sm.science,
                              sm.social,
                              sd.istrue,
                              Total = sm.tamil + sm.english + sm.maths + sm.science + sm.social
                          });

        return rankresult;
    }

    [HttpPut("studentistrueUpdateN/{stuid}")]
    public void studentistrueUpdateN(int stuid)
    {
        var studentToUpdate = _databaseContext.studentDetail.FirstOrDefault(s => s.stuId == stuid);
        if (studentToUpdate != null)
        {
            studentToUpdate.istrue = "N";
        }
        _databaseContext.studentDetail.Update(studentToUpdate);
        _databaseContext.SaveChanges();
    }

    [HttpPut("studentistrueUpdateY/{stuid}")]
    public void studentistrueUpdateY(int stuid)
    {
        var studentToUpdate = _databaseContext.studentDetail.FirstOrDefault(s => s.stuId == stuid);
        if (studentToUpdate != null)
        {
            studentToUpdate.istrue = "Y";
        }
        _databaseContext.studentDetail.Update(studentToUpdate);
        _databaseContext.SaveChanges();
    }

}



