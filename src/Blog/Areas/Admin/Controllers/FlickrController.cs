using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Core.Repositories;
using Web.Areas.Admin.Models.Flickr;

namespace Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class FlickrController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFlickrRepository _flickrRepository;

		public FlickrController(
			IUnitOfWork unitOfWork,
			IFlickrRepository flickrRepository)
		{
			_unitOfWork = unitOfWork;
			_flickrRepository = flickrRepository;
		}

		public ActionResult Index()
		{
			var photos = _flickrRepository.GetAll().OrderByDescending(x => x.Id).ToArray();
			
			var model = new FlickrIndexModel();

			model.Photos = Mapper.Map<FlickrModel[]>(photos);

			return View(model);
		}

		public ActionResult GetPhotos()
		{
			var photos = _flickrRepository.GetAll().OrderByDescending(x => x.Id).ToArray();

			return Json(photos, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult UpdatePhoto(int id)
		{
			using (var unit = _unitOfWork.BeginTransaction())
			{
				var photo = _flickrRepository.GetById(id);

				photo.Update();

				_flickrRepository.Update(photo);

				unit.Commit();
			}

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}