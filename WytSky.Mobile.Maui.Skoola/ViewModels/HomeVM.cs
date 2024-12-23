
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WytSky.Mobile.Maui.Skoola.Dtos;
using WytSky.Mobile.Maui.Skoola.Views.Courses;
using Settings = WytSky.Mobile.Maui.Skoola.Helpers.Settings;

namespace WytSky.Mobile.Maui.Skoola.ViewModels;

public partial class HomeVM : BaseViewModel
{
    #region Properties
    [ObservableProperty]
    public ObservableCollection<CategoryModel> parentCategories = new ObservableCollection<CategoryModel>();

    [ObservableProperty]
    public ObservableCollection<CategoryModel> mainCategories = new ObservableCollection<CategoryModel>();

    [ObservableProperty]
    public ObservableCollection<CategoryModel> categories = new ObservableCollection<CategoryModel>();

    [ObservableProperty]
    public bool categoriesVisiblity;

    [ObservableProperty]
    string userName;

    /*[ObservableProperty]
    public ObservableCollection<StMentor> mentors;

    [ObservableProperty]
    public ObservableCollection<StCourse> topRatedCourses;

    [ObservableProperty]
    public ObservableCollection<StCatgeory> categories;

    [ObservableProperty]
    public ObservableCollection<StCourse> mostPopularCourses;*/
    #endregion

    #region Constructor
    public HomeVM()
    {
        UserName = Settings.UserName;
        /* Mentors = new ObservableCollection<StMentor>()
         {
             new StMentor(){Name = "Anaya Singh",Image="avatar"},
             new StMentor(){Name = "Deep Lumari",Image="avatar"},
             new StMentor(){Name = "Rishita Lal",Image="avatar"},
             new StMentor(){Name = "Anaya Bai",Image="avatar"},
             new StMentor(){Name = "Mark Brown",Image="avatar"},
             new StMentor(){Name = "Anaya Singh",Image="avatar"},
             new StMentor(){Name = "Deep Lumari",Image="avatar"},
             new StMentor(){Name = "Rishita Lal",Image="avatar"},
             new StMentor(){Name = "Anaya Bai",Image="avatar"},
             new StMentor(){Name = "Mark Brown",Image="avatar"},
         };
         TopRatedCourses = new ObservableCollection<StCourse>()
         {
              new StCourse(),
              new StCourse(),
              new StCourse(),
              new StCourse(),
              new StCourse(),
              new StCourse(),
              new StCourse(),
         };
         Categories = new ObservableCollection<StCatgeory>()
         {
             new StCatgeory(){IsSelected = true,  NameEn = "All"},
             new StCatgeory(){IsSelected = false, NameEn = "Graphic Designer"},
             new StCatgeory(){IsSelected = false, NameEn = "Software Development"},
         };
         MostPopularCourses = new ObservableCollection<StCourse>()
         {
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
             new StCourse(),
         };*/
    }
    #endregion

    #region Methods
    public async Task GetParentCategories()
    {
        try
        {
            IsRunning = true;
            CategoriesVisiblity = false;

            ParentCategories = await APIs.ServiceCatgeory.GetParentCategories();
            if (ParentCategories != null && ParentCategories.Count > 0)
            {
                ParentCategories[0].TextColor = StringExtensions.ToColorFromResourceKey("White");
                ParentCategories[0].BackgroundColor = StringExtensions.ToColorFromResourceKey("PrimaryColor");

                await GetMainCategoriesByParentCategoryId(ParentCategories[0].Id.ToString());
            }
            IsRunning = false;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "", "GetParentCategories");
        }
    }

    public async Task GetMainCategoriesByParentCategoryId(string ParentId)
    {
        IsRunning = true;
        MainCategories = await APIs.ServiceCatgeory.GetMainCategories(ParentId);
        IsRunning = false;
    }

    public async Task GetCategoriesByMainCategoryId(string mainCategoryId)
    {
        try
        {
            IsRunning = true;
            Categories = await APIs.ServiceCatgeory.GetCategoriesByMainCategoryId(mainCategoryId);
            if (Categories != null && Categories.Count > 0)
            {
                CategoriesVisiblity = true;
            }
            IsRunning = false;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "CategoryVM", "GetCategories");
        }
    }
    #endregion

    #region Commands
    [RelayCommand]
    public async Task SelectParentCategory(CategoryModel categoryModel)
    {
        try
        {
            CategoriesVisiblity = false;

            foreach (var item in ParentCategories)
            {
                item.IsSelected = false;
                item.TextColor = Colors.DimGray;
                item.BackgroundColor = Colors.White;
            }

            categoryModel.IsSelected = true;
            categoryModel.TextColor = StringExtensions.ToColorFromResourceKey("White");
            categoryModel.BackgroundColor = StringExtensions.ToColorFromResourceKey("PrimaryColor");

            await GetMainCategoriesByParentCategoryId(categoryModel.Id.ToString());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectCategory");
        }
    }

    [RelayCommand]
    public async Task SelectMainCategory(CategoryModel categoryModel)
    {
        try
        {
            await GetCategoriesByMainCategoryId(categoryModel.Id.ToString());
            //await NavigateToPage.OpenPage(new CategoriesPage(categoryModel));
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectMainCategory");
        }
    }

    [RelayCommand]
    public async Task SelectCategory(CategoryModel categoryModel)
    {
        try
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new AllCoursesByCategoryPage(categoryModel));
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectCategory");
        }
    }


    /*[RelayCommand]
    public void SelectCategory(StCatgeory stCatgeory)
    {
        try
        {
            foreach (var item in Categories)
            {
                item.IsSelected = false;
            }
            stCatgeory.IsSelected = true;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectCategory");
        }
    }

    [RelayCommand]
    public async Task SelectCourse(StCourse stCourse)
    {
        try
        {
            IsRunning = true;
            await NavigateToPage.OpenPage(new CourseDetailsPage());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectCourse");
        }
        finally
        {
            IsRunning = false;
        }
    }

    [RelayCommand]
    public async Task EnrolCourse()
    {
        try
        {
            IsRunning = true;
            await NavigateToPage.OpenPage(new EnrollCoursePage());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "EnrolCourse");
        }
        finally
        {
            IsRunning = false;
        }
    }

    [RelayCommand]
    public async Task SelectedMentor()
    {
        try
        {
            IsRunning = true;
            await NavigateToPage.OpenPage(new MentorDetailsPage());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "SelectedMentor");
        }
        finally
        {
            IsRunning = false;
        }
    }

    [RelayCommand]
    public async Task TopMentors()
    {
        try
        {
            IsRunning = true;
            await NavigateToPage.OpenPage(new AllMentorsPage());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "TopMentors");
        }
        finally
        {
            IsRunning = false;
        }
    }

    [RelayCommand]
    public async Task OpenMostPopularCourses()
    {
        try
        {
            IsRunning = true;
            await NavigateToPage.OpenPage(new MostPopularCoursesPage());
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex.Message, "", "HomeVM", "OpenMostPopularCourses");
        }
        finally
        {
            IsRunning = false;
        }
    }*/
    #endregion



}