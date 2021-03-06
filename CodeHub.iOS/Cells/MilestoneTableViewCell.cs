using System;
using ReactiveUI;
using CodeHub.iOS.ViewComponents;
using UIKit;
using CoreGraphics;
using System.Reactive.Linq;
using CodeHub.Core.ViewModels.Issues;
using Foundation;

namespace CodeHub.iOS.Cells
{
    public class MilestoneTableViewCell : ReactiveTableViewCell<IssueMilestoneItemViewModel>
    {
        public static NSString Key = new NSString("milestonecellview");
        private readonly MilestoneView _milestoneView;

        public override void SetSelected(bool selected, bool animated)
        {
            BackgroundColor = selected ? UIColor.FromWhiteAlpha(0.9f, 1.0f) : UIColor.White;
        }

        public override void SetHighlighted(bool highlighted, bool animated)
        {
            BackgroundColor = highlighted ? UIColor.FromWhiteAlpha(0.9f, 1.0f) : UIColor.White;
        }

        public MilestoneTableViewCell(IntPtr handle)
            : base(handle)
        {
            var frame = Frame = new CGRect(0, 0, 320f, 80);
            AutosizesSubviews = true;
            ContentView.AutosizesSubviews = true;

            _milestoneView = new MilestoneView();
            _milestoneView.Frame = frame;
            _milestoneView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            ContentView.Add(_milestoneView);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x => _milestoneView.Init(x.Title, x.OpenIssues, x.ClosedIssues, x.DueDate));
        }
    }
}

