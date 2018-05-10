using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessApp.Common
{
    public static class Utils
    {
        public static async Task<bool> CheckPermissions(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    string title, question, positive, negative;

                    switch (permission)
                    {
                        case Permission.Location:
                            title = $"Location Access permission";
                            question = $"EDA requires {title}. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                        case Permission.MediaLibrary:
                            title = $"Media Access permission";
                            question = $"EDA requires {title} to upload photo. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                        default:
                            title = $"{permission} permission";
                            question = $"EDA requires {permission}. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                    }

                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }

                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    string title, question, positive, negative;

                    switch (permission)
                    {
                        case Permission.Location:
                            title = $"Location Access permission";
                            question = $"EDA requires {title}. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                        case Permission.MediaLibrary:
                            title = $"Media Access permission";
                            question = $"EDA requires {title} to upload photo. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                        default:
                            title = $"{permission} permission";
                            question = $"EDA requires {permission}. Go to Settings to enable access.";
                            positive = "Settings";
                            negative = "Maybe Later";
                            break;
                    }

                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }

            return true;
        }
    }
}
