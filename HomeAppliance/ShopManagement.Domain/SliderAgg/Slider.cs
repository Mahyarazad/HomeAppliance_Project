﻿using _0_Framework;

namespace SM.Domain.SliderAgg
{
    public class Slider : BaseEntity
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }
        public bool IsDeleted { get; private set; }
        protected Slider()
        {

        }
        public Slider(string picture, string pictureAlt, string pictureTitle, string heading
            , string title, string text, string btnText, string link)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
            IsDeleted = false;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string heading
            , string title, string text, string btnText, string link)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;

        }

        public void Delete()
        {
            this.IsDeleted = true;
        }
        public void ReActivate()
        {
            this.IsDeleted = false;
        }

    }
}