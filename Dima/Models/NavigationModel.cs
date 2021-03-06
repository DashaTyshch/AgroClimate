﻿using Dima.Tools;
using Dima.Views;
using Dima.Windows;
using System;

namespace Dima.Models
{
    class NavigationModel
    {
        private ContentWindow _contentWindow;
        private readonly MainView _mainView;

        public NavigationModel(ContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
            _mainView = new MainView();
        }

        public void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.Login:
                    _contentWindow.ContentControl.Content = new LoginView();
                    break;
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = new MainView();
                    break;
                case ModesEnum.AddRequest:
                    _contentWindow.ContentControl.Content = new AddRequestView();
                    break;
                case ModesEnum.AddEngineer:
                    _contentWindow.ContentControl.Content = new AddEngineerView();
                    break;
                case ModesEnum.Request:
                    _contentWindow.ContentControl.Content = new RequestView();
                    break;
                case ModesEnum.Enginner:
                    _contentWindow.ContentControl.Content = new EngineerView();
                    break;
                case ModesEnum.AddBrigadier:
                    _contentWindow.ContentControl.Content = new AddBrigadier(); //povynno bulo view
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
