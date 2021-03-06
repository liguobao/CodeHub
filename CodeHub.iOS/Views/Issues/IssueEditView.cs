using System;
using System.Linq;
using UIKit;
using CodeHub.Core.ViewModels.Issues;
using ReactiveUI;
using CodeHub.iOS.TableViewSources;
using CodeHub.iOS.DialogElements;

namespace CodeHub.iOS.Views.Issues
{
    public class IssueEditView : BaseTableViewController<IssueEditViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new DialogTableViewSource(TableView);
            TableView.Source = source;

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(Images.SaveButton, UIBarButtonItemStyle.Plain, (s, e) => {
                View.EndEditing(true);
                ViewModel.SaveCommand.ExecuteIfCan();
            });
            NavigationItem.RightBarButtonItem.EnableIfExecutable(ViewModel.SaveCommand.CanExecuteObservable);

            var title = new InputElement("Title", string.Empty, string.Empty);
            title.Changed += (sender, e) => ViewModel.Subject = title.Value;

            var content = new MultilinedElement("Description");
            content.Tapped += () => ViewModel.GoToDescriptionCommand.ExecuteIfCan();

            ViewModel.WhenAnyValue(x => x.Issue.Title).Subscribe(x => title.Value = x);
            ViewModel.WhenAnyValue(x => x.Content).Subscribe(x => content.Value = x);

            source.Root.Reset(new Section { title }, new Section { content });
        }
    }
}

