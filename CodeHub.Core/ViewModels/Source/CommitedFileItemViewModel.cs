﻿using System;
using ReactiveUI;
using GitHubSharp.Models;

namespace CodeHub.Core.ViewModels.Source
{
    public class CommitedFileItemViewModel : ReactiveObject, ICanGoToViewModel
    {
        public string Name { get; private set; }

        public string RootPath { get; private set; }

        public IReactiveCommand<object> GoToCommand { get; private set; }

        internal CommitedFileItemViewModel(CommitModel.CommitFileModel file, Action<CommitedFileItemViewModel> gotoAction)
        {
            Name = System.IO.Path.GetFileName(file.Filename);
            RootPath = file.Filename.Substring(0, file.Filename.Length - Name.Length);
//            var name = y.Filename.Substring(y.Filename.LastIndexOf("/", StringComparison.Ordinal) + 1);
            GoToCommand = ReactiveCommand.Create().WithSubscription(_ => gotoAction(this));
        }
    }
}

