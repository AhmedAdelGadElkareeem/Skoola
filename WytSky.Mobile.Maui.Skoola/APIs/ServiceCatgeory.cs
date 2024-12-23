using System.Collections.ObjectModel;
using System.Text.Json.Nodes;
using WytSky.Mobile.Maui.Skoola.Dtos;
using WytSky.Mobile.Maui.Skoola.Models;

namespace WytSky.Mobile.Maui.Skoola.APIs
{
    public class ServiceCatgeory
    {
        public const string BASE = "appservices";

        #region Categories
        public async static Task<ObservableCollection<CategoryModel>> GetParentCategories()
        {
            try
            {
                var dictionary = new Dictionary<string, string>()
                {
                      {"_datatype", "json"},
                      {"_jsonarray", "1"},
                };
                var result = await Services.RequestProvider.Current.GetData<TempletData<CategoryModel>>(BASE, "parentcategory", dictionary, Enums.AuthorizationType.UserNamePassword);
                if (result != null && result.IsPassed)
                {
                    return result.Data.ItemData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "ServiceCatgeory", "GetParentCategories");
                return null;
            }
        }
        public async static Task<ObservableCollection<CategoryModel>> GetMainCategories(string ParentId)
        {
            try
            {
                var dictionary = new Dictionary<string, string>()
                {
                    {"ParentCategoryID", ParentId},
                    {"_datatype", "json"},
                    {"_jsonarray", "1"},
                };
                var result = await Services.RequestProvider.Current.GetData<TempletData<CategoryModel>>(BASE, "maincategory", dictionary, Enums.AuthorizationType.UserNamePassword, isLoading:false);
                if (result != null && result.IsPassed)
                {
                    return result.Data.ItemData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "ServiceCatgeory", "GetMainCategories");
                return null;
            }
        }
        public async static Task<ObservableCollection<CategoryModel>> GetCategoriesByMainCategoryId(string MainCategoryId)
        {
            try
            {
                var dictionary = new Dictionary<string, string>()
                {
                      {"MainCategoryID", MainCategoryId},
                      {"_datatype", "json"},
                      {"_jsonarray", "1"},
                };
                var result = await Services.RequestProvider.Current.GetData<TempletData<CategoryModel>>(BASE, "category", dictionary, Enums.AuthorizationType.UserNamePassword);
                if (result != null && result.IsPassed)
                {
                    return result.Data.ItemData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "ServiceCatgeory", "GetCategories");
                return null;
            }
        }
        #endregion
    }
}
