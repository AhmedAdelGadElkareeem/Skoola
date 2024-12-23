using WytSky.Mobile.Maui.Skoola.ViewModels;

namespace WytSky.Mobile.Maui.Skoola.Views
{
    public partial class HomePage : BaseContentPage
    {
        HomeVM homeVM = new HomeVM();
        public HomePage()
        {
            try
            {
                InitializeComponent();
                BindingContext = homeVM;
            }
            catch (Exception ex)
            {
                ExtensionLogMethods.LogExtension(ex, "", "HomePage", "Constructor");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await homeVM.GetParentCategories();
        }
    }
}