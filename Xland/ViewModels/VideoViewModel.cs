using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Models;

namespace Xland.ViewModels
{
    public class VideoViewModel
    {
        public Video Video { get; set; }
        public string VideoDescription { get; set; }
        public string VideoHtmlThumb { get; set; }
        public string VideoHtmlSlide { get; set; }

        public VideoViewModel(Video video, string description)
        {
            this.Video = video;
            this.VideoDescription = description;

            CreateVideoThumbHtml(video);
            CreateVideoSlideHtml(video);
        }

        private void CreateVideoThumbHtml(Video video)
        {
            switch (video.VideoType.ToString())
            {
                case "Upload":
                    this.VideoHtmlThumb = String.Format("<video controls data-video-id=\"thumb-{0}\" class=\"item\"><source src=\"{1}?width=300\" type=\"video/mp4\" /><p>Your browser does not support this format</p></video>", Video.ID, Video.Path);
                    break;
                case "Youtube":
                    this.VideoHtmlThumb = String.Format("<iframe width=\"100%\" height=\"200\" src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" class=\"item\" data-video-id=\"thumb-{1}\"></iframe>", Video.Embed, Video.ID);
                    break;
                case "Vimeo":
                    this.VideoHtmlThumb = String.Format("<iframe width=\"100%\" height=\"200\" src=\"https://player.vimeo.com/video/{0}\" frameborder=\"0\" class=\"item\" data-video-id=\"thumb-{1}\"></iframe>", Video.Embed, Video.ID);
                    break;
                default:
                    this.VideoHtmlThumb = "<p>Video format unrecognised</p>";
                    break;
            }
        }

        private void CreateVideoSlideHtml(Video video)
        {
            switch (video.VideoType.ToString())
            {
                case "Upload":
                    this.VideoHtmlSlide = String.Format("<video controls data-video-id=\"thumb-{0}\" class=\"item\"><source src=\"{1}\" type=\"video/mp4\" /><p>Your browser does not support this format</p></video>", Video.ID, Video.Path);
                    break;
                case "Youtube":
                    this.VideoHtmlSlide = String.Format("<iframe width=\"100%\" height=\"400\" src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" data-video-id=\"thumb-{1}\" class=\"item\"></iframe>", Video.Embed, Video.ID);
                    break;
                case "Vimeo":
                    this.VideoHtmlSlide = String.Format("<iframe width=\"100%\" height=\"400\" src=\"https://player.vimeo.com/video/{0}\" frameborder=\"0\" data-video-id=\"thumb-{1}\" class=\"item\"></iframe>", Video.Embed, Video.ID);
                    break;
                default:
                    this.VideoHtmlSlide = "<p>Video format unrecognised</p>";
                    break;
            }
        }

    }
}