using System;
using System.Web;
using System.Resources;
using Westwind.Globalization;

namespace WebApplication1.Properties
{
    public class GeneratedResourceSettings
    {
        // You can change the ResourceAccess Mode globally in Application_Start        
        public static ResourceAccessMode ResourceAccessMode = DbResourceConfiguration.Current.ResourceAccessMode;
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "3.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AddMoviePage
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("WebApplication1.Properties.AddMoviePage", typeof(AddMoviePage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String AddMovie
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","AddMovie",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String DropYourPosterHere
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","DropYourPosterHere",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String CreateNow
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","CreateNow",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MovieTrailerLink
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","MovieTrailerLink",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String CroppedImage
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","CroppedImage",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String Crop
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","Crop",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String UploadFile
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","UploadFile",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MovieName
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","MovieName",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String UploadedImage
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","UploadedImage",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String Or
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","Or",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String BackToList
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AddMoviePage","BackToList",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}


    [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "3.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Javascript
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("WebApplication1.Properties.Javascript", typeof(Javascript).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String Error
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","Error",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String Success
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","Success",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String PleaseUploadAMoviePoster
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","PleaseUploadAMoviePoster",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String BackToList
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","BackToList",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String AddAnother
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","AddAnother",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String PleaseUploadASmallerImage
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","PleaseUploadASmallerImage",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}

}
