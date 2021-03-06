﻿using ReactiveUI;
using System.Reactive;
using System;
using Splat;
using CodeHub.Core.Services;
using CodeHub.Core.Factories;

namespace CodeHub.Core
{
    public static class Bootstrap
    {
        public static void Init()
        {
            RxApp.DefaultExceptionHandler = Observer.Create((Exception e) => 
                Locator.Current.GetService<IAlertDialogFactory>().Alert("Error", e.Message));

            var defaultValueService = Locator.Current.GetService<IDefaultValueService>();
            var accountService = new GitHubAccountsService(defaultValueService);
            var applicationService = new ApplicationService(accountService);
            var loginService = new LoginService(accountService);

            Locator.CurrentMutable.RegisterLazySingleton(() => accountService, typeof(IAccountsService));
            Locator.CurrentMutable.RegisterLazySingleton(() => applicationService, typeof(IApplicationService));
            Locator.CurrentMutable.RegisterLazySingleton(() => loginService, typeof(ILoginService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ImgurService(), typeof(IImgurService));
        }
    }
}