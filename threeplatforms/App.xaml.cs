using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace threeplatforms
{
    public partial class App : Application
    {
        static View error(string message)
        {
            return new Label
            {
                Text = message
            };
        }

        public static View tryLoadLibrary(string path)
        {
            // Use the file name to load the assembly into the current
            // application domain.
            Assembly a = Assembly.LoadFile(path);
            if (a == null) {
                return error("failed to load Assembly File");
            }

            // Get the type to use.
            Type myType = a.GetType("test_plugin.Class1");
            if (myType == null)
            {
                return error("failed to get type from Assembly File");
            }

            // Get the method to call.
            MethodInfo myMethod = myType.GetMethod("onCreateView");
            if (myMethod == null)
            {
                return error("failed to get method `onCreateView` from type");
            }

            // Create an instance.
            object obj = Activator.CreateInstance(myType);
            if (obj == null)
            {
                return error("failed to create instance of type");
            }

            // Execute the method.
            View view = (View)myMethod.Invoke(obj, null);
            if (view == null)
            {
                return error("failed to invoke method `onCreateView`, or method `onCreateView` returned null");
            }

            return view;
        }

        StackLayout createTestView()
        {
            StackLayout buttons = new StackLayout();
            buttons.Orientation = StackOrientation.Vertical;

            Button add_Button = new Button()
            {
                Text = "add view"
            };

            Button remove_Button = new Button()
            {
                Text = "remove view"
            };

            buttons.Children.Add(add_Button);
            buttons.Children.Add(remove_Button);

            Frame content = new Frame();

            add_Button.Clicked += (sender, eventArgs) =>
            {
                content.Content = new Button()
                {
                    Text = "I AM AN ADDED BUTTON"
                };
            };

            remove_Button.Clicked += (sender, eventArgs) =>
            {
                content.Content = null;
            };

            StackLayout screenContent = new StackLayout();
            screenContent.Orientation = StackOrientation.Vertical;
            screenContent.Children.Add(content);
            screenContent.Children.Add(buttons);
            return screenContent;
        }

        StackLayout createTestPluginView()
        {
            StackLayout buttons = new StackLayout();
            buttons.Orientation = StackOrientation.Vertical;

            Button add_Button = new Button()
            {
                Text = "add plugin view"
            };

            Button remove_Button = new Button()
            {
                Text = "remove plugin view"
            };

            buttons.Children.Add(add_Button);
            buttons.Children.Add(remove_Button);

            Frame content = new Frame();

            add_Button.Clicked += (sender, eventArgs) =>
            {
                View v = tryLoadLibrary("/Users/smallville7123/Projects/threeplatforms/test_plugin/bin/Debug/netstandard2.1/test_plugin.dll");

                content.Content = v == null ? new Button()
                {
                    Text = "ERROR: PLUGIN COULD NOT BE LOADED"
                } : v;
            };

            remove_Button.Clicked += (sender, eventArgs) =>
            {
                content.Content = null;
            };

            StackLayout screenContent = new StackLayout();
            screenContent.Orientation = StackOrientation.Vertical;
            screenContent.Children.Add(content);
            screenContent.Children.Add(buttons);
            return screenContent;
        }

        public App()
        {
            InitializeComponent();

            StackLayout screenContent = new StackLayout();
            screenContent.Orientation = StackOrientation.Vertical;

            screenContent.Children.Add(createTestView());
            screenContent.Children.Add(createTestPluginView());

            ContentPage screen = new ContentPage();
            screen.Content = screenContent;

            MainPage = screen;
        }

        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
