using Xamarin.Forms;

namespace test_plugin { public class Class1 : plugin_interface.Plugin
    {
        public override View onCreateView()
        {
            return new Button
            {
                Text = "I AM A PLUGIN BUTTON!!!"
            };
        }
    }
}
