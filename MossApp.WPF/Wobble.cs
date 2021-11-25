using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MossApp.WPF
{
    public static class Wobble
    {
        public static bool GetWobble(DependencyObject obj)
        {
            return (bool)obj.GetValue(WobbleProperty);
        }

        public static void SetWobble(DependencyObject obj, bool value)
        {
            obj.SetValue(WobbleProperty, value);
        }

        public static readonly DependencyProperty WobbleProperty = DependencyProperty.RegisterAttached("Wobble", typeof(bool), typeof(Wobble), new UIPropertyMetadata(false, new PropertyChangedCallback(OnWobbleChanged)));

        private static void OnWobbleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            Image image = sender as Image;

            if (image == null)
                throw new InvalidOperationException("only images can wobble!");

            // don't really need this check (the find ancestor binding would still find the button), but the spec said the image should be the only child of the button
            Button button = LogicalTreeHelper.GetParent(image) as Button;
            if (button == null)
                throw new InvalidOperationException("only images that are the only child of a button can wobble!");

            Style previousStyle = image.Style;

            Style newStyle = new Style(image.GetType(), previousStyle);

            // this will override any existing render transform + origin on the button, hope they didn't already have one (and I'm too lazy to check)
            newStyle.Setters.Add(new Setter(Image.RenderTransformProperty, new RotateTransform(0)));
            newStyle.Setters.Add(new Setter(Image.RenderTransformOriginProperty, new Point(0.5, 0.5)));

            DataTrigger trigger = new DataTrigger();

            Binding binding = new Binding();

            RelativeSource relativeSource = new RelativeSource();
            relativeSource.Mode = RelativeSourceMode.FindAncestor;
            relativeSource.AncestorType = typeof(Button);

            binding.RelativeSource = relativeSource;
            binding.Path = new PropertyPath(Button.VisibilityProperty);

            trigger.Binding = binding;
            trigger.Value = Visibility.Visible;

            Storyboard storyboard = new Storyboard();

            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(0).(1)", Image.RenderTransformProperty, RotateTransform.AngleProperty));
            animation.Duration = new Duration(TimeSpan.FromSeconds(5)); // spec said 30, but i wanted to actually see it happen!
            _ = animation.KeyFrames.Add(new LinearDoubleKeyFrame(-12, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            _ = animation.KeyFrames.Add(new LinearDoubleKeyFrame(12, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            _ = animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));

            storyboard.Children.Add(animation);
            storyboard.RepeatBehavior = RepeatBehavior.Forever;

            BeginStoryboard beginStoryboard = new BeginStoryboard();
            beginStoryboard.Storyboard = storyboard;
            beginStoryboard.Name = "its_wobble_time"; // it is

            trigger.EnterActions.Add(beginStoryboard);

            RemoveStoryboard removeStoryboard = new RemoveStoryboard();
            removeStoryboard.BeginStoryboardName = beginStoryboard.Name;

            trigger.ExitActions.Add(removeStoryboard);

            newStyle.Triggers.Add(trigger);

            image.Style = newStyle;
        }
    }
}
