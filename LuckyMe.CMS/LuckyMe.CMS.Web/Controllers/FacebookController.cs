using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Extensions;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Web.ClientHelpers;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class FacebookController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;

        public FacebookController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
        }

        #region Views

        public ActionResult FacebookPhotoViewer()
        {
            return View();
        }

        public ActionResult FacebookVideoViewer()
        {
            return View();
        }

        #endregion

        #region Photos

        public async Task<ActionResult> GetAllFacebookPhotosAsync(int page, int limit, string sort, string filter,
            int start = 0)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;


            //Get data
            var basequery = await _client.GetAllPhotosAsync();

            //Get data count
            var totalCount = basequery.Count;

            var sorting = new List<SortData>();

            if (sort == null)
            {
                sorting.Add(new SortData {Direction = "ASC", Property = "Name"});
            }
            else
            {
                var deserzdSort = JsonConvert.DeserializeObject<List<Sorting>>(sort);
                basequery = basequery.OrderBy(deserzdSort[0].Property + " " + deserzdSort[0].Direction).ToList();
            }

            if (filter != null)
            {
                var deserzdFilter = JsonConvert.DeserializeObject<List<Filtering>>(filter);

                for (var i = 0; i < deserzdFilter.Count(); i++)
                {
                    var filterparam = deserzdFilter[i];

                    switch (deserzdFilter[i].Field.ToUpper())
                    {
                        case "NAME":

                            basequery =
                                basequery.Where(
                                    x => x.Name != null && x.Name.ToLower().Contains(filterparam.Value.ToLower()))
                                    .ToList();
                            break;
                    }
                }
            }

            basequery = basequery.Skip(start)
                .Take(limit)
                .ToList();


            //Return data
            return Json(new {success = true, data = basequery, totalCount}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Videos

        public async Task<ActionResult> GetAllFacebookVideosAsync(int page, int limit, string sort, string filter,
            int start = 0)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;


            //Get data
            var basequery = await _client.GetAllVideosAsync();

            //Get data count
            var totalCount = basequery.Count;

            var sorting = new List<SortData>();

            if (sort == null)
            {
                sorting.Add(new SortData {Direction = "ASC", Property = "Name"});
            }
            else
            {
                var deserzdSort = JsonConvert.DeserializeObject<List<Sorting>>(sort);
                basequery = basequery.OrderBy(deserzdSort[0].Property + " " + deserzdSort[0].Direction).ToList();
            }

            if (filter != null)
            {
                var deserzdFilter = JsonConvert.DeserializeObject<List<Filtering>>(filter);

                for (var i = 0; i < deserzdFilter.Count(); i++)
                {
                    var filterparam = deserzdFilter[i];

                    switch (deserzdFilter[i].Field.ToUpper())
                    {
                        case "NAME":

                            basequery =
                                basequery.Where(
                                    x => x.Name != null && x.Name.ToLower().Contains(filterparam.Value.ToLower()))
                                    .ToList();

                            break;
                    }
                }
            }

            basequery = basequery.Skip(start)
                .Take(limit)
                .ToList();


            //Return data
            return Json(new {success = true, data = basequery, totalCount}, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}