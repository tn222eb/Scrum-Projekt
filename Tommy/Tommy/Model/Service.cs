using System;
using System.Collections.Generic;
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

        public void DeleteImageData(string imagename)
        {
            ImageDAL.DeleteImageData(imagename);
        }

        public void InsertImageData(string imagename, string userid, int imagecategoryid, string videotitle)
        {
            ImageDAL.InsertImageData(imagename, userid, imagecategoryid, videotitle);
        }

        public List<Image> GetUserImages(string userid)
        {
            return ImageDAL.GetUserImages(userid);
        }

        public List<Image> GetCategoryImages(int imagecategoryid)
        {
            return ImageDAL.GetCategoryImages(imagecategoryid);
        }

        public void InsertUserData(string access_token, string userid, string name)
        {
            FacebookDAL.InsertUserData(access_token, userid, name);
        }

        public string GetUserData(string userid)
        {
            return FacebookDAL.GetUserData(userid);
        }

        public IEnumerable<ImageCategory> GetImageCategory()
        {
            return ImageCategoryDAL.GetImageCategory();
        }

        public IEnumerable<VideoCategory> GetVideoCategory()
        {
            return VideoCategoryDAL.GetVideoCategory();
        }

        public void InsertVideoData(string videoname, string userid, int videocategoryid, string videotitle)
        {
            VideoDAL.InsertVideoData(videoname, userid, videocategoryid, videotitle);
        }

        public void DeleteVideoData(string videoname)
        {
            VideoDAL.DeleteVideoData(videoname);
        }

        public List<Video> GetUserVideos(string userid)
        {
            return VideoDAL.GetUserVideos(userid);
        }

        public List<Video> GetCategoryVideos(int videocategoryid)
        {
            return VideoDAL.GetCategoryVideos(videocategoryid);
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
        public VideoCategory GetCategoryByVideoID(int videoid)
        {
            return VideoCategoryDAL.GetCategoryByVideoID(videoid);
        }

        public void UpdateVideo(Video video, int videocategoryid)
        {
            VideoDAL.UpdateVideo(video, videocategoryid);
        }
    }
}