using System.Web;

namespace Berry.SEOKit.Services {
    public class PageTitleService : IPageTitleService {
        public bool HasTitle() {
            return HttpContext.Current.Items["Berry.SEOKit.Title"] != null
                && !string.IsNullOrWhiteSpace(HttpContext.Current.Items["Berry.SEOKit.Title"].ToString());
        }

        public string GetTitle() {
            return HttpContext.Current.Items["Berry.SEOKit.Title"].ToString();
        }
    }
}