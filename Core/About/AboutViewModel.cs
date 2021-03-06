﻿using GalaSoft.MvvmLight;

namespace Core.About
{
    public class AboutViewModel :ViewModelBase
    {
        private string _textAbout = "Тут будет информация о приложении";
        public string TextAbout
        {
            get => _textAbout;
            set => Set(ref _textAbout, value);
        }
    }
}