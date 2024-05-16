using DoorangMVC.Business.Exceptions;
using DoorangMVC.Business.Services.Abstract;
using DoorangMVC.Core.Models;
using DoorangMVC.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangMVC.Business.Services.Concretes
{
    public class ExploreService : IExploreService
    {
        private readonly IExploreRepository _exploreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ExploreService(IExploreRepository exploreRepository,IWebHostEnvironment webHostEnvironment)
        {
            _exploreRepository = exploreRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreateExplore(Explore explore)
        {
            if(explore.ImageFile == null) 
                throw new FileNullReferenceException("ImageFile","File null reference");
            if (!explore.ImageFile.ContentType.Contains("image/")) 
                throw new FileContentTypeException("ImageFile","File content type error");
            if (explore.ImageFile.Length > 2097152) 
                throw new FileSizeException("ImageFile","File Size Error");

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(explore.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\explores\" + filename;

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                explore.ImageFile.CopyTo(stream);
            }
            explore.ImageUrl = filename;
            _exploreRepository.Add(explore);
            _exploreRepository.Commit();
        }

        public void DeleteExplore(int id)
        {
           var existExplore = _exploreRepository.Get(x=>x.Id == id);
            if (existExplore == null) throw new EntityNotFoundException("", "Team not Found");
            string path = _webHostEnvironment.WebRootPath + @"\uploads\explores\" + existExplore.ImageUrl;

            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("ImageFile", "File not found!");

            File.Delete(path);

            _exploreRepository.Delete(existExplore);
            _exploreRepository.Commit();
        }

        public List<Explore> GetAllExplores(Func<Explore, bool>? func = null)
        {
            return _exploreRepository.GetAll(func);
        }

        public Explore GetExplore(Func<Explore, bool>? func = null)
        {
            return _exploreRepository.Get(func);
        }

        public void UpdateExplore(int id, Explore explore)
        {
            var olderExplore = _exploreRepository.Get(x => x.Id == id);
            if (olderExplore == null) throw new EntityNotFoundException("", "Team Not found!");

            if (explore.ImageFile != null)
            {
                if (!explore.ImageFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type error!");

                if (explore.ImageFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "File Size Error");

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(explore.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\explores\" + fileName;

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    explore.ImageFile.CopyTo(stream);
                }

                olderExplore.ImageUrl = fileName;
            }

            olderExplore.Title = explore.Title;
            olderExplore.Subtitle = explore.Subtitle;
            olderExplore.Description = explore.Description;

            _exploreRepository.Commit();
        }
    }
}
