using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CaseWebApp.Models;
using CaseWebApp.Models.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseWebApp.Controllers
{
    [Route("api/RestRequest")]
    public class RestController : Controller
    {
        private readonly DataContext db;
        public RestController(DataContext dbContext)
        {
            db = dbContext;
        }

        // Kullanıcıya ait url kayıtlarını listeleme. 
        [Produces("application/json")]
        [HttpGet("getAllUrlList")]
        public async Task<IActionResult> GetAllUrlList()
        {
            try
            {
                string userId = HttpContext.Session.GetString(Globals.Session_UserId);
                var urlListData = db.URLList.Where(q=> q.IsActive == true  && q.CreateAddById == Convert.ToInt32(userId)).ToList();

                if (urlListData != null && urlListData.Count > 0)
                {
                    return Ok(urlListData);
                }
                else
                {
                    return BadRequest(new { message = "An error occurred, try again!" });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new { message = " "+ex.Message });
            }
        }
        // Seçili url silme işlemi. 
        [Produces("application/json")]
        [HttpGet("deleteURL")]
        public async Task<IActionResult> DeleteURL()
        {
                string id= HttpContext.Request.Query["id"].ToString();
            try
            {
                URLList url = db.URLList.Where(x => x.Id == Convert.ToInt32(id)).First();
                url.IsActive = false;
                db.URLList.Update(url);
                db.SaveChanges();
                return Ok(new { message = "Is deleted." });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = " " + ex.Message });

            }

        }
        // Seçili url için update işlemi. 
        [Produces("application/json")]
        [HttpPost("updateURL")]
        public IActionResult updateURL(URLList view)
        {
            try
            {
                URLList url = db.URLList.Where(x => x.Id == Convert.ToInt32(view.Id)).First();
                url.IsActive = true;
                url.WebName = view.WebName;
                url.URL = view.URL;
                url.Time = view.Time;
                db.URLList.Update(url);
                db.SaveChanges();
                return Ok(new { message = "Is Update." });

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = " " + ex.Message });

            }

        }

        // Url listesine kayıt işlemi
        [Produces("application/json")]
        [HttpPost("addURL")]
        public IActionResult addURL(URLList view)
        {
            try
            {
                string userId = HttpContext.Session.GetString(Globals.Session_UserId);
                URLList url = new URLList();
                url.IsActive = true;
                url.WebName = view.WebName;
                url.URL = view.URL;
                url.Time = view.Time;
                url.CreateAddById = Convert.ToInt32(userId);
                url.CreateDate = DateTime.Now;
                db.URLList.Add(url);
                db.SaveChanges();
                return Ok(new { message = "Add Url." });

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = " " + ex.Message });

            }

        }
        //Login olan kişiye ayit tüm hata loglarını listeliyoruz
        [Produces("application/json")]
        [HttpGet("getAllUrlListLog")]
        public async Task<IActionResult> GetAllUrlListLog()
        {
            try
            {
                string userId = HttpContext.Session.GetString(Globals.Session_UserId);
                var urlListLogData = from p in db.URLList
                             join c in db.LogList on p.Id equals c.UrlListId
                             where p.CreateAddById == Convert.ToInt32(userId)
                             select new
                             {
                                 WebName = p.WebName,
                                 WebUrl = p.URL,
                                 ResponseCode = c.ResponseCode,
                             };


                if (urlListLogData != null)
                {
                    return Ok(urlListLogData);
                }
                else
                {
                    return BadRequest(new { message = "An error occurred, try again!" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = " " + ex.Message });
            }
        }


        //**********************************************Cron job içerisine alındı*****************************************

        //[Produces("application/json")]
        //[HttpGet("urlChecks")]
        //public async Task<IActionResult> urlChecks()
        //{
        //    try
        //    {
        //        string userId = HttpContext.Session.GetString(Globals.Session_UserId);
        //        var userData = db.Users.Where(q => q.Id == Convert.ToInt32(userId)).FirstOrDefault();
        //        var urlListData = db.URLList.Where(q => q.IsActive == true && q.CreateAddById == Convert.ToInt32(userId)).ToList();
        //        var client = new HttpClient();

        //        foreach (var url in urlListData)
        //        {
        //            var UrlCheck = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url.URL));
        //            int statusCode = (int)UrlCheck.StatusCode;

        //            if(statusCode != 200)
        //            {

        //                Tools tls = new Tools();
        //                string body = "<style>*{margin:0;padding:0;font-family:'HelveticaNeue','Helvetica',Helvetica,Arial,sans-serif;box-sizing:border-box;font-size:14px;}img{max-width:100%;}body{-webkit-font-smoothing:antialiased;-webkit-text-size-adjust:none;width:100%!important;height:100%;line-height:1.6;}/*Let'smakesurealltableshavedefaults*/tabletd{vertical-align:top;}body{background-color:#f6f6f6;}.body-wrap{background-color:#f6f6f6;width:100%;}.container{display:block!important;max-width:600px!important;margin:0auto!important;/*makesitcentered*/clear:both!important;}.content{max-width:600px;margin:0auto;display:block;padding:20px;}.main{background:#fff;border:1pxsolid#e9e9e9;border-radius:3px;}.content-wrap{padding:20px;}.content-block{padding:0020px;}.header{width:100%;margin-bottom:20px;}.footer{width:100%;clear:both;color:#999;padding:20px;}.footera{color:#999;}.footerp,.footera,.footerunsubscribe,.footertd{font-size:12px;}h1,h2,h3{font-family:'HelveticaNeue',Helvetica,Arial,'LucidaGrande',sans-serif;color:#000;margin:40px00;line-height:1.2;font-weight:400;}h1{font-size:32px;font-weight:500;}h2{font-size:24px;}h3{font-size:18px;}h4{font-size:14px;font-weight:600;}p,ul,ol{margin-bottom:10px;font-weight:normal;}pli,ulli,olli{margin-left:5px;list-style-position:inside;}a{color:#1ab394;text-decoration:underline;}.btn-primary{text-decoration:none;color:#FFF;background-color:#1ab394;border:solid#1ab394;border-width:5px10px;line-height:2;font-weight:bold;text-align:center;cursor:pointer;display:inline-block;border-radius:5px;text-transform:capitalize;}.last{margin-bottom:0;}.first{margin-top:0;}.aligncenter{text-align:center;}.alignright{text-align:right;}.alignleft{text-align:left;}.clear{clear:both;}.alert{font-size:16px;color:#fff;font-weight:500;padding:20px;text-align:center;border-radius:3px3px00;}.alerta{color:#fff;text-decoration:none;font-weight:500;font-size:16px;}.alert.alert-warning{background:#f8ac59;}.alert.alert-bad{background:#ed5565;}.alert.alert-good{background:#1ab394;}.invoice{margin:40pxauto;text-align:left;width:80%;}.invoicetd{padding:5px0;}.invoice.invoice-items{width:100%;}.invoice.invoice-itemstd{border-top:#eee1pxsolid;}.invoice.invoice-items.totaltd{border-top:2pxsolid#333;border-bottom:2pxsolid#333;font-weight:700;}@mediaonlyscreenand(max-width:640px){h1,h2,h3,h4{font-weight:600!important;margin:20px05px!important;}h1{font-size:22px!important;}h2{font-size:18px!important;}h3{font-size:16px!important;}.container{width:100%!important;}.content,.content-wrap{padding:10px!important;}.invoice{width:100%!important;}}</style>" +
        //                    "<table class='body-wrap'>" +
        //                                "<tr>" +
        //                                    "<td></td>" +
        //                                    "<td class='container' width='600'>" +
        //                                        "<div class='content'>" +
        //                                            "<table class='main' width='100%' cellpadding='0' cellspacing='0'>" +
        //                                                "<tr>" +
        //                                                    "<td class='content-wrap'>" +
        //                                                        "<table cellpadding = '0' cellspacing='0'>" +
        //                                                            "<tr>" +
        //                                                                "<td class='content-block'>" +
        //                                                                    "<h3>URL Check Notification</h3>" +
        //                                                                "</td>" +
        //                                                            "</tr>" +
        //                                                            "<tr>" +
        //                                                                "<td class='content-block'>" +
        //                                                                   "Dear " + userData.UserName + ",<br><br>" +
        //                                                                  "<br><br>" +
        //                                                                   "<strong>Web Name:</strong> " + url.WebName + " <br>" +
        //                                                                   "<strong>Url:</strong> " + url.URL + " <br>" +
        //                                                                    "<strong>Code:</strong> " + statusCode + " <br>" +
        //                                                                "</td>" +
        //                                                            "</tr>" +
        //                                                          "</table>" +
        //                                                    "</td>" +
        //                                                "</tr>" +
        //                                            "</table>" +
        //                                            "<div class='footer'>" +
        //                                                "<table width = '100%' >" +
        //                                                    "<tr>" +
        //                                                        "<td class='aligncenter content-block'>© Copyright.All Rights Reserved</td>" +
        //                                                    "</tr>" +
        //                                                "</table>" +
        //                                            "</div></div>" +
        //                                    "</td>" +
        //                                    "<td></td>" +
        //                                "</tr>" +
        //                            "</table>";

        //                string mailResult = tls.SendMail(userData.UserEmail, body, "URL Check Notification");

        //                if (!string.IsNullOrEmpty(mailResult))
        //                {
        //                    LogList log = new LogList();
        //                    log.UrlListId = url.Id;
        //                    log.CreateDate = DateTime.Now;
        //                    log.ResponseCode = statusCode;
        //                    db.LogList.Add(log);
        //                    db.SaveChanges();
        //                }



                     
        //            }


        //        }
        //        return Ok(new { message = "Add Log." });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = " " + ex.Message });
        //    }
        //}


  

    }
}
