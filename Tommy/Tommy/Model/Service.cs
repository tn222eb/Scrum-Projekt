using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tommy.Model.DAL;

namespace Tommy.Model
{
    public class Service
    {
        private FacebookUserDAL _facebookDAL;
        private ImageCategoryDAL _imageCategoryDAL;
        private ImageDAL _imageDAL;
        private VideoCategoryDAL _videoCategoryDAL;
        private VideoDAL _videoDAL;

        private FacebookUserDAL FacebookDAL
        {
            get { return _facebookDAL ?? (_facebookDAL = new FacebookUserDAL()); }
        }

        private ImageCategoryDAL ImageCategoryDAL
        {
            get { return _imageCategoryDAL ?? (_imageCategoryDAL = new ImageCategoryDAL()); }
        }

        private ImageDAL ImageDAL
        {
            get { return _imageDAL ?? (_imageDAL = new ImageDAL()); }
        }

        private VideoCategoryDAL VideoCategoryDAL
        {
            get { return _videoCategoryDAL ?? (_videoCategoryDAL = new VideoCategoryDAL()); }
        }

        private VideoDAL VideoDAL
        {
            get { return _videoDAL ?? (_videoDAL = new VideoDAL()); }
        }

        public void DeleteImageData(int imageid)
        {
            ImageDAL.DeleteImageData(imageid);
        }

        public void InsertImageData(string imagename, string userid, int imagecategoryid, string videotitle)
        {
            ImageDAL.InsertImageData(imagename, userid, imagecategoryid, videotitle);
        }

        public void InsertUserData(string userid, string name)
        {
            FacebookDAL.InsertUserData(userid, name);
        }

        public string GetUserData(string userid)
        {
            return FacebookDAL.GetUserData(userid);
        }

        public IEnumerable<ImageCategory> GetImageCategory(bool refresh = false)
        {
            var imagecategorys = HttpContext.Current.Cache["ImageCategory"] as IEnumerable<ImageCategory>;
            if (imagecategorys == null || refresh)
            {
                imagecategorys = ImageCategoryDAL.GetImageCategory();
                HttpContext.Current.Cache.Insert("ImageCategory", imagecategorys, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            return imagecategorys;
        }

        public IEnumerable<VideoCategory> GetVideoCategory(bool refresh = false)
        {
            var videocategorys = HttpContext.Current.Cache["VideoCategory"] as IEnumerable<VideoCategory>;
            if (videocategorys == null || refresh)
            {
                videocategorys = VideoCategoryDAL.GetVideoCategory();
                HttpContext.Current.Cache.Insert("VideoCategory", videocategorys, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            return videocategorys;
        }

        public void InsertVideoData(string videoname, string userid, int videocategoryid, string videotitle)
        {
            VideoDAL.InsertVideoData(videoname, userid, videocategoryid, videotitle);
        }

        public void DeleteVideoData(int videoid)
        {
            VideoDAL.DeleteVideoData(videoid);
        }

        public static IEnumerable<Image> GetImagesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ImageDAL.GetImagesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public static IEnumerable<Image> GetImagesPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int imagecategoryid)
        {
            return ImageDAL.GetImagesPageWiseByID(maximumRows, startRowIndex, out totalRowCount, imagecategoryid);
        }

        public static IEnumerable<Image> GetMyImagesPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string userid)
        {
            return ImageDAL.GetMyImagesPageWiseByID(maximumRows, startRowIndex, out totalRowCount, userid);
        }

        public static IEnumerable<Video> GetVideosPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return VideoDAL.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public static IEnumerable<Video> GetVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int videocategoryid)
        {
            return VideoDAL.GetVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, videocategoryid);
        }

        public static IEnumerable<Video> GetMyVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string userid)
        {
            return VideoDAL.GetMyVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, userid);
        }

        public Video GetVideoDataByID(int videoid)
        {
            return VideoDAL.GetVideoDataByID(videoid);
        }

        public void UpdateVideo(Video video, int videocategoryid)
        {
            ICollection<ValidationResult> validationResults;
            if (!video.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            VideoDAL.UpdateVideo(video, videocategoryid);
        }

        public void UpdateImage(Image image, int imagecategoryid)
        {
            ICollection<ValidationResult> validationResults;
            if (!image.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            ImageDAL.UpdateImage(image, imagecategoryid);
        }

        public Image GetImageDataByID(int imageid)
        {
            return ImageDAL.GetImageDataByID(imageid);
        }

        public List<Image> GetLatestImages()
        {
            return ImageDAL.GetLatestImages();
        }
    }
}