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

		public static System.String MovieImageRequired
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Javascript","MovieImageRequired",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}


    [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "3.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MovieModel
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("WebApplication1.Properties.MovieModel", typeof(MovieModel).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String MovieID
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MovieModel","MovieID",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MovieImage
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MovieModel","MovieImage",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MovieNameIsRequired
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MovieModel","MovieNameIsRequired",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MovieName
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MovieModel","MovieName",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}


    [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "3.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MoviesListPage
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("WebApplication1.Properties.MoviesListPage", typeof(MoviesListPage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String Rent
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MoviesListPage","Rent",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String AlreadyRented
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MoviesListPage","AlreadyRented",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String Actions
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MoviesListPage","Actions",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String MoviesListPageTitle
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MoviesListPage","MoviesListPageTitle",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

		public static System.String AddMovie
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("MoviesListPage","AddMovie",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}

}
