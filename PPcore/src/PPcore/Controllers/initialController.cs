using System;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;
using Microsoft.EntityFrameworkCore;

namespace PPcore.Controllers
{
    public class initialController : Controller
    {
        private PalangPanyaDBContext _context;

        private void insertMasterData()
        {
            _context.Database.ExecuteSqlCommand("INSERT INTO ini_country (country_code,area_part,country_desc,id,x_status) VALUES (1,'T',N'ไทย',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO ini_country (country_code,area_part,country_desc,id,x_status) VALUES (2,'E',N'ลาว',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO ini_country (country_code,area_part,country_desc,id,x_status) VALUES (3,'U',N'เวียดนาม',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO project_course (course_code,project_code,course_desc,id,x_status) VALUES ('1','ASP5','ASP.Net 5',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO project_course (course_code,project_code,course_desc,id,x_status) VALUES ('2','MVC6','MVC 6',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO project_course (course_code,project_code,course_desc,id,x_status) VALUES ('3','EF7','Entity Framework 7',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO course_grade (cgrade_code,cgrade_desc,id,x_status) VALUES ('A','Good',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO course_grade (cgrade_code,cgrade_desc,id,x_status) VALUES ('B','Pass',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO course_grade (cgrade_code,cgrade_desc,id,x_status) VALUES ('F','Fail',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO mem_group (mem_group_code,mem_group_desc,id,x_status) VALUES ('1',N'สุขสวัสดิ์',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_group (mem_group_code,mem_group_desc,id,x_status) VALUES ('2',N'เมืองทอง',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('1','1',N'ผู้บริหาร',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('1','2',N'ผู้บริหารกลุ่ม',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('1','3',N'สมาชิก',newid(),'Y')");
            //_context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('2','3','Gold',newid(),'Y')");
            //_context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('2','2','Platinum',newid(),'Y')");
            //_context.Database.ExecuteSqlCommand("INSERT INTO mem_type (mem_group_code,mem_type_code,mem_type_desc,id,x_status) VALUES ('2','1','Wisdom',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO mem_level (mlevel_code,mlevel_desc,id,x_status) VALUES ('1',N'เพชร',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_level (mlevel_code,mlevel_desc,id,x_status) VALUES ('2',N'ทอง',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_level (mlevel_code,mlevel_desc,id,x_status) VALUES ('3',N'เงิน',newid(),'Y')");

            _context.Database.ExecuteSqlCommand("INSERT INTO mem_status (mstatus_code,mstatus_desc,id,x_status) VALUES ('1',N'สมาชิก',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_status (mstatus_code,mstatus_desc,id,x_status) VALUES ('2',N'หมดอายุ',newid(),'Y')");
            _context.Database.ExecuteSqlCommand("INSERT INTO mem_status (mstatus_code,mstatus_desc,id,x_status) VALUES ('3',N'กำลังดำเนินการต่ออายุ',newid(),'Y')");


        }

        private void insertSampleData()
        {
            string guid; string memberCode;
            guid = Guid.NewGuid().ToString();
            //memberCode = DateTime.Now.ToString("MMddHHmmssfff");
            memberCode = "1000011111333";
            _context.Database.ExecuteSqlCommand("INSERT INTO[dbo].[member] ([member_code], [fname], [lname], [sex], [nationality], [mem_photo], [cid_type], [cid_card], [cid_card_pic], [birthdate], [current_age], [religion], [place_name], [marry_status], [h_no], [lot_no], [village], [building], [floor], [room], [lane], [street], [subdistrict_code], [district_code], [province_code], [country_code], [zip_code], [mstatus_code], [mem_type_code], [mem_group_code], [mlevel_code], [zone], [latitude], [longitude], [texta_address], [textb_address], [textc_address], [tel], [mobile], [fax], [social_app_data], [email], [parent_code], [x_status], [x_note], [x_log], [id]) VALUES(N'"+ memberCode + "', N'ชูใจ', N'ใจดี', 'F', NULL, NULL, NULL, N'" + memberCode + "', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'G  ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, N'" + guid+"')");
            guid = Guid.NewGuid().ToString();
            memberCode = "2000011111333";
            _context.Database.ExecuteSqlCommand("INSERT INTO[dbo].[member] ([member_code], [fname], [lname], [sex], [nationality], [mem_photo], [cid_type], [cid_card], [cid_card_pic], [birthdate], [current_age], [religion], [place_name], [marry_status], [h_no], [lot_no], [village], [building], [floor], [room], [lane], [street], [subdistrict_code], [district_code], [province_code], [country_code], [zip_code], [mstatus_code], [mem_type_code], [mem_group_code], [mlevel_code], [zone], [latitude], [longitude], [texta_address], [textb_address], [textc_address], [tel], [mobile], [fax], [social_app_data], [email], [parent_code], [x_status], [x_note], [x_log], [id]) VALUES(N'" + memberCode + "', N'สมชาย', N'ใจเย็น', 'M', NULL, NULL, NULL, N'" + memberCode + "', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'G  ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, N'" + guid + "')");
            guid = Guid.NewGuid().ToString();
            memberCode = "3000011111333";
            _context.Database.ExecuteSqlCommand("INSERT INTO[dbo].[member] ([member_code], [fname], [lname], [sex], [nationality], [mem_photo], [cid_type], [cid_card], [cid_card_pic], [birthdate], [current_age], [religion], [place_name], [marry_status], [h_no], [lot_no], [village], [building], [floor], [room], [lane], [street], [subdistrict_code], [district_code], [province_code], [country_code], [zip_code], [mstatus_code], [mem_type_code], [mem_group_code], [mlevel_code], [zone], [latitude], [longitude], [texta_address], [textb_address], [textc_address], [tel], [mobile], [fax], [social_app_data], [email], [parent_code], [x_status], [x_note], [x_log], [id]) VALUES(N'" + memberCode + "', N'ปิติ', N'ใจสว่าง', 'M', NULL, NULL, NULL, N'" + memberCode + "', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'G  ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, N'" + guid + "')");
        }

        private void deleteData()
        {
            _context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'DELETE FROM ?'");
        }

        public initialController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: initial
        public IActionResult Index()
        {
            string data = "<div style='padding:50px;'>";
            data += "<a href='"+Url.Action("init")+"'>intial all master data</a> - WARNING THIS WILL DELETE ALL DATA<br/><br/>"; 
            data += "<a href='"+ Url.Action("addsample") + "'>add sample data</a><br/><br/>";
            data += "</div>";
            return Content(data, "text/html");
        }

        // GET: initial/init
        public IActionResult init()
        {
            deleteData();
            insertMasterData();

            return Ok("Succeed");
        }

        // GET: initial/addsample
        public IActionResult addsample()
        {
            insertSampleData();

            return Ok("Succeed");
        }

        // GET: initial/addsample
        public IActionResult initAndAddsample()
        {
            insertSampleData();

            return Ok("Succeed");
        }
    }
}
