using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using DMZ_Page.Models;
using DMZPage.Models;



namespace DMZ_Page.Controllers
{
    public class HomeController : Controller
    {

        private readonly DMZPage.Models.DMZDBContext _context;
        public static List<SrvError> errLst=new List<SrvError>(); 
        string resFile=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "settings.xml");

        public HomeController(DMZDBContext context)
        {
            _context=context;
        }


        public IActionResult BaseMonitor()
        {
           return View();
 
        }

public async Task<IActionResult> Workflow()
{
 
          await Task.Delay(1000);
           
         
            //var dbs= _context.RequestSelect.ToList();
            DMZPage.Models.BaseClass bases=new DMZPage.Models.BaseClass(){Workflows=_context.Workflow.ToList(),RequestSelects=_context.RequestSelect.Take(20).ToList()};

            return PartialView("_WorklowPanel",bases);

}


        public IActionResult Index()
        {
                
                Settings set = new Settings();
                try{
                    XmlSerializer xmlS = new XmlSerializer(typeof(Settings));
                        using(StreamReader sw = new StreamReader(resFile))
                        {
                            Settings st=(Settings)xmlS.Deserialize(sw);
                            ViewBag.path1= st.ReportPath1;
                            ViewBag.path2= st.ReportPath2;
                        }
                }catch{}

            var aaa=_context.PositionReference.ToList();
            ViewBag.summsg=errLst.Where(c=>c.TimeStamp.Date==DateTime.Now.Date).Count();
            return View(aaa);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Эта программа настройки службы ОМС.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Контактная информация";

            return View();
        }

        public IActionResult ResPath()
        {
               //ViewData["pth"]=resFile;
               Settings set = new Settings();
                try{
                XmlSerializer xmlS = new XmlSerializer(typeof(Settings));
               // using(StreamReader sw = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "settings.xml"))
                using(StreamReader sw = new StreamReader(resFile))
                
                {
                                set= (Settings)xmlS.Deserialize(sw);
                }
                }catch{}
                return View(set);
        }
        
        [HttpPost]
        public IActionResult ResPath(Settings set)
        {
             XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(resFile))
                {
                    serializer.Serialize(writer, set);
                }
            return Redirect("/");// View("<p>Файл настроек сохранен. Адрес файла:" + AppDomain.CurrentDomain.BaseDirectory + "settings.xml</p>");
        }

public IActionResult UploadWorkers(){
    
    return View();
}


[HttpPost("UploadFiles")]
public async  Task<IActionResult> UploadWorkers(IFormFile file)
{
    try{
        Stream stm =file.OpenReadStream();
        DataSet dataSet=new DataSet();
        dataSet.ReadXml(stm);
        
        List<PositionReference> PR=new List<PositionReference>();

      
       foreach(DataRow r in dataSet.Tables[0].Rows)
       {
              PR.Add(new PositionReference(){
                Code=int.Parse(r["Code"].ToString()),
                Parent=int.TryParse(r["Parent"].ToString(),out int i1)?i1:new Nullable<int>(),               
                Certification=bool.TryParse(r["Certification"].ToString(),out bool b1)?b1:new Nullable<bool>(),
                Position=(string)r["Position"].ToString(),
                Medical=bool.TryParse(r["Medical"].ToString(),out bool b2)?b2:new Nullable<bool>(),
                DateEnd=DateTime.TryParse(r["DateEnd"].ToString(),out DateTime dt)?dt:new Nullable<DateTime>(),
                MedCat= byte.TryParse(r["MedCat"].ToString(),out byte bt1)?bt1:new Nullable<byte>(),
                Enrolling=byte.TryParse(r["Enrolling"].ToString(),out byte bt2)?bt2:new Nullable<byte>()             
               });
       }

        _context.PositionReference.RemoveRange(_context.PositionReference);
        await _context.SaveChangesAsync();
        _context.PositionReference.AddRange(PR);
       await _context.SaveChangesAsync();


        //return Ok(new {retText="Всё отлично данные сохранены."});
    //return  Ok(dataSet.GetXml());
      //ViewData["count"]=_context.PositionReference.Count();
      
      //return View("UploadResult",_context.PositionReference.OrderBy(c=>c.Code).ToList());
      return Redirect("Home/UploadResult");

//return Ok(_context.PositionReference);

    }catch(Exception ex){
        return Ok(ex.Message);
        //var vr=new {"Что-то пошло не так."};
       
    }
}


public IActionResult UploadResult(){
  ViewData["count"]=_context.PositionReference.Count();
  return View(_context.PositionReference.OrderBy(c=>c.Code).ToList());
}


public IActionResult WindowsErrors()
{
    return View(errLst.OrderByDescending(c=>c.TimeStamp).ToList());
    //return View(_context.LogStore.ToList());
}



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }



}
